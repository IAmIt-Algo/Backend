using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.LevelService
{
    public interface ILevelService
    {
        Task AddAttempAsync(AddAttempModel model);
    }
}
