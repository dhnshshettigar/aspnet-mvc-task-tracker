using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace TaskTracker.DAL
{
    public class DbHelper
    {
        public static SqlConnection GetConnection()
        {
            string cs = ConfigurationManager
                        .ConnectionStrings["TaskTrackerDb"]
                        .ConnectionString;

            return new SqlConnection(cs);
        }
    }
}