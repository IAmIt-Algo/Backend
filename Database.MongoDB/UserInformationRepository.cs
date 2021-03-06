﻿using System;
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
            var levels = (await _informations.FindAsync(i => i.UserId == userId)).FirstOrDefault().Levels;
            var index = levels.IndexOf(levels.FirstOrDefault(levelFunc));
            var update = Builders<UserInformation>.Update.Inc(i => i.Levels[index].AttemptsCount, 1)
                    .Inc(i => i.Levels[index].SummaryAttemptsTime, attempt.AttemptTime);       
            if(attempt.IsSussessful)
            {
             update = update.Set(i => i.Levels[index].SuccessfulAttemptTime, attempt.AttemptTime)
                    .Set(i => i.Levels[index].Stars, attempt.Stars);
            }
            await _informations.UpdateOneAsync(i => i.UserId == userId, update);
        }

        public async Task AddLevelAsync(string userId, Level level)
        {
            var update = Builders<UserInformation>.Update
                .AddToSet(i => i.Levels, level);
            await _informations.UpdateOneAsync(i => i.UserId == userId, update);
        }

        public async Task AddUserInformationAsync(string userId, UserInformation information)
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
            try
            {
                var levels = (await _informations.FindAsync(i => i.UserId == userId)).FirstOrDefault().Levels;
                return levels.FirstOrDefault(l => l.Name == levelName);
            } catch (NullReferenceException e)
            {
                return null;
            }
        }

        public async Task<UserInformation> GetUserInformationAsync(string userId)
        {
            return (await _informations.FindAsync(i => i.UserId == userId)).FirstOrDefault();
        }
    }
}
