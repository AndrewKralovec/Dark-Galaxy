using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DarkDarkGalaxy.Models;
using MySql.Data.MySqlClient;
using DarkDarkGalaxy.Background;

namespace DarkDarkGalaxy.Controllers
{

    // Everything will be useing AndrewCousre [ testing out new data base object type] instead of KralovecCourse 
    public class ScheudleController : Controller
    {
        // GET: Scheudle
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult KralovecObjectCheck(AndrewCourse newItem)
        {
            return Json("test", JsonRequestBehavior.AllowGet);

        }

        public ActionResult KralovecSCH(AndrewCourse newItem)
        {
            KralovecBrownScheduler kralovecObject = new KralovecBrownScheduler();

            int room = DatatCount("Room", newItem.Room);
            int teachers = DatatCount("Instructor", newItem.Instructor);
            int courses = DatatCount("Course", newItem.Course);
           

            List<AndrewCourse> rooms = ListBy("Room", newItem.Room);
            List<AndrewCourse> teachers = ListBy("Instructor", newItem.Instructor);
            List<AndrewCourse> courses = ListBy("Course", newItem.Course);

            string test;
            bool check = kralovecObject.OneCourse(room, teachers, courses, newItem);
            if (check)
            {
                test = "True";
            }
            else
            {
                test = "False";
            }
            //string message = "room count: "+room+ "teachers count: " + teachers+ "Courses count: " + courses;
            return Json(test, JsonRequestBehavior.AllowGet);
        }

        // Count
        [HttpPost]
        public int DatatCount(string element, string id)
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

                string stm = "SELECT * FROM Records WHERE " + element + "= '" + id + "' ;";
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
            return (count);
        }

        // Find Records 
        [HttpPost]
        public List<AndrewCourse> ListBy(string element, string id)
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
                /*
                string stm = "SELECT COUNT(*) FROM Records WHERE Room='" + id + "' ;";
                MySqlCommand cmd = new MySqlCommand(stm, conn);
                Int32 count = Convert.ToInt32(cmd.ExecuteScalar());
                */
                string stm = "SELECT * FROM Records WHERE " + element + "= '" + id + "' ;";
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
            return objectList ;
        }

    }
}