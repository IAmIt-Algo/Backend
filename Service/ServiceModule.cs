using Ninject.Modules;
using Service.LevelService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class ServiceModule : NinjectModule
    {
        public override void Load()
        {
            Bind<ILevelService>().To<LevelService.LevelService>();
        }
    }
}
