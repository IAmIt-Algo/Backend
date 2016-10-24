using Configuration.Configuration;
using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Configuration.ConfigurationModule
{
    public class ConfigurationModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IConfiguration>().To<Configuration.Configuration>();
        }
    }
}
