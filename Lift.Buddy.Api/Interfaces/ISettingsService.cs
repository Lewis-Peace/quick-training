using Lift.Buddy.Core.Models;

namespace Lift.Buddy.API.Interfaces
{
    public interface ISettingsService
    {
        public Task<Response<SettingsDTO>> GetSettings(Guid userId);
        public Task<Response<SettingsDTO>> AddSettings(Guid userId);
        public Task<Response<SettingsDTO>> UpdateSettings(Guid userId, SettingsDTO settings);
        public Task<Response<SettingsDTO>> DeleteSettings(Guid userId);
    }
}
