﻿using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class AddAttempModel
    {
        public string UserId { get; set; }
        public string LevelName { get; set; }
        public int Time { get; set; }
        public int stars { get; set; }
    }
}
