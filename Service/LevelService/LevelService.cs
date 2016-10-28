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

        public LevelService(IUserInformationRepository userInfoRepository)
        {
            _userInfoRepository = userInfoRepository;
        }

        public async Task AddAttempAsync(AddAttempModel model)
        {
            int SuccessfulTime = 0;
            //если это его первый уровень и первая попытка
            var userInfo = await _userInfoRepository.GetUserInformationAsync(model.UserId);
            if (userInfo == null)
            {
                int CompletedLevels = 0;
                //если он прошел его
                if (model.stars != null)
                {
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
                        Stars = (Level.StarsCount) model.stars
                    });
                return;
            }
            var level = await _userInfoRepository.GetLevelAsync(model.UserId, model.LevelName);
            //если он еще не проходил этот уровень
            if (level == null)
            {
                //если уровень пройден
                if (model.stars != null)
                {

                    SuccessfulTime = model.Time;
                }
                await _userInfoRepository.AddLevelAsync(
                    model.UserId, new Level
                    {
                        Name = model.LevelName,
                        AttemptsCount = 1,
                        SummaryAttemptsTime = model.Time,
                        SuccessfulAttemptTime = SuccessfulTime,
                        Stars = (Level.StarsCount) model.stars
                    });
                return;
            }
            
            if (level.Stars == Level.StarsCount.Three)
            {
                return;
            }
            SuccessfulTime = (int) level.SuccessfulAttemptTime;
            Level.StarsCount? stars = (Level.StarsCount) model.stars;
            if (level.Stars >= stars)
            {
                stars = level.Stars;
            }
            if (model.stars == 3)
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
