using Database.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database
{
    public interface IUserInformationRepository
    {
        Task AddUserInformationAsync(UserInformation information);
        Task<UserInformation> GetUserInformationAsync(string userId);
        Task UpdateUserInformationAsync(UserInformation information);
        Task DeleteUserInformationAsync(string userId);

        Task AddLevelAsync(string userId, Level level);
        Task<Level> GetLevelAsync(string userId, string levelName);
        Task UpdateLevelAsync(Level level);
        Task DeleteLevelAsync(string userId, string levelName);
    }
}
