using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;

namespace DarkDarkGalaxy.Background
{
    public class KralovecReadFileToSQL
    {

        public static void Main()
        {

            bool inLoop = false;
            string[] lines = System.IO.File.ReadAllLines(@"rooms.txt");
            // Display the file contents by using a foreach loop.
            System.Console.WriteLine("Contents of rooms.txt = ");
            foreach (string line in lines)
            {

                string collect = null;
                string[] array = line.Split(' ');
                int counter = 0;
                string[] commandArray = new string[9];

                foreach (string word in array)
                {
                    if (checkLine(word) == 1)
                    {
                        inLoop = true;
                        commandArray[counter++] = word;
                        continue;
                    }
                    if (inLoop)
                    {
                        if (checkLine(word) == 2)
                        {
                            inLoop = false;
                            collect += word.Trim(); // Trim not removing space afterword, Can fix with checking in the switch
                            commandArray[counter++] = collect;
                            collect = null;
                        }
                        else
                        {
                            collect += word;
                            continue;
                        }
                    }
                    else
                    {
                        commandArray[counter++] = word;
                    }

                } // End foreach word

                // Check if command Array is working
                for (int i = 0; i < commandArray.Length; i++)
                {
                    Console.WriteLine("\n** Array[{0}]: {1}**\n", i, commandArray[i]);
                }

                if (insertCommand(commandArray) <= 0)
                {
                    Console.WriteLine("Data has been inserted");
                }
                else
                {
                    Console.WriteLine("Did not work");
                }
            }// End foreach line 

        }// End main method

        static int checkLine(string word)
        {
            switch (word)
            {
                case "M":
                case "MW":
                case "MWF":
                case "T":
                case "TTR":
                case "TR":
                case "R":
                    return 1;
                case "a.m.":
                case "p.m.":
                    return 2;
                default:
                    return 0;
            }
        } // End method 

        static int insertCommand(string[] array)
        {
            if (array.Length != 9)
            {
                Console.WriteLine("Array count error");
                return -1;
            }
            string cs = @"server=localhost;
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
                cmd.CommandText = "INSERT INTO Records(Course,Section,Max ,CRN ,Instructor ,Days ,Time,Room,Notes) VALUES(@Course, @Section, @Max, @CRN, @Instructor, @Days, @Time, @Room, @Notes )";
                cmd.Prepare();

                int num = 0;
                cmd.Parameters.AddWithValue("@Course", array[0]);
                cmd.Parameters.AddWithValue("@Section", array[1]);
                cmd.Parameters.AddWithValue("@Max", array[2]);
                cmd.Parameters.AddWithValue("@CRN", array[3]);
                cmd.Parameters.AddWithValue("@Instructor", array[4]);
                cmd.Parameters.AddWithValue("@Days", array[5]);
                cmd.Parameters.AddWithValue("@Time", array[6]);
                cmd.Parameters.AddWithValue("@Room", array[7]);
                cmd.Parameters.AddWithValue("@Notes", array[8]);
                cmd.ExecuteNonQuery();
                return 0;


            }
            catch (MySqlException ex)
            {
                Console.WriteLine("Error: {0}", ex.ToString());
                return -1;

            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }
            }

        }// End insert 
    }
}