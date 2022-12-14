using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceAutomation.DataAccess
{
    public interface IDatabaseSecret
    {
        string GetConnectionString();
    }

    public class DatabaseSecret : IDatabaseSecret
    {
        private string host = "postgresql-86496-0.cloudclusters.net";
        private string port = "11042";
        private string username = "admin";
        private string password = "652431Tim";
        private string database = "TrifectaProduction"; 
        private string minPool = "1";

        public string GetConnectionString()
        {
            return $"Host={host};Port={port};Username={username};Password={password};Database={database};MinPoolSize={minPool}";
        }
    }
}
