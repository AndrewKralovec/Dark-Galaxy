using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DarkDarkGalaxy.Models;

namespace DarkDarkGalaxy.Background
{
        class KralovecBrownScheduler { 
            
        public KralovecBrownScheduler()
        {

        }
        // just work with timespan and not have to do all that time date stuff 
        TimeSpan hour = new TimeSpan(1, 0, 0); // define hours length 
        TimeSpan startTime = new TimeSpan(8, 0, 0); // morning
        TimeSpan endTime = new TimeSpan(18, 0, 0); // military
        TimeSpan newCourseTime;
        TimeSpan newCourseEnd;

        bool roomTime = true;//if rooms and time don't match
        bool teacherInvalid = false;//if teacher is unaviable

        // lists are comming from controller, so i dont have so many for loops, check alot of data 
        public bool OneCourse(List<AndrewCourse> rooms, List<AndrewCourse> teacherCourses, List<AndrewCourse> coures, AndrewCourse newCourse)
        {
            // pare time   
            // TimeSpan newCourseTime;
            if (TimeSpan.TryParse(newCourse.startTime, out newCourseTime))
            {
                Console.WriteLine("{0} --> {1}", newCourse.startTime, newCourseTime); // example  '11:00' --> 11:00:00  
            }
            else
            {
                Console.WriteLine("Unable to parse '{0}'", newCourse.startTime);
            }
            if (TimeSpan.TryParse(newCourse.endTime, out newCourseEnd))
            {
                Console.WriteLine("{0} --> {1}", newCourse.endTime, newCourseEnd); // example  '11:00' --> 11:00:00  
            }
            else
            {
                Console.WriteLine("Unable to parse '{0}'", newCourse.endTime);
            }
            // find a room, check if a class is going on 
            foreach (AndrewCourse room in rooms)
            {
                if (newCourse.Room == room.Room)
                {
                    // Parse Times for rooms
                    TimeSpan roomStart;
                    TimeSpan roomEnd;
                    if (TimeSpan.TryParse(room.startTime, out roomStart))
                    {
                        Console.WriteLine("{0} --> {1}", room.startTime, roomStart); // example  '11:00' --> 11:00:00  
                    }
                    if (TimeSpan.TryParse(room.endTime, out roomEnd))
                    {
                        Console.WriteLine("{0} --> {1}", room.endTime, roomEnd); // example  '11:00' --> 11:00:00  
                    }

                    if (newCourseTime >= roomStart && newCourseTime <= roomEnd)
                    {
                        // ROOM AND TIME IS INCORRECT
                        Console.WriteLine("ROOM AND TIME IS WRONG");
                        roomTime = false;
                        break;
                    }
                    else
                    {
                        Console.WriteLine("ROOM AND TIME IS CORRECT");
                    }
                }
                else
                {
                    // Room not found 
                    Console.WriteLine("ROOM NOT FOUND");
                }
            }
            if (roomTime)
            {
                foreach (AndrewCourse teacher in teacherCourses)
                {
                    //Parse Time of teacher
                    TimeSpan teacherTime;
                    if (TimeSpan.TryParse(teacher.startTime, out teacherTime))
                    {
                        Console.WriteLine("{0} --> {1}", teacher.startTime, teacherTime); // example  '11:00' --> 11:00:00  
                    }
                    else
                    {
                        // fix later 
                        Console.WriteLine("Unable to parse '{0}'", teacher.startTime);
                    }
                    if (teacherTime >= newCourseTime && teacherTime <= newCourseEnd)
                    {
                        // TEACHER IS INCORRECT
                        Console.WriteLine("teacher time works, teacherTime: {0}, newCourse time: {1} newCourseEnd: {2} ", teacherTime,newCourseTime, newCourseEnd);
                        teacherInvalid = false;
                        break;
                    }
                    else
                    {
                        // teacher is wrong 
                        Console.WriteLine("teacher does not work, teacherTime: {0}, newCourse time: {1} newCourseEnd: {2} ", teacherTime, newCourseTime, newCourseEnd);
                    }
                }
            }
            Console.Read();//to wait at end of test
            if (!teacherInvalid && roomTime)
            {
                // values are going to be set to default in js due to lack of time, i just dont have time to mess with the required fields to fix null errros 
                /*
                var obj = new AndrewCourse
                {
                    Course = newCourse.Course,
                    Section = newCourse.Section,
                    Max = newCourse.Max,
                    CRN = newCourse.CRN,
                    Instructor = newCourse.Instructor,
                    Days = newCourse.Days,
                    startTime = newCourse.startTime,
                    endTime = newCourse.endTime,
                    Room = newCourse.Room,
                    Notes = newCourse.Notes
                };
                */

                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
