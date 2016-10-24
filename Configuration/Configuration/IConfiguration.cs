using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Configuration.Configuration
{
    public interface IConfiguration
    {
        string Host { get; }
        string NameDatabase { get; }
    }
}
