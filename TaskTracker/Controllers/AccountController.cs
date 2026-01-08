using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TaskTracker.DAL;

namespace TaskTracker.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        public ActionResult Register()
        {
            return View();
        }

        // Post: Account
        [HttpPost]
        public ActionResult Register(string Username, string Email, string Password) {
            if (string.IsNullOrWhiteSpace(Username) ||
                string.IsNullOrWhiteSpace(Email) ||
                string.IsNullOrWhiteSpace(Password))
            {
                ViewBag.Error = "All fields are required.";
                return View();
            }

            using (SqlConnection con = DbHelper.GetConnection())
            {
                con.Open();

                //check duplicate emaila and username
                string checkQuery = "select count(*) from Users where Email=@Email or UserName=@Username";
                SqlCommand CheckCmd = new SqlCommand(checkQuery, con);
                CheckCmd.Parameters.AddWithValue("@Email", Email);
                CheckCmd.Parameters.AddWithValue("@Username", Username);

                int exists = (int)CheckCmd.ExecuteScalar();
                if(exists > 0)
                {
                    ViewBag.Error = "Email or Username Already exists";
                    return View();
                }

                //Insert query
                string query = "Insert into Users (UserName, Email, PasswordHash) values (@Username, @Email, @Password)";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@Username", Username);
                cmd.Parameters.AddWithValue("@Email", Email);
                cmd.Parameters.AddWithValue("@Password", Password);

                cmd.ExecuteNonQuery();
            }
            return RedirectToAction("Login");
        }

        // GET: Account
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        // Post: Account
        [HttpPost]
        public ActionResult Login(string Email, string Password)
        {
            using (SqlConnection con = DbHelper.GetConnection()) { 
                con.Open();
                string query = "Select UserId, UserName from Users where Email=@Email and PasswordHash=@password";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@Email", Email);
                cmd.Parameters.AddWithValue("@password", Password);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read()) {
                    Session["UserId"] = reader["UserId"];
                    Session["UserName"] = reader["UserName"];
                    return RedirectToAction("Index", "Task");
                }
            }
            ViewBag.Error = "Invalid Login Attempt";
            return View();

        }

        //logout
        public ActionResult Logout() {
            Session.Clear();
            return RedirectToAction("Login");
        }
    }
}