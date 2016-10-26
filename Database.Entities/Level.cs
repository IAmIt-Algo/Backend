using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Entities
{
    public class Level
    {
        public string Name { get; set; } // or another unique identificator
        public int? AttemptsCount { get; set; }
        public enum StarsCount
        {
            One = 1,
            Two,
            Three
        }
        public StarsCount? Stars{ get; set; }
        public int? SuccessfulAttemptTime {get; set;}
        public int? SummaryAttemptsTime { get; set; }
    }
}
