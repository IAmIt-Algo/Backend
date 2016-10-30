using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class IncreaseRatingModel
    {
        public int StarsCount { get; set; }
        public string UserName { get; set; }
    }
    public class GetRatingPositionModel
    {
        public long LowestPosition { get; set; }
        public long HighestPosition { get; set; }
    }
}
