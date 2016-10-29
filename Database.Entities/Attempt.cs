using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Database.Entities.Level;

namespace Database.Entities
{
    public class Attempt
    {
        public bool IsSussessful { get; set; }
        public string LevelName { get; set;}
        public StarsCount Stars { get; set; }
        public int AttemptTime { get; set; }
    }
}
