using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AspNet.Identity.MongoDB;

namespace Backend
{
    public class EnsureAuthIndexes
    {
        public static void Exist()
        {
            var context = ApplicationIdentityContext.Create();
            IndexChecks.EnsureUniqueIndexOnEmail(context.Users);
            IndexChecks.EnsureUniqueIndexOnUserName(context.Users);
        }
    }
}
