using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;
using Database;
using Database.Entities;

namespace Service.RatingService
{
    public class RatingService : IRatingService
    {
        private IRatingRepository _ratingRepository;
        public RatingService(IRatingRepository ratingRepository)
        {
            _ratingRepository = ratingRepository;
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
                await _ratingRepository.CreateRatingItemAsync(raitingItem);
            } 
            await _ratingRepository.IncreaseRatingItemAsync(username, stars);
        }
    }
}
