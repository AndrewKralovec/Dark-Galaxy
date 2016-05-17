using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DarkDarkGalaxy.Models; 
using MySql.Data.MySqlClient;


namespace DarkDarkGalaxy.Controllers
{
    public class DatabaseController : Controller
    {
        // GET: Database
        public ActionResult Index()
        {
            return View();
        }
        // Tester 
        [HttpGet]
        public ActionResult KralovecTest()
        {
            var obj = new[] {
               new { Name = "Alfreds Futterkiste", City = "Berlin", Country = "Germany" },
               new { Name = "Bill Cheng", City = "Carbondale", Country = "USA" }
           };

            Console.WriteLine("Sent GET info ");
            return Json(obj, JsonRequestBehavior.AllowGet);
        }
        // Get DB version 
        [HttpGet]
        public ActionResult KralovecVersion()
        {
            string myVersion;

            string cs = @"server=dbserv.cs.siu.edu;
                    userid=root;
                    password=linux;
                    database=Dark Galaxy";

            MySqlConnection conn = null;

            try
            {
                conn = new MySqlConnection(cs);
                conn.Open();
                myVersion = "MySQL version : "+conn.ServerVersion;

            }
            catch (MySqlException ex)
            {
                myVersion = "Error:" + ex.ToString();

            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }
            }

            return Json(myVersion, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult KralovecMessage(string id)
        {
            AndrewCourse obj  = new AndrewCourse(); 
            string cs = @"server=dbserv.cs.siu.edu;
                    userid=root;
                    password=linux;
                    database=Dark Galaxy";

            MySqlConnection conn = null;
            MySqlDataReader rdr = null;

            try
            {
                conn = new MySqlConnection(cs);
                conn.Open();

                string stm = "SELECT * FROM Records WHERE Course="+id+";";
                MySqlCommand cmd = new MySqlCommand(stm, conn);
                rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    obj = new AndrewCourse
                    {
                        Course = rdr.GetString(0),
                        Section = rdr.GetString(1),
                        Max = rdr.GetString(2),
                        CRN = rdr.GetString(3),
                        Instructor = rdr.GetString(4),
                        Days = rdr.GetString(5),
                        startTime = rdr.GetString(6),
                        endTime = rdr.GetString(7),
                        Room = rdr.GetString(8),
                        Notes = rdr.GetString(9)
                    };
                }

            }
            catch (MySqlException ex)
            {
                System.Diagnostics.Debug.WriteLine("Error: {0}", ex.ToString());

            }
            finally
            {
                if (rdr != null)
                {
                    rdr.Close();
                }

                if (conn != null)
                {
                    conn.Close();
                }

            }
            return Json(new { data = obj }, JsonRequestBehavior.AllowGet);
        }

        // Insert data into Records 
        public ActionResult KralovecInsert(AndrewCourse newItem)
        {
            string[] array = { };
            string message = "Null";
            string cs = @"server=dbserv.cs.siu.edu;
                    userid=root;
                    password=linux;
                    database=Dark Galaxy";

            MySqlConnection conn = null;
            try
            {
                conn = new MySqlConnection(cs);
                conn.Open();

                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "INSERT INTO Records(Course,Section,Max,CRN,Instructor,Days,startTime,endTime,Room,Notes) VALUES(@Course, @Section, @Max, @CRN, @Instructor, @Days, @startTime,@endTime, @Room, @Notes );";
                cmd.Prepare();

                int num = 0;
                cmd.Parameters.AddWithValue("@Course", newItem.Course);
                cmd.Parameters.AddWithValue("@Section", newItem.Section);
                cmd.Parameters.AddWithValue("@Max", newItem.Max);
                cmd.Parameters.AddWithValue("@CRN", newItem.CRN);
                cmd.Parameters.AddWithValue("@Instructor", newItem.Instructor);
                cmd.Parameters.AddWithValue("@Days", newItem.Days);
                cmd.Parameters.AddWithValue("@startTime", newItem.startTime);
                cmd.Parameters.AddWithValue("@endTime", newItem.endTime);
                cmd.Parameters.AddWithValue("@Room", newItem.Room);
                cmd.Parameters.AddWithValue("@Notes", newItem.Notes);
                cmd.ExecuteNonQuery();
                message = "Record added successfully ";

            }
            catch (MySqlException ex)
            {
                message = "Error: " + ex.ToString();
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }
            }
            return Json(message, JsonRequestBehavior.AllowGet);
        }

        // Delete data out of Records 
        [HttpPost]
        public ActionResult KralovecCount()
        {
            int count = 0; 
            string cs = @"server=dbserv.cs.siu.edu;
                    userid=root;
                    password=linux;
                    database=Dark Galaxy";

            MySqlConnection conn = null;
            MySqlDataReader rdr = null;

            try
            {
                conn = new MySqlConnection(cs);
                conn.Open();

                string stm = "SELECT COUNT(*) FROM Records;";
                MySqlCommand cmd = new MySqlCommand(stm, conn);
                count = Convert.ToInt32(cmd.ExecuteScalar());
            }
            catch (MySqlException ex)
            {
                count = 0;

            }
            finally
            {
                if (rdr != null)
                {
                    rdr.Close();
                }

                if (conn != null)
                {
                    conn.Close();
                }

            }
            return Json(count, JsonRequestBehavior.AllowGet);
        }

        // Delete data out of Records 
        [HttpPost]
        public ActionResult KralovecDelete(string id)
        {
            AndrewCourse obj = new AndrewCourse();
            string message = "Null";
            string cs = @"server=dbserv.cs.siu.edu;
                    userid=root;
                    password=linux;
                    database=Dark Galaxy";

            MySqlConnection conn = null;
            MySqlDataReader rdr = null;

            try
            {
                conn = new MySqlConnection(cs);
                conn.Open();

                string stm = "DELETE FROM Records WHERE Course="+id+";";
                MySqlCommand cmd = new MySqlCommand(stm, conn);
                rdr = cmd.ExecuteReader();
                message = "Record deleted successfully ";
            }
            catch (MySqlException ex)
            {
                message = "Error: " + ex.ToString();

            }
            finally
            {
                if (rdr != null)
                {
                    rdr.Close();
                }

                if (conn != null)
                {
                    conn.Close();
                }

            }
            return Json(message, JsonRequestBehavior.AllowGet);
        }

        // Update data in Records 
        [HttpPost]
        public ActionResult KralovecUpdate(AndrewCourse newItem)
        {
            string[] array = { };
            string message = "Null";
            string cs = @"server=dbserv.cs.siu.edu;
                    userid=root;
                    password=linux;
                    database=Dark Galaxy";

            MySqlConnection conn = null;
            try
            {
                conn = new MySqlConnection(cs);
                conn.Open();

                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "UPDATE Records SET Course=@Course, Section=@Section, Max=@Max, CRN=@CRN, Instructor=@Instructor, Days=@Days, startTime=@startTime, endTime=@endTime, Room=@Room, Notes=@Notes WHERE Course="+newItem.Course+ ";";
                cmd.Prepare();

                cmd.Parameters.AddWithValue("@Course", newItem.Course);
                cmd.Parameters.AddWithValue("@Section", newItem.Section);
                cmd.Parameters.AddWithValue("@Max", newItem.Max);
                cmd.Parameters.AddWithValue("@CRN", newItem.CRN);
                cmd.Parameters.AddWithValue("@Instructor", newItem.Instructor);
                cmd.Parameters.AddWithValue("@Days", newItem.Days);
                cmd.Parameters.AddWithValue("@startTime", newItem.startTime);
                cmd.Parameters.AddWithValue("@endTime", newItem.endTime);
                cmd.Parameters.AddWithValue("@Room", newItem.Room);
                cmd.Parameters.AddWithValue("@Notes", newItem.Notes);
                cmd.ExecuteNonQuery();
                message = "Record updated successfully ";

            }
            catch (MySqlException ex)
            {
                message = "Error: " + ex.ToString();
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }
            }
            return Json(message, JsonRequestBehavior.AllowGet);
        }

        // Get all data out of Record
        [HttpGet]
        public ActionResult KralovecSelect()
        {
            var objectList = new List<AndrewCourse>();
            
            string cs = @"server=dbserv.cs.siu.edu;
                    userid=root;
                    password=linux;
                    database=Dark Galaxy";

            MySqlConnection conn = null;
            MySqlDataReader rdr = null;

            try
            {
                conn = new MySqlConnection(cs);
                conn.Open();

                string stm = "SELECT * FROM Records ";
                MySqlCommand cmd = new MySqlCommand(stm, conn);
                rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    objectList.Add(new AndrewCourse
                    {
                        Course = rdr.GetString(0),
                        Section = rdr.GetString(1),
                        Max = rdr.GetString(2),
                        CRN = rdr.GetString(3),
                        Instructor = rdr.GetString(4),
                        Days = rdr.GetString(5),
                        startTime = rdr.GetString(6),
                        endTime = rdr.GetString(7),
                        Room = rdr.GetString(8),
                        Notes = rdr.GetString(9)
                    });
                }

            }
            catch (MySqlException ex)
            {
                System.Diagnostics.Debug.WriteLine("Error: {0}", ex.ToString());

            }
            finally
            {
                if (rdr != null)
                {
                    rdr.Close();
                }

                if (conn != null)
                {
                    conn.Close();
                }

            }
            return Json(objectList, JsonRequestBehavior.AllowGet);
        }

    }
}