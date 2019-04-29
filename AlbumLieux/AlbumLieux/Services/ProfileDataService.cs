using AlbumLieux.Models;
using AlbumLieux.Models.Requests;
using System;
using System.Threading.Tasks;

namespace AlbumLieux.Services
{
    public interface IProfileDataService
    {
        Task<User> GetMe();
        Task<User> UpdateMe(string firstName, string lastName, int imageId);
        Task UpdatePassword(string newPassword, string oldPassword);
    }
    public class ProfileDataService : BaseDataService, IProfileDataService
    {
        public async Task<User> GetMe()
        {
            var response = await GetAsync<User>("/me", authenticated: true);
            if (response.IsSuccess)
            {
                return response.Data;
            }
            else
            {
                throw new NotImplementedException();
            }
        }

        public async Task<User> UpdateMe(string firstName, string lastName, int imageId)
        {
            var response = await PatchAsync<User, UpdateProfileRequest>("/me", new UpdateProfileRequest
            {
                FirstName = firstName,
                LastName = lastName,
                ImageId = imageId
            }, authenticated: true);

            if (response.IsSuccess)
            {
                return response.Data;
            }
            else
            {
                throw new NotImplementedException();
            }
        }

        public async Task UpdatePassword(string newPassword, string oldPassword)
        {
            var response = await PatchAsync<User, UpdatePasswordRequest>("/me/password", new UpdatePasswordRequest
            {
                OldPassword = oldPassword,
                NewPassword = newPassword
            }, authenticated: true);

            if (!response.IsSuccess)
            {
                throw new NotImplementedException();
            }
        }
    }
}
