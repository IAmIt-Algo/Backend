using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Database.Entities;
using Configuration.Configuration;
using MongoDB.Driver;
using MongoDB.Bson;

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

        public async Task AddAttemptAsync(string userId, Attempt attempt)
        {
            Func<Level,bool> levelFunc = l => l.Name == attempt.LevelName;
            var level = await GetLevelAsync(userId, attempt.LevelName);
            var update = Builders<UserInformation>.Update.Inc(i => i.Levels.FirstOrDefault(levelFunc).AttemptsCount, 1)
                    .Inc(i => i.Levels.FirstOrDefault(levelFunc).SummaryAttemptsTime, attempt.AttemptTime);       
            if(level.Stars <= attempt.Stars && level.SuccessfulAttemptTime >= attempt.AttemptTime)
            {
                var success = Builders<UserInformation>.Update.Set(i => i.Levels.FirstOrDefault(levelFunc).SuccessfulAttemptTime, attempt.AttemptTime)
                    .Set(i => i.Levels.FirstOrDefault(levelFunc).Stars, attempt.Stars);
                await _informations.UpdateOneAsync(i => i.UserId == userId, success);
            }
            await _informations.UpdateOneAsync(i => i.UserId == userId, update);
        }

        public async Task<Level> AddLevelAsync(string userId, string levelName)
        {
            var level = new Level
            {
                Name = levelName,
                AttemptsCount = 0,
                Stars = Level.StarsCount.Zero,
                SuccessfulAttemptTime = 0,
                SummaryAttemptsTime = 0
            };
            var update = Builders<UserInformation>.Update
                .AddToSet(i => i.Levels, level);
            await _informations.UpdateOneAsync(i => i.UserId == userId, update);
            return level;
        }

        public async Task AddUserInformationAsync(string userId)
        {
            var information = new UserInformation
            {
                Id = ObjectId.GenerateNewId(),
                UserId = userId,
                CompletedLevelsCount = 0,
                Levels = new List<Level>()
            };
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


        public async Task UpdateUserInformationAsync(UserInformation information)
        {
            var update = Builders<UserInformation>.Update
                .Set(i => i.CompletedLevelsCount, information.CompletedLevelsCount);
            await _informations.UpdateOneAsync(i => i.Id == information.Id, update);
        }
    }
}
