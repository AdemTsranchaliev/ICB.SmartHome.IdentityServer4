using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ICB.SmartHome.IdentityServer4.Data
{
    public static class DataBaseConfig
    {
        public const string DevelopmentConnection = "Data Source=(LocalDb)\\MSSQLLocalDB;database=SmartHomeIdentity;trusted_connection=yes;";
        public const string TestConnection = "";
        public const string ProductionConnection = "";
    }
}
