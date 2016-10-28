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
        Task IncreaseRatingAsync(IncreaseRatingModel model);
    }
}
