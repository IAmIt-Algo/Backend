﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;
using Database;
using Database.Entities;
using System.Collections.ObjectModel;
using MongoDB.Bson;
using static Database.Entities.Level;

namespace Service.LevelService
{
    public class LevelService : ILevelService
    {
        private IUserInformationRepository _userInfoRepository;
        private IRatingRepository _ratingRepository;

        public LevelService(IUserInformationRepository userInfoRepository, IRatingRepository ratingRepository)
        {
            _userInfoRepository = userInfoRepository;
            _ratingRepository = ratingRepository;
        }
        public async Task AddAttemptAsync(AddAttemptModel model)
        {
            var userInfo = await _userInfoRepository.GetUserInformationAsync(model.UserId);
            var level = await _userInfoRepository.GetLevelAsync(model.UserId, model.LevelName);
            var newlevel = new Level
            {
                AttemptsCount = 0,
                Name = model.LevelName,
                Stars = 0,
                SuccessfulAttemptTime = 0,
                SummaryAttemptsTime =  0
            };
            if (userInfo == null)
            {
                var newInfo = new UserInformation
                {
                    CompletedLevelsCount = 0,
                    Id = ObjectId.GenerateNewId(),
                    Levels = new List<Level>(),
                    UserId = model.UserId
                };
                await _userInfoRepository.AddUserInformationAsync(model.UserId, newInfo);
                level = newlevel;
                await _userInfoRepository.AddLevelAsync(model.UserId, level);
            } else if(level == null)
            {
                level = newlevel;
                await _userInfoRepository.AddLevelAsync(model.UserId, level);
            }
            var isSuccessful = (int)level.Stars <= model.Stars && level.SuccessfulAttemptTime >= model.Time;
            if (isSuccessful)
            {
                await _ratingRepository.IncreaseRatingItemAsync(model.UserName, (int)model.Stars - (int)level.Stars);
            }
            await _userInfoRepository.AddAttemptAsync(model.UserId, new Attempt
            {
                AttemptTime = model.Time,
                LevelName = model.LevelName,
                Stars = (StarsCount) model.Stars
            });
        }
    }
}
