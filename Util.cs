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
            // Layout the badge
            int BADGE_WIDTH = 669;
            int BADGE_HEIGHT = 1044;

            // Create a new SKBitmap object with the correct dimensions
            int PHOTO_LEFT_X = 184;
            int PHOTO_TOP_Y = 215;
            int PHOTO_RIGHT_X = 486;
            int PHOTO_BOTTOM_Y = 517;

            int COMPANY_NAME_Y = 150;

            int EMPLOYEE_NAME_Y = 600;

            int EMPLOYEE_ID_Y = 730;

            // instance of HttpClient is disposable, so we use the 'using' keyword to create it and dispose of it immediately after the code in the block executes
            using (HttpClient client = new HttpClient())
            {
                for (int i = 0; i < employees.Count; i++)
                {
                    SKImage photo = SKImage.FromEncodedData(await client.GetStreamAsync(employees[i].GetPhotoUrl()));  // await keyword allows us to wait for the GetStreamAsync() method to finish before continuing
                    SKImage background = SKImage.FromEncodedData(File.OpenRead("badge.png"));  // FromEncodedData() method takes a stream as an argument, so we use File.OpenRead() to open the file and pass it to the method

                    SKBitmap badge = new SKBitmap(BADGE_WIDTH, BADGE_HEIGHT);  // create a new SKBitmap object with the correct dimensions
                    SKCanvas canvas = new SKCanvas(badge); // create a new SKCanvas object and pass the SKBitmap object to it

                    canvas.DrawImage(background, new SKRect(0, 0, BADGE_WIDTH, BADGE_HEIGHT));  // draw the background image to the canvas, using the DrawImage() method, and the SKRect object to set the dimensions
                    canvas.DrawImage(photo, new SKRect(PHOTO_LEFT_X, PHOTO_TOP_Y, PHOTO_RIGHT_X, PHOTO_BOTTOM_Y));  // draw the photo to the canvas, using the DrawImage() method, and the SKRect object to set the dimensions

                    SKPaint paint = new SKPaint();
                    paint.TextSize = 42.0f;
                    paint.IsAntialias = true;
                    paint.Color = SKColors.White;
                    paint.IsStroke = false;
                    paint.TextAlign = SKTextAlign.Center;
                    paint.Typeface = SKTypeface.FromFamilyName("Arial");

                    // Company name
                    canvas.DrawText(employees[i].GetCompanyName(), BADGE_WIDTH / 2f, COMPANY_NAME_Y, paint);

                    // Employee name
                    paint.Color = SKColors.Black;
                    canvas.DrawText(employees[i].GetFullName(), BADGE_WIDTH / 2f, EMPLOYEE_NAME_Y, paint); // draw the employee name to the canvas, using the DrawText() method, and the SKRect object to set the dimensions

                    // Employee ID
                    paint.Typeface = SKTypeface.FromFamilyName("Courier New");
                    canvas.DrawText(employees[i].GetId().ToString(), BADGE_WIDTH / 2f, EMPLOYEE_ID_Y, paint); // draw the employee ID to the canvas, using the DrawText() method, and the SKRect object to set the dimensions

                    string badgePath = $"data/{employees[i].GetId()}_badge.png";  // create a string variable to store the path to the badge file
                    using (var fileStream = new FileStream(badgePath, FileMode.Create))  // use the FileStream class in a using block to create a new file and save the badge to it, whilst disposing of the fileStream object immediately after the code in the block executes
                    {
                        badge.Encode(SKEncodedImageFormat.Png, 100).SaveTo(fileStream);  // encode the badge as a PNG, and save it to the fileStream object
                    }
                }
            }
        }
    }
}