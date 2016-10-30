using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.RatingService
{
    public interface IRatingService
    {
        Task IncreaseRatingAsync(string username, int stars);
        Task<GetRatingPositionModel> GetRatingPositionAsync(string username);
        Task AddRatingItemAsync(string username);
    }
}
