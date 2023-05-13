using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventOrganizator.Persistence.Configuration
{
    static class DbConfiguration
    {
        static public string ConnectionString
        {
            get
            {
                ConfigurationManager configurationManager = new();
                try
                {
                    var currentDirectory = Directory.GetCurrentDirectory();
                    configurationManager.SetBasePath(currentDirectory);
                    configurationManager.AddJsonFile("appsettings.json");
                }
                catch
                {
                    throw new NotImplementedException();
                }

                return configurationManager.GetConnectionString("MsSql");
            }
        }
    }
}
