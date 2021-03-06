﻿using Models;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.LevelService
{
    public interface ILevelService
    {
        Task AddAttemptAsync(AddAttemptModel model);
        Task<UserInformationModel> GetUserInformationAsync(string userId);
    }
}
