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
        public async Task IncreaseRatingAsync(IncreaseRatingModel model)
        {
            if(await _ratingRepository.GetRatingItemAsync(model.UserName) == null)
            {
                var raitingItem = new Rating
                {
                    UserName = model.UserName,
                    StarsCount = model.StarsCount
                };
                await _ratingRepository.CreateRatingItemAsync(raitingItem);
            } 
            await _ratingRepository.IncreaseRatingItemAsync(model.UserName, model.StarsCount);
        }
    }
}
