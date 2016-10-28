using Database.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database
{
    public interface IRatingRepository
    {
        Task CreateRatingItemAsync(Rating rating);
        Task IncreaseRatingItemAsync(string userName, int increaseAmount);
        Task<Rating> GetRatingItemAsync(string userName);
    }
}
