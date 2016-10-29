using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Entities
{
    public class Rating
    {
        public ObjectId Id { get; set; }
        public string UserName { get; set; }
        public int StarsCount { get; set; }
    }
}
