using System;
using System.IO;

namespace DarkDarkGalaxy
{
    public class Read
{
        public static void Main()
        {
            string[] lines = System.IO.File.ReadAllLines(@"rooms.txt");
    // Display the file contents by using a foreach loop.
    Console.WriteLine("Contents of rooms.txt = ");
            Console.WriteLine();
        foreach (var line in lines)
        {
            
			string[] array = line.Trim().Split(' ');

            foreach(string word in array)
			{
                    if (!word.Contains(" "))
                    {
                        Console.WriteLine("** {0} **", word);
                    }           
			} 
            System.Console.WriteLine ("** \n end line \n **");
        }
        
	}
}

    }

