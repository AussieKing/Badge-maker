// Import correct packages
using System;
using System.IO;
using System.Collections.Generic;
using SkiaSharp;
using System.Net.Http;
using System.Threading.Tasks;

namespace CatWorx.BadgeMaker
{
    class Util
    {
        // Add List parameter to method
        //! PRINT EMPLOYEE
        public static void PrintEmployees(List<Employee> employees)
        {
            for (int i = 0; i < employees.Count; i++)
            {
                string template = "{0,-10}\t{1,-20}\t{2}";
                Console.WriteLine(String.Format(template, employees[i].GetId(), employees[i].GetFullName(), employees[i].GetPhotoUrl()));
            }
        }

        //! MAKE CSV
        public static void MakeCSV(List<Employee> employees)
        {
            // Check to see if folder exists
            if (!Directory.Exists("data"))
            {
                // If not, create it
                Directory.CreateDirectory("data");
            }
            // Create a new StreamWriter, and name it "employees.csv", then dispose of it immediately after the code in the block executes
            using (StreamWriter file = new StreamWriter("data/employees.csv"))
            {
                file.WriteLine("ID,Name,PhotoUrl");

                // Loop over employees
                for (int i = 0; i < employees.Count; i++)
                {
                    // Write each employee to the file
                    string template = "{0},{1},{2}";
                    file.WriteLine(String.Format(template, employees[i].GetId(), employees[i].GetFullName(), employees[i].GetPhotoUrl()));
                }
            }

        }
        //! MAKE BADGES
        async public static Task MakeBadges(List<Employee> employees)  // async keyword allows us to use the await keyword inside the method
        {
            // instance of HttpClient is disposable, so we use the 'using' keyword to create it and dispose of it immediately after the code in the block executes
            using(HttpClient client = new HttpClient())
            {
                for (int i=0; i < employees.Count; i++)
                {

                }
            }

            // SKImage newImage = SKImage.FromEncodedData(File.OpenRead("badge.png"));  // FromEncodedData() method takes a stream as an argument, so we use File.OpenRead() to open the file and pass it to the method
            // SKData data = newImage.Encode(); // using the Encode() method to encode the image into a byte array, by default () it encodes it as a PNG
            // data.SaveTo(File.OpenWrite("data/employeeBadge.png"));  // SaveTo() method allows us to save the image to a file, we use File.OpenWrite() to open the file and pass it to the method
        }
    }
}