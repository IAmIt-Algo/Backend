using MongoDB.Bson;
using System.Collections.Generic;

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
