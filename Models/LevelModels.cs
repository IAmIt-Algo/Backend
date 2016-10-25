using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class AddAttempModel
    {
        ObjectId UserId { get; set; }
        string LevelName { get; set; }
        string Time { get; set; }
        int stars { get; set; }
    }
}
