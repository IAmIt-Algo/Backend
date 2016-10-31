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
        Task<List<Rating>> GetRatingAsync();
        Task AddRatingItemAsync(Rating rating);
        Task IncreaseRatingItemAsync(string userName, int increaseAmount);
        Task<Rating> GetRatingItemAsync(string userName);
        Task<long> CountUsersByConditionAsync(System.Linq.Expressions.Expression<Func<Rating, bool>> filter);
    }
}
