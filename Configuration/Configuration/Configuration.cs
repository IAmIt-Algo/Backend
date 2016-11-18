using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Configuration.Configuration
{
    public class Configuration : IConfiguration
    {
        public string Host1 => "http://ec2-184-72-112-237.compute-1.amazonaws.com";
        public string Host => "http://localhost:8000";
        public string NameDatabase => "algorithmics";
    }
}
