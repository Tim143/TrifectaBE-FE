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
        //private string host = "postgresql-86496-0.cloudclusters.net";
        //private string port = "11042";
        //private string username = "admin";
        //private string password = "652431Tim";
        //private string database = "TrifectaProd"; //TrifectaProd - prod DB Name
        //private string minPool = "1";

        private string host = "localhost";
        private string port = "5432";
        private string username = "trifecta_db_usr";
        private string password = "6z8GIKue2XyDwLUs";
        private string database = "trifecta_db"; //TrifectaProd - prod DB Name
        private string minPool = "1";

        public string GetConnectionString()
        {
            return $"Host={host};Port={port};Username={username};Password={password};Database={database};MinPoolSize={minPool}";
        }
    }
}
