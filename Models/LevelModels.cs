using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class AddAttemptModel
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string LevelName { get; set; }
        public int Time { get; set; }
        public int? Stars { get; set; }
    }
    public class UserInformationModel
    {
        public List<LevelModel> Levels { get; set; }
    }
    public class LevelModel
    {
        public string Name { get; set; }
        public int StarsCount { get; set; }
    }
}
