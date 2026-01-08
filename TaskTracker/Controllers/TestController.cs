using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TaskTracker.DAL;

namespace TaskTracker.Controllers
{
    public class TestController : Controller
    {
        // GET: Test
        public ActionResult Index()
        {
            using (SqlConnection con = DbHelper.GetConnection())
            {
                string query = "Select count(*) from Users";
                SqlCommand cmd = new SqlCommand(query, con);
                con.Open();
                int count = (int)cmd.ExecuteScalar();
                return Content("User Count:" + count);
            }
        }
    }
}