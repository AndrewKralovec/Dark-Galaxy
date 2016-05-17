using System;
using System.IO;

public class FileUpload
{

    string[] lines = System.IO.File.ReadAllLines(@"rooms.txt");
    // Display the file contents by using a foreach loop.
    System.Console.WriteLine("Contents of rooms.txt = ");
        foreach (var line in lines)
        {
            
			string[] array = line.trim().Split(' ');
            foreach(string word in array)
			{
				if(!word.Contains(" ")){
                      Console.WriteLine ("** {0} **", word);
                        
				}
                 
			} 
            Console.WriteLine ("** \n end line \n **");
        }
        
	}
}
