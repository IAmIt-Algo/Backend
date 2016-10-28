using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.MongoDB
{
    public class DatabaseModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IRatingRepository>().To<RatingRepository>();
            Bind<IUserInformationRepository>().To<UserInformationRepository>();
        }
    }
}
