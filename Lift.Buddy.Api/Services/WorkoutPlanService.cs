using Lift.Buddy.API.Interfaces;
using Lift.Buddy.Core;
using Lift.Buddy.Core.Database;
using Lift.Buddy.Core.Database.Entities;
using Lift.Buddy.Core.Models;
using Microsoft.EntityFrameworkCore;
using MigraDoc.DocumentObjectModel;
using MigraDoc.Rendering;
using System.Text;

namespace Lift.Buddy.API.Services
{
    public class WorkoutPlanService : IWorkoutPlanService
    {
        private readonly LiftBuddyContext _context;
        private readonly IDatabaseMapper _mapper;

        public WorkoutPlanService(LiftBuddyContext context, IDatabaseMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        #region Get

        public async Task<Response<WorkoutPlanDTO>> GetAssignedWorkoutPlans(Guid userId)
        {
            var response = new Response<WorkoutPlanDTO>();

            try
            {
                var workoutPlan = await _context.Users
                    .Include(x => x.AssignedPlans)
                        .ThenInclude(x => x.Reviews)
                    .Include(x => x.AssignedPlans)
                        .ThenInclude(wp => wp.WorkoutDays)
                        .ThenInclude(wd => wd.Exercises)
                    .Where(x => x.UserId == userId)
                    .SelectMany(x => x.AssignedPlans)
                    .ToArrayAsync();

                response.Result = true;
                response.Body = workoutPlan.Select(p => _mapper.Map(p, userId));
            }
            catch (Exception ex)
            {
                response.Result = false;
                response.Notes = Utils.ErrorMessage(nameof(GetAssignedWorkoutPlans), ex);
            }

            return response;
        }

        public async Task<Response<WorkoutPlanDTO>> GetWorkoutPlanById(Guid workoutplanId)
        {
            var response = new Response<WorkoutPlanDTO>();

            try
            {
                var workoutPlan = await _context.WorkoutPlans
                    .Include(wp => wp.WorkoutDays)
                        .ThenInclude(wd => wd.Exercises)
                    .SingleOrDefaultAsync(x => x.WorkoutPlanId == workoutplanId);

                if (workoutPlan == null) throw new Exception("The workplan does not exist in the database.");

                response.Result = true;
                response.Body = new WorkoutPlanDTO[] { _mapper.Map(workoutPlan, null) };
            }
            catch (Exception ex)
            {
                response.Result = false;
                response.Notes = Utils.ErrorMessage(nameof(GetWorkoutPlanById), ex);
            }

            return response;
        }

        public async Task<Response<WorkoutPlanDTO>> GetUserWorkoutPlans(Guid userId)
        {
            var response = new Response<WorkoutPlanDTO>();

            try
            {
                // TODO dovrà diventare u.AssignedPlans
                // TODO querare direttamente i workout
                var user = await _context.Users
                    .Include(u => u.CreatedPlans)
                        .ThenInclude(x => x.Reviews)
                    .Include(u => u.CreatedPlans)
                        .ThenInclude(p => p.WorkoutDays)
                        .ThenInclude(p => p.Exercises)
                    .SingleOrDefaultAsync(x => x.UserId == userId);

                if (user == null) throw new Exception($"User with user ID '{userId} doesn't exists.");

                var workoutPlans = user?.CreatedPlans;

                response.Result = true;
                response.Body = workoutPlans?.Select(p => _mapper.Map(p, userId));
            }
            catch (Exception ex)
            {
                response.Result = false;
                response.Notes = Utils.ErrorMessage(nameof(GetUserWorkoutPlans), ex);
            }

            return response;
        }

        public async Task<Response<WorkoutPlanDTO>> GetWorkoutPlanCreatedByUser(Guid userId)
        {
            var response = new Response<WorkoutPlanDTO>();

            try
            {
                var workoutPlans = await _context.WorkoutPlans
                    .Where(x => x.Creator.UserId == userId)
                    .Include(x => x.WorkoutDays)
                    .Include(x => x.Reviews)
                    .ToArrayAsync();

                response.Body = workoutPlans.Select(p => _mapper.Map(p, userId));
                response.Result = true;
            }
            catch (Exception ex)
            {
                response.Result = false;
                response.Notes = Utils.ErrorMessage(nameof(GetWorkoutPlanCreatedByUser), ex);
            }

            return response;
        }

        public async Task<Response<UserDTO>> GetWorkoutPlanSubscribers(Guid workoutPlanId)
        {
            var response = new Response<UserDTO>();
            try
            {
                var workoutPlan = await _context.WorkoutPlans
                    .Include(x => x.Users)
                    .FirstOrDefaultAsync(x => x.WorkoutPlanId == workoutPlanId);

                if (workoutPlan == null)
                {
                    throw new Exception("Workout plan does not exist.");
                }

                response.Result = true;
                response.Body = workoutPlan.Users.Select(x => _mapper.Map(x));
            }
            catch (Exception ex)
            {
                response.Result = false;
                response.Notes = Utils.ErrorMessage(nameof(GetWorkoutPlanSubscribers), ex);
            }
            return response;
        }

        public async Task<Response<Document>> GetWorkoutPlanPdf(Guid workoutPlanId)
        {
            var response = new Response<Document>();

            try
            {
                var workoutPlan = await _context.WorkoutPlans
                    .SingleOrDefaultAsync(x => x.WorkoutPlanId == workoutPlanId);

                if (workoutPlan == null) throw new Exception("The workplan does not exist in the database.");

                var doc = _mapper.Map(workoutPlan, null).ToPDF();

                // TODO: servizio creazione PDF
                var pdfRenderer = new PdfDocumentRenderer(false)
                {
                    Document = doc
                };

                Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
                pdfRenderer.RenderDocument();

                const string filename = "HelloWorld.pdf";
                pdfRenderer.PdfDocument.Save(filename);
            }
            catch (Exception ex)
            {
                response.Result = false;
                response.Notes = Utils.ErrorMessage(nameof(GetWorkoutPlanPdf), ex);
            }

            return response;
        }
        #endregion

        #region Add
        public async Task<Response<WorkoutPlanDTO>> AddWorkoutPlan(WorkoutPlanDTO workoutPlan)
        {
            var response = new Response<WorkoutPlanDTO>();

            try
            {
                var plan = _mapper.Map(workoutPlan);
                plan.Creator = await _context.Users
                    .SingleOrDefaultAsync(u => u.UserId == plan.CreatorId);

                await _context.WorkoutPlans.AddAsync(plan);

                if ((await _context.SaveChangesAsync()) < 1)
                {
                    throw new Exception("Failed to save changes in database");
                }

                response.Result = true;
                response.Body = new WorkoutPlanDTO[] { workoutPlan };
            }
            catch (Exception ex)
            {
                response.Result = false;
                response.Notes = Utils.ErrorMessage(nameof(AddWorkoutPlan), ex);
            }

            return response;
        }
        #endregion

        #region Delete
        public async Task<Response<WorkoutPlanDTO>> DeleteWorkoutPlan(Guid workoutPlanId)
        {
            var response = new Response<WorkoutPlanDTO>();
            try
            {
                var workoutPlan = await _context.WorkoutPlans
                    .SingleOrDefaultAsync(p => p.WorkoutPlanId == workoutPlanId);

                if (workoutPlan == null) throw new Exception("The workplan does not exist in the database.");

                _context.WorkoutPlans.Remove(workoutPlan);
                if ((await _context.SaveChangesAsync()) < 1)
                {
                    throw new Exception("Failed to save changes in database");
                }

                response.Result = true;
                response.Body = new List<WorkoutPlanDTO> { _mapper.Map(workoutPlan, null) };
            }
            catch (Exception ex)
            {
                response.Result = false;
                response.Notes = Utils.ErrorMessage(nameof(DeleteWorkoutPlan), ex);
            }

            return response;
        }
        #endregion

        #region Update
        public async Task<Response<WorkoutPlanDTO>> UpdateWorkoutPlan(WorkoutPlanDTO workoutPlan)
        {
            var response = new Response<WorkoutPlanDTO>();

            try
            {
                var plan = _mapper.Map(workoutPlan);

                var oldPlan = await _context.WorkoutPlans
                    .SingleAsync(p => p.WorkoutPlanId == plan.WorkoutPlanId);

                _context.WorkoutPlans.Remove(oldPlan);
                await _context.SaveChangesAsync();

                await _context.WorkoutPlans.AddAsync(plan);

                if ((await _context.SaveChangesAsync()) < 1)
                {
                    throw new Exception("Failed to save changes in database");
                }

                response.Result = true;
                response.Body = new WorkoutPlanDTO[] { workoutPlan };
            }
            catch (Exception ex)
            {
                response.Result = false;
                response.Notes = Utils.ErrorMessage(nameof(UpdateWorkoutPlan), ex);
            }

            return response;
        }

        public async Task<Response<WorkoutPlanDTO>> ReviewWorkoutPlan(Guid userId, ReviewDTO reviewDTO)
        {
            var response = new Response<WorkoutPlanDTO>();

            try
            {
                var currentPlan = await _context.WorkoutPlans
                    .Include(x => x.Reviews)
                    .FirstOrDefaultAsync(x => x.WorkoutPlanId == reviewDTO.WorkoutPlanId);

                if (currentPlan == null)
                    throw new Exception($"Trying to review non existing workout plan with id {reviewDTO.WorkoutPlanId}.");

                var reviewfromDb = await _context.Reviews.FirstOrDefaultAsync(x => x.WorkoutPlanId ==  currentPlan.WorkoutPlanId && x.UserId == userId);

                if (reviewfromDb == null)
                {
                    var oldAverage = currentPlan.ReviewAverage;
                    var amountOfPeopleThatVoted = currentPlan.Reviews.Count;
                    var review = _mapper.Map(reviewDTO);
                    review.UserId = userId;
                    _context.Reviews.Add(review);
                    currentPlan.ReviewAverage = CalculateMean(oldAverage, amountOfPeopleThatVoted, reviewDTO.Value);
                } 
                else
                {
                    reviewfromDb.Value = reviewDTO.Value;
                    _context.Reviews.Update(reviewfromDb);
                    var count = 0;
                    foreach (var reviewForAvg in currentPlan.Reviews)
                    {
                        count += reviewForAvg.Value;
                    }
                    currentPlan.ReviewAverage = count / currentPlan.Reviews.Count;
                }


                _context.WorkoutPlans.Update(currentPlan);

                if ((await _context.SaveChangesAsync()) < 1)
                {
                    throw new Exception("Failed to save changes in database");
                }

                response.Result = true;
                response.Body = new WorkoutPlanDTO[] { _mapper.Map(currentPlan, userId) };
            }
            catch (Exception ex)
            {
                response.Result = false;
                response.Notes = Utils.ErrorMessage(nameof(UpdateWorkoutPlan), ex);
            }

            return response;
        }

        public async Task<Response<UserDTO>> SetWorkoutPlanAssignment(Guid id, UserDTO[] userDTO)
        {
            var response = new Response<UserDTO>();

            try
            {
                var workoutPlan = await _context.WorkoutPlans
                    .Where(x => x.WorkoutPlanId.Equals(id))
                    .Include(x => x.Users)
                    .FirstOrDefaultAsync();

                if (workoutPlan == null) throw new Exception($"Workout plan {id} doesn't exist.");

                if (userDTO == null) throw new Exception();

                List<User> users = new List<User>();
                foreach (var user in userDTO)
                {
                    var userFromDb = await _context.Users
                            .Where(x => x.UserId == user.Id)
                            .FirstOrDefaultAsync();
                    if (userFromDb != null)
                    {
                        users.Add(userFromDb);
                    }
                }

                workoutPlan.Users = users;

                _context.Update(workoutPlan);

                if (await _context.SaveChangesAsync() < 1) throw new Exception("Failed to save change into database.");

                response.Result = true;
            }
            catch (Exception ex)
            {
                response.Result = false;
                response.Notes = Utils.ErrorMessage(nameof(SetWorkoutPlanAssignment), ex);
            }

            return response;
        }
        #endregion

        private int CalculateMean(double currentMean, int count, double value)
            => (int)(value + (currentMean * count)) / (count + 1);

    }
}
