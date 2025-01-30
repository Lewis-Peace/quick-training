using Lift.Buddy.API.Interfaces;
using Lift.Buddy.Core.Models;
using Lift.Buddy.Core;
using Microsoft.EntityFrameworkCore;
using Lift.Buddy.Core.Database;

namespace Lift.Buddy.API.Services
{
    public class FrontpageService: IFrontpageService
    {
        private readonly LiftBuddyContext _context;
        private readonly IDatabaseMapper _mapper;

        public FrontpageService(LiftBuddyContext context, IDatabaseMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Response<FrontpageDTO>> GetFrontpage(Guid trainerGuid)
        {
            var response = new Response<FrontpageDTO>();
            try
            {
                var frontpage = await _context.Frontpages.FirstOrDefaultAsync(x => x.Id == trainerGuid);

                if (frontpage != null)
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

    }
}
