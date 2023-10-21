using Lift.Buddy.Core.Database.Entities;
using Lift.Buddy.Core.Models;

namespace Lift.Buddy.API.Interfaces
{
    public interface IFrontpageService
    {
        public Task<Response<FrontpageDTO>> GetFrontpage(Guid trainerGuid);
        public Task<Response<FrontpageDTO>> AddFrontpage(Guid trainerGuid, FrontpageDTO frontpage);
        public Task<Response<FrontpageDTO>> UpdateFrontpage(Guid trainerGuid, FrontpageDTO frontpage);
    }
}
