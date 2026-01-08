using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TaskTracker.DAL;
using TaskTracker.Models;
namespace TaskTracker.Controllers
{
    public class TaskController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            if (Session["UserId"] == null)
            {
                return RedirectToAction("Login", "Account");
            }

            List<TaskItem> tasks = new List<TaskItem>();
            using (SqlConnection con = DbHelper.GetConnection())
            {
                con.Open();
                string query = "Select TaskId, Title, Description, IsComplete from Tasks where UserId = @UserId";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@UserId", Session["UserId"]);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    tasks.Add(new TaskItem
                    {
                        TaskId = (int)reader["TaskId"],
                        Title = reader["Title"].ToString(),
                        Description = reader["Description"].ToString(),
                        IsComplete = (bool)reader["IsComplete"] 
                    });
                }
            }
            return View(tasks);
        }

        [HttpGet]
        public ActionResult Create()
        {
            if (Session["UserId"] == null)
            {
                return RedirectToAction("Login", "Account");
            }
            return View();
        }

        [HttpPost]
        public ActionResult Create(string Title, string Description)
        {
            if (Session["UserId"] == null)
            {
                return RedirectToAction("Login", "Account");
            }

            if (string.IsNullOrWhiteSpace(Title))
            {
                ViewBag.Error = "Title is required";
                return View();
            }

            using (SqlConnection con = DbHelper.GetConnection())
            {
                con.Open();

                string query =
                    "INSERT INTO Tasks (Title, Description, IsComplete, UserId) VALUES (@Title, @Desc, 0, @UserId)";

                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@Title", Title);
                cmd.Parameters.AddWithValue("@Desc", Description ?? "");
                cmd.Parameters.AddWithValue("@UserId", Session["UserId"]);

                cmd.ExecuteNonQuery();
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            if (Session["UserId"] == null)
                return RedirectToAction("Login", "Account");

            TaskItem task = null;

            using (SqlConnection con = DbHelper.GetConnection())
            {
                con.Open();

                string query =
                    "SELECT TaskId, Title, Description, IsComplete " +
                    "FROM Tasks WHERE TaskId=@Id AND UserId=@UserId";

                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@Id", id);
                cmd.Parameters.AddWithValue("@UserId", Session["UserId"]);

                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    task = new TaskItem
                    {
                        TaskId = (int)reader["TaskId"],
                        Title = reader["Title"].ToString(),
                        Description = reader["Description"].ToString(),
                        IsComplete = (bool)reader["IsComplete"]
                    };
                }
            }

            if (task == null)
                return HttpNotFound();

            return View(task);
        }

        //Edit - post
        [HttpPost]
        public ActionResult Edit(int TaskId,  string Title, string Description, bool ?IsComplete)
        {
            if (Session["UserId"] == null)
                return RedirectToAction("Login", "Account");

            if (string.IsNullOrWhiteSpace(Title))
            {
                ViewBag.Error = "Title required";
                return View();
            }

            bool isCompleteValue = IsComplete ?? false;

            using (SqlConnection con = DbHelper.GetConnection())
            {
                con.Open();

                string query =
                    "UPDATE Tasks SET Title=@Title, Description=@Desc, IsComplete=@IsComplete " +
                    "WHERE TaskId=@Id AND UserId=@UserId";

                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@Title", Title);
                cmd.Parameters.AddWithValue("@Desc", Description ?? "");
                cmd.Parameters.AddWithValue("@IsComplete", isCompleteValue);
                cmd.Parameters.AddWithValue("@Id", TaskId);
                cmd.Parameters.AddWithValue("@UserId", Session["UserId"]);

                cmd.ExecuteNonQuery();
            }

            return RedirectToAction("Index");
        }

        // Delete 
        public ActionResult Delete(int id)
        {
            if (Session["UserId"] == null)
                return RedirectToAction("Login", "Account");

            using (SqlConnection con = DbHelper.GetConnection())
            {
                con.Open();

                string query =
                    "DELETE FROM Tasks WHERE TaskId=@Id AND UserId=@UserId";

                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@Id", id);
                cmd.Parameters.AddWithValue("@UserId", Session["UserId"]);

                cmd.ExecuteNonQuery();
            }

            return RedirectToAction("Index");
        }

    }
}