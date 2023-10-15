using Lift.Buddy.API.Interfaces;
using Lift.Buddy.Core;
using Lift.Buddy.Core.Database;
using Lift.Buddy.Core.Models;
using Microsoft.EntityFrameworkCore;
using Lift.Buddy.Core.Database.Entities;

namespace Lift.Buddy.API.Services
{
    public class SettingsService : ISettingsService
    {
        private readonly LiftBuddyContext _context;
        private readonly IDatabaseMapper _mapper;

        public SettingsService(LiftBuddyContext context, IDatabaseMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Response<SettingsDTO>> AddSettings(Guid userId)
        {
            var response = new Response<SettingsDTO>();
            try
            {
                var settings = new Settings();
                var user = await _context.Users.FirstOrDefaultAsync(x => x.UserId == userId);
                settings.User = user;

                _context.Settings.Add(settings);

                if (await _context.SaveChangesAsync() < 1)
                {
                    throw new Exception("Failed to update database.");
                }

                response.Result = true;
                response.Body = new[] { _mapper.Map(settings) };
            }
            catch (Exception ex)
            {
                response.Result = false;
                response.Notes = Utils.ErrorMessage(nameof(DeleteSettings), ex);
            }
            return response;
        }

        public async Task<Response<SettingsDTO>> DeleteSettings(Guid userId)
        {
            var response = new Response<SettingsDTO>();
            try
            {
                var settings = await _context.Settings.Include(x => x.User).FirstOrDefaultAsync(x => x.UserId == userId);

                if (settings == null)
                {
                    throw new Exception("Settings for user not found.");
                }

                _context.Settings.Update(new Settings { User = settings.User });

                if (await _context.SaveChangesAsync() < 1)
                {
                    throw new Exception("Failed to update database.");
                }

                response.Result = true;
                response.Body = new[] { _mapper.Map(settings) };
            }
            catch (Exception ex)
            {
                response.Result = false;
                response.Notes = Utils.ErrorMessage(nameof(DeleteSettings), ex);
            }
            return response;
        }

        public async Task<Response<SettingsDTO>> GetSettings(Guid userId)
        {
            var response = new Response<SettingsDTO>();
            try
            {
                var settings = await _context.Settings.Include(x => x.User).FirstOrDefaultAsync(x => x.User.UserId == userId);

                if (settings == null)
                {
                    throw new Exception("Settings for user not found. Loading default setings");
                }

                response.Result = true;
                response.Body = new[]{ _mapper.Map(settings) };
            }
            catch (Exception ex)
            {
                response.Result = false;
                response.Notes = Utils.ErrorMessage(nameof(GetSettings), ex);
            }
            return response;
        }

        public async Task<Response<SettingsDTO>> UpdateSettings(Guid userId, SettingsDTO settings)
        {
            var response = new Response<SettingsDTO>();
            try
            {
                var settingsToDb = _mapper.Map(settings);
                var user = await _context.Users.FirstOrDefaultAsync(x => x.UserId == userId);
                settingsToDb.User = user;

                _context.Settings.Update(settingsToDb);

                if (await _context.SaveChangesAsync() < 1)
                {
                    throw new Exception("Failed to update database.");
                }

                response.Result = true;
                response.Body = new[] { settings };
            }
            catch (Exception ex)
            {
                response.Result = false;
                response.Notes = Utils.ErrorMessage(nameof(UpdateSettings), ex);
            }
            return response;
        }
    }
}
