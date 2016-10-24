using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Entities
{
    public class UserInformation
    {
        public ObjectId Id { get; set; }
        public string UserId { get; set; }
        public int CompletedLevelsCount { get; set; }
        public ICollection<Level> Levels { get; set; }
    }
}
