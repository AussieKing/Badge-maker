using System;
using System.Collections.Generic;

namespace CatWorx.BadgeMaker
{
    class Employee
    {
        private string FirstName;
        private string LastName;
        private int Id;
        private string PhotoUrl;

        // Constructor with two parameters
        public Employee(string firstName, string lastName, int id, string photoUrl)
        {
            FirstName = firstName;
            LastName = lastName;
            Id = id;
            PhotoUrl = photoUrl;
        }

        //! GET FULL NAME
        public string GetFullName()
        {
            return FirstName + " " + LastName;
        }

        //! GET ID
        public int GetId()
        {
            return Id;
        }

        //! GET PHOTO URL
        public string GetPhotoUrl()
        {
            return PhotoUrl;
        }
    }
}
