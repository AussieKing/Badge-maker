using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CatWorx.BadgeMaker
{
  class Program
  {
    //! GET EMPLOYEES
    static List<Employee> GetEmployees()
    {
      List<Employee> employees = new List<Employee>();
      while (true)
      {
        // Move the initial prompt inside the loop, so it repeats for each employee
        Console.WriteLine("Enter first name (leave empty to exit): ");

        // change input to firstName
        string firstName = Console.ReadLine() ?? "";
        if (firstName == "")
        {
          break;
        }

        // add a Console.ReadLine() for each value
        Console.Write("Enter last name: ");
        string lastName = Console.ReadLine() ?? "";
        Console.Write("Enter ID: ");
        int id = Int32.Parse(Console.ReadLine() ?? "");  // using Int32.Parse() to convert string to int
        Console.Write("Enter Photo URL:");
        string photoUrl = Console.ReadLine() ?? "";
        Employee currentEmployee = new Employee(firstName, lastName, id, photoUrl);
        employees.Add(currentEmployee);
      }

      return employees;
    }

    //! PRINT EMPLOYEES
    static void PrintEmployees(List<Employee> employees)
    {
      Util.PrintEmployees(employees);
    }

    //! MAIN
    async static Task Main(string[] args)
    {
      List<Employee> employees = GetEmployees();
      Util.PrintEmployees(employees);
      Util.MakeCSV(employees);
      await Util.MakeBadges(employees);
    }
  }
}
