using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend
{
    class Program
    {
        static void Main(string[] args)
        {
            var kernel = new StandardKernel();
            kernel.Load<WebServerModule>();

            var server = kernel.Get<WebServer>();
            server.Start(() => kernel);
            Console.ReadKey();
        }
    }
}
