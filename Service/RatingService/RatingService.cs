using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;
using Database;
using Database.Entities;
using MongoDB.Bson;

namespace Service.RatingService
{
    public class RatingService : IRatingService
    {
        private IRatingRepository _ratingRepository;
        public RatingService(IRatingRepository ratingRepository)
        {
            _ratingRepository = ratingRepository;
        }

        public async Task AddRatingItemAsync(string username)
        {
            var rating = new Rating
            {
                Id = ObjectId.GenerateNewId(),
                StarsCount = 0,
                UserName = username
            };
            await _ratingRepository.AddRatingItemAsync(rating);
        }

        public async Task<GetRatingPositionModel> GetRatingPositionAsync(string username)
        {
            var starsCount = (await _ratingRepository.GetRatingItemAsync(username)).StarsCount;
            var biggerRatingCount = await _ratingRepository.CountUsersByConditionAsync(r => r.StarsCount > starsCount);
            var equalRatingCount = await _ratingRepository.CountUsersByConditionAsync(r => r.StarsCount == starsCount);
            var model = new GetRatingPositionModel
            {
                HighestPosition = biggerRatingCount + 1,
                LowestPosition = biggerRatingCount + equalRatingCount
            };
            return model;
        }

        public async Task IncreaseRatingAsync(string username, int stars)
        {
            if(await _ratingRepository.GetRatingItemAsync(username) == null)
            {
                var raitingItem = new Rating
                {
                    UserName = username,
                    StarsCount = stars
                };
                await _ratingRepository.AddRatingItemAsync(raitingItem);
            } 
            await _ratingRepository.IncreaseRatingItemAsync(username, stars);
        }
    }
}
