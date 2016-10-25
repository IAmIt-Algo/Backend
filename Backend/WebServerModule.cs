using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ninject;
using Ninject.Modules;
using Configuration.ConfigurationModule;
using Service;

namespace Backend
{
    public class WebServerModule : NinjectModule
    {
        public override void Load()
        {
            Kernel.Load(new ConfigurationModule());
            Kernel.Load(new ServiceModule());

            Bind<WebServer>().ToSelf();
        }
    }
}
