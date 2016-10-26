using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Database.Entities;
using Configuration.Configuration;
using MongoDB.Driver;

namespace Database.MongoDB
{
    public class UserInformationRepository : IUserInformationRepository
    {
        private IMongoCollection<UserInformation> _informations;
        public UserInformationRepository(IConfiguration configuration)
        {
            var client = new MongoClient();
            var db = client.GetDatabase(configuration.NameDatabase);
            _informations = db.GetCollection<UserInformation>(nameof(UserInformation));
        }

        public async Task AddLevelAsync(string userId, Level level)
        {
            var update = Builders<UserInformation>.Update
                .AddToSet(i => i.Levels, level);
            await _informations.UpdateOneAsync(i => i.UserId == userId, update);
        }

        public async Task AddUserInformationAsync(UserInformation information)
        {
            await _informations.InsertOneAsync(information);
        }

        public async Task DeleteLevelAsync(string userId, string levelName)
        {
            var level = await GetLevelAsync(userId, levelName);
            var update = Builders<UserInformation>.Update
                .Pull(i => i.Levels, level);
            await _informations.UpdateOneAsync(i => i.UserId == userId, update);
        }

        public async Task DeleteUserInformationAsync(string userId)
        {
            await _informations.DeleteOneAsync(i => i.UserId == userId);
        }

        public async Task<Level> GetLevelAsync(string userId, string levelName)
        {
            var levels = (await _informations.FindAsync(i => i.UserId == userId)).FirstOrDefault().Levels;
            return levels.FirstOrDefault(l => l.Name == levelName);
        }

        public async Task<UserInformation> GetUserInformationAsync(string userId)
        {
            return (await _informations.FindAsync(i => i.UserId == userId)).FirstOrDefault();
        }

        public async Task UpdateLevelAsync(string userId, Level level)
        {
            
            var update = Builders<UserInformation>.Update.Inc(i => i.Levels.FirstOrDefault(l => l.Name == level.Name).AttemptsCount, 1);
            if (level.SummaryAttemptsTime != null)
                update.Set(i => i.Levels.FirstOrDefault(l => l.Name == level.Name).SummaryAttemptsTime, level.SummaryAttemptsTime);
            if (level.SuccessfulAttemptTime != null)
                update.Set(i => i.Levels.FirstOrDefault(l => l.Name == level.Name).SuccessfulAttemptTime, level.SuccessfulAttemptTime);
            if (level.Stars != null)
                update.Set(i => i.Levels.FirstOrDefault(l => l.Name == level.Name).Stars, level.Stars);
            await _informations.UpdateOneAsync(i => i.UserId == userId, update);
        }

        public async Task UpdateUserInformationAsync(UserInformation information)
        {
            var update = Builders<UserInformation>.Update
                .Set(i => i.CompletedLevelsCount, information.CompletedLevelsCount);
            await _informations.UpdateOneAsync(i => i.Id == information.Id, update);
        }
    }
}
