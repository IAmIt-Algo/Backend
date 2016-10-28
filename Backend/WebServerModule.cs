using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ninject;
using Ninject.Modules;
using Configuration.ConfigurationModule;
using Service;
using Database.MongoDB;

namespace Backend
{
    public class WebServerModule : NinjectModule
    {
        public override void Load()
        {
            Kernel.Load<ServiceModule>();
            Kernel.Load<ConfigurationModule>();
            Kernel.Load<DatabaseModule>();

            Bind<WebServer>().ToSelf();
        }
    }
}
