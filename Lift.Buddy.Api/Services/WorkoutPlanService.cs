using Lift.Buddy.API.Interfaces;
using Lift.Buddy.Core;
using Lift.Buddy.Core.DB;
using Lift.Buddy.Core.DB.Models;
using Lift.Buddy.Core.Models;
using Microsoft.EntityFrameworkCore;
using MigraDoc.DocumentObjectModel;
using MigraDoc.Rendering;
using PdfSharp.Pdf;

namespace Lift.Buddy.API.Services
{
    public class WorkoutPlanService : IWorkoutPlanService
    {
        private readonly DBContext _context;

        public WorkoutPlanService(DBContext context)
        {
            _context = context;
        }

        #region Get
        public async Task<Response<WorkoutPlan>> GetWorkoutPlan(int id)
        {
            var response = new Response<WorkoutPlan>();

            try
            {
                List<WorkoutPlan> workoutSchedules;
                if (id > 0)
                {
                    workoutSchedules = await _context.WorkoutSchedules.Where(x => x.Id == id).ToListAsync();
                } else
                {
                    workoutSchedules = await _context.WorkoutSchedules.ToListAsync();
                }

                response.result = true;
                response.body = workoutSchedules;
            }
            catch (Exception ex)
            {
                response.result = false;
                response.notes = Utils.ErrorMessage(nameof(GetWorkoutPlan), ex);
            }

            return response;
        }

        public async Task<Response<WorkoutPlan>> GetWorkoutPlanAssignedToUsername(string username)
        {
            var response = new Response<WorkoutPlan>();

            try
            {
                if (username == String.Empty)
                {
                    throw new Exception("No username given.");
                }
                 
                var workoutAssignments = await _context.WorkoutAssignments
                    .Where(x => x.WorkoutUser == username)
                    .ToListAsync();

                List<WorkoutPlan> workoutSchedules;
                foreach (var workoutAssignment in workoutAssignments)
                {
                    workoutSchedules = await _context.WorkoutSchedules
                        .Where(x => x.WorkoutAssignments.Contains(workoutAssignment))
                        .ToListAsync();

                    response.body = response.body.Concat(workoutSchedules).ToList();
                }

                response.result = true;
            }
            catch (Exception ex)
            {
                response.result = false;
                response.notes = Utils.ErrorMessage(nameof(GetWorkoutPlan), ex);
            }

            return response;
        }

        public async Task<Response<WorkoutPlan>> GetWorkoutPlanCreatedByUsername(string username)
        {
            var response = new Response<WorkoutPlan>();

            try
            {
                if (username == String.Empty)
                {
                    throw new Exception("No username given.");
                }

                var workoutSchedules = await _context.WorkoutSchedules
                    .Where(x => x.CreatedBy == username)
                    .ToListAsync();

                response.body = workoutSchedules;
                response.result = true;
            }
            catch (Exception ex)
            {
                response.result = false;
                response.notes = Utils.ErrorMessage(nameof(GetWorkoutPlan), ex);
            }

            return response;
        }

        public async Task<Response<int>> GetWorkoutPlanSubscribersNumber(int workoutPlanId)
        {
            var response = new Response<int>();
            var subscribersCount = new List<int>();
            try
            {
                var workoutPlanSubscribers = await _context.WorkoutAssignments.Where(x => x.WorkoutId == workoutPlanId).ToListAsync();

                response.result = true;
                subscribersCount.Add(workoutPlanSubscribers.Count);
                response.body = subscribersCount;
            }
            catch (Exception ex)
            {
                response.result = false;
                response.notes = Utils.ErrorMessage(nameof(GetWorkoutPlanSubscribersNumber), ex);
            }
            return response;
        }

        public async Task<Response<Document>> GetWorkplanPdf(int workplanId)
        {
            var response = new Response<Document>();

            try
            {
                var workoutPlan = _context.WorkoutSchedules.Where(x => x.Id == workplanId).FirstOrDefault();

                if (workoutPlan == null)
                {
                    throw new Exception("There workplan does not exist in the database.");
                }

                var doc = workoutPlan.WorkoutDays[0].GetPDF();
                doc.UseCmykColor = true;
                PdfDocumentRenderer pdfRenderer = new PdfDocumentRenderer(false);
                pdfRenderer.Document = doc;

                System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

                pdfRenderer.RenderDocument();

                const string filename = "HelloWorld.pdf";
                pdfRenderer.PdfDocument.Save(filename);
            }
            catch (Exception ex)
            {
                response.result = false;
                response.notes = Utils.ErrorMessage(nameof(GetWorkplanPdf), ex);
            }

            return response;
        } 
        #endregion

        #region Add
        public async Task<Response<WorkoutPlan>> AddWorkoutPlan(WorkoutPlan schedule)
        {
            var response = new Response<WorkoutPlan>();

            try
            {
                _context.WorkoutSchedules.Add(schedule);

                if ((await _context.SaveChangesAsync()) < 1)
                {
                    throw new Exception("Failed to save changes in database");
                }
                response.result = true;
                response.body = new List<WorkoutPlan> { schedule };
            }
            catch (Exception ex)
            {
                response.result = false;
                response.notes = Utils.ErrorMessage(nameof(AddWorkoutPlan), ex);
            }

            return response;
        }
        #endregion

        #region Delete
        public async Task<Response<WorkoutPlan>> DeleteWorkoutPlan(WorkoutPlan schedule)
        {
            var response = new Response<WorkoutPlan>();

            try
            {
                _context.WorkoutSchedules.Remove(schedule);

                if ((await _context.SaveChangesAsync()) < 1)
                {
                    throw new Exception("Failed to save changes in database");
                }

                response.result = true;
                response.body = new List<WorkoutPlan> { schedule };
            }
            catch (Exception ex)
            {
                response.result = false;
                response.notes = Utils.ErrorMessage(nameof(DeleteWorkoutPlan), ex);
            }

            return response;
        }
        #endregion

        #region Update
        public async Task<Response<WorkoutPlan>> UpdateWorkoutPlan(WorkoutPlan schedule)
        {
            var response = new Response<WorkoutPlan>();

            try
            {
                _context.WorkoutSchedules.Update(schedule);

                if ((await _context.SaveChangesAsync()) < 1)
                {
                    throw new Exception("Failed to save changes in database");
                }

                response.result = true;
                response.body = new List<WorkoutPlan> { schedule };
            }
            catch (Exception ex)
            {
                response.result = false;
                response.notes = Utils.ErrorMessage(nameof(UpdateWorkoutPlan), ex);
            }

            return response;
        }

        public async Task<Response<WorkoutPlan>> ReviewWorkoutPlan(WorkoutPlan schedule)
        {
            var response = new Response<WorkoutPlan>();

            try
            {
                if (schedule == null)
                {
                    throw new Exception("Cannot review with empty data.");
                }

                var oldSchedule = await _context.WorkoutSchedules.FirstOrDefaultAsync(x => x.Id == schedule.Id);

                if (oldSchedule == null)
                {
                    throw new Exception($"Trying to review non existing workout plan with id {schedule.Id}.");
                }

                oldSchedule.ReviewAverage = (schedule.ReviewAverage + (oldSchedule.ReviewAverage * oldSchedule.ReviewersAmount)) / (oldSchedule.ReviewersAmount + 1);
                oldSchedule.ReviewersAmount++;

                _context.WorkoutSchedules.Update(oldSchedule);

                if ((await _context.SaveChangesAsync()) < 1)
                {
                    throw new Exception("Failed to save changes in database");
                }

                response.result = true;
                response.body = new List<WorkoutPlan> { schedule };
            }
            catch (Exception ex)
            {
                response.result = false;
                response.notes = Utils.ErrorMessage(nameof(UpdateWorkoutPlan), ex);
            }

            return response;
        }
        #endregion

    }
}
