using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;
using Database;
using Database.Entities;
using System.Collections.ObjectModel;
using MongoDB.Bson;

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
            int SuccessfulTime = 0;
            //если это его первый уровень и первая попытка
            var userInfo = await _userInfoRepository.GetUserInformationAsync(model.UserId);
            if (userInfo == null)
            {
                int CompletedLevels = 0;
                //если он прошел его
                if (model.Stars != null)
                {
                    await _ratingRepository.CreateRatingItemAsync(new Rating { UserName = model.UserName, StarsCount = (int) model.Stars });
                    SuccessfulTime = model.Time;
                    CompletedLevels = 1;
                }
                await _userInfoRepository.AddUserInformationAsync(
                    new Database.Entities.UserInformation {
                        CompletedLevelsCount = CompletedLevels,
                        UserId = model.UserId,
                        Levels = new Collection<Level>(),
                        Id = ObjectId.GenerateNewId() });
                await _userInfoRepository.AddLevelAsync(
                    model.UserId, new Level {
                        AttemptsCount = 1,
                        Name = model.LevelName,
                        SummaryAttemptsTime = model.Time,
                        SuccessfulAttemptTime = SuccessfulTime,
                        Stars = (Level.StarsCount) model.Stars
                    });
                return;
            }
            var level = await _userInfoRepository.GetLevelAsync(model.UserId, model.LevelName);
            //если он еще не проходил этот уровень
            if (level == null)
            {
                //если уровень пройден
                if (model.Stars != null)
                {
                    await _ratingRepository.IncreaseRatingItemAsync(model.UserName, (int) model.Stars);
                    SuccessfulTime = model.Time;
                }
                await _userInfoRepository.AddLevelAsync(
                    model.UserId, new Level
                    {
                        Name = model.LevelName,
                        AttemptsCount = 1,
                        SummaryAttemptsTime = model.Time,
                        SuccessfulAttemptTime = SuccessfulTime,
                        Stars = (Level.StarsCount) model.Stars
                    });
                return;
            }
            
            if (level.Stars == Level.StarsCount.Three)
            {
                return;
            }
            SuccessfulTime = (int) level.SuccessfulAttemptTime;
            Level.StarsCount? stars = (Level.StarsCount) model.Stars;
            if (level.Stars >= stars)
            {
                stars = level.Stars;
            }
            else
            {
                await _ratingRepository.IncreaseRatingItemAsync(model.UserName, (int) model.Stars - (int) level.Stars); //так точно можно????????
            }
            if (model.Stars == 3)
            {
                SuccessfulTime = model.Time;
            }
            await _userInfoRepository.UpdateLevelAsync(model.UserId,
                new Level { Name = model.LevelName,
                    AttemptsCount = level.AttemptsCount + 1,
                    SuccessfulAttemptTime = SuccessfulTime,
                    SummaryAttemptsTime = level.SummaryAttemptsTime + model.Time,
                    Stars = stars});
        }
    }
}
