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
            var rating = await _ratingRepository.GetRatingAsync();
            rating.Sort(Compare);
            int PlayersCount = 50;
            if (rating.Count < PlayersCount)
                PlayersCount = rating.Count;
            rating = rating.GetRange(0, PlayersCount);
            var starsCount = (await _ratingRepository.GetRatingItemAsync(username)).StarsCount;
            var biggerRatingCount = await _ratingRepository.CountUsersByConditionAsync(r => r.StarsCount > starsCount);
            var equalRatingCount = await _ratingRepository.CountUsersByConditionAsync(r => r.StarsCount == starsCount);
            var model = new GetRatingPositionModel
            {
                HighestPosition = biggerRatingCount + 1,
                LowestPosition = biggerRatingCount + equalRatingCount,
                Rating = rating.Select(r => new RatingModel { UserName = r.UserName, StarsCount = r.StarsCount }).ToList()
            };
            return model;
        }

        public int Compare(Rating a, Rating b)
        {
            return b.StarsCount - a.StarsCount;
        }
    }
}
