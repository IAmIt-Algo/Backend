using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class GetRatingPositionModel
    {
        public List<RatingModel> Rating { get; set; }
        public long LowestPosition { get; set; }
        public long HighestPosition { get; set; }
    }

    public class RatingModel
    {
        public string UserName { get; set; }
        public int StarsCount { get; set; }
    }
}
