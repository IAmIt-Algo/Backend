using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Driver;
using Models;
using Ninject;

namespace Backend
{
    public class ApplicationIdentityContext : IDisposable
    {
        public static ApplicationIdentityContext Create()
        {
            var client = new MongoClient();
            var database = client.GetDatabase("algorithmics");
            var users = database.GetCollection<ApplicationUser>("users");
            return new ApplicationIdentityContext(users);
        }

        private ApplicationIdentityContext(IMongoCollection<ApplicationUser> users)
        {
            Users = users;
        }

        public IMongoCollection<ApplicationUser> Users { get; set; }

        public void Dispose()
        {
        }
    }
}
