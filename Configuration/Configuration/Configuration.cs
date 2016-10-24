using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Configuration.Configuration
{
    public class Configuration : IConfiguration
    {
        public string Host => "http://localhost:8000";
        public string NameDatabase => "algorithmics";
    }
}
