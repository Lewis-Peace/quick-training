using Lift.Buddy.API.Interfaces;
using Lift.Buddy.Core;
using Lift.Buddy.Core.Database;
using Lift.Buddy.Core.Database.Entities;
using Lift.Buddy.Core.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Lift.Buddy.API.Services
{
    public class TrainerService : ITrainerService
    {
        private readonly LiftBuddyContext _context;
        private readonly IDatabaseMapper _mapper;

        public TrainerService(LiftBuddyContext context, IDatabaseMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Response<UserDTO>> GetAthletes(Guid trainerGuid)
        {
            var response = new Response<UserDTO>();

            try
            {
                var association = await _context.WorkoutPlans
                    .Where(x => x.CreatorId == trainerGuid)
                    .Include(x => x.Users)
                    .SelectMany(x => x.Users)
                    .Distinct()
                    .Select(x => _mapper.Map(x))
                    .ToArrayAsync();

                response.Result = true;
                response.Body = association;
            }
            catch (Exception ex)
            {
                response.Result = false;
                response.Notes = Utils.ErrorMessage(nameof(GetAthletes), ex);
            }

            return response;
        }

        public async Task<Response<WorkoutPlan>> RemoveFollower(Guid trainerGuid, User athlete)
        {
            var response = new Response<WorkoutPlan>();

            try
            {
                var workplans = _context.WorkoutPlans
                    .Where(x => x.CreatorId == trainerGuid)
                    .Include(x => x.Users);

                foreach (var workplan in workplans)
                {
                    if (workplan.Users.Contains(athlete))
                    {
                        workplan.Users.Remove(athlete);
                        _context.WorkoutPlans.Update(workplan);
                        response.Body = response.Body.Concat(new[] { workplan });
                    }
                }

                if (await _context.SaveChangesAsync() < 1) 
                {
                    throw new Exception("No changes done to database");
                }

                response.Result = true;
            }
            catch (Exception ex)
            {
                response.Result = false;
                response.Notes = Utils.ErrorMessage(nameof(RemoveFollower), ex);
            }

            return response;
        }

        #region Frontpage

        public async Task<Response<FrontpageDTO>> GetFrontpage(Guid trainerGuid)
        {
            var response = new Response<FrontpageDTO>();
            try
            {
                var frontpage = await _context.Frontpages.FirstOrDefaultAsync(x => x.Id == trainerGuid);

                if ( frontpage != null)
                {
                    response.Body = new[] { _mapper.Map(frontpage) };
                }
                response.Result = true;
            }
            catch (Exception ex)
            {
                response.Result = false;
                response.Notes = Utils.ErrorMessage(nameof(AddFrontpage), ex);
            }

            return response;
        }
        public async Task<Response<FrontpageDTO>> AddFrontpage(Guid trainerGuid, FrontpageDTO frontpage)
        {
            var response = new Response<FrontpageDTO>();
            try
            {
                var dbFrontpage = _mapper.Map(frontpage);
                dbFrontpage.Id = trainerGuid;
                _context.Frontpages.Add(dbFrontpage);
                if (await _context.SaveChangesAsync() < 1)
                {
                    throw new Exception("Failed to save in database.");
                }

                response.Result = true;
                response.Body = new[] { frontpage };
            }
            catch (Exception ex)
            {
                response.Result = false;
                response.Notes = Utils.ErrorMessage(nameof(AddFrontpage), ex);
            }

            return response;
        }
        public async Task<Response<FrontpageDTO>> UpdateFrontpage(Guid trainerGuid, FrontpageDTO frontpage)
        {
            var response = new Response<FrontpageDTO>();
            try
            {
                var dbFrontpage = _mapper.Map(frontpage);
                dbFrontpage.Id = trainerGuid;
                _context.Frontpages.Update(dbFrontpage);
                if (await _context.SaveChangesAsync() < 1)
                {
                    throw new Exception("Failed to save in database.");
                }

                response.Result = true;
                response.Body = new[] { frontpage };
            }
            catch (Exception ex)
            {
                response.Result = false;
                response.Notes = Utils.ErrorMessage(nameof(AddFrontpage), ex);
            }

            return response;
        }


        #endregion
    }
}
