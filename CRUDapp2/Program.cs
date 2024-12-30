// See https://aka.ms/new-console-template for more information

using CRUDapp2;
using Microsoft.EntityFrameworkCore;
using static System.Net.Mime.MediaTypeNames;

internal class Program
{
    private static void Main(string[] args)
    {
        using var dbContext = new AppDbContext();
        
        while (true)
        {
            Console.WriteLine("What would you like to do?");
            Console.WriteLine("1. Add user to database\n" +
                "2. Read users from database\n" +
                "3. Update users in database\n" +
                "4. Delete use from database\n" +
                "5. Generate random users.");

            switch (Console.ReadKey().Key)
            {
                case ConsoleKey.NumPad1:
                case ConsoleKey.D1:
                    CreateUser(dbContext);
                    break;
                case ConsoleKey.NumPad2:
                case ConsoleKey.D2:
                    ReadUsers(dbContext);
                    break;
                case ConsoleKey.NumPad3:
                case ConsoleKey.D3:
                    UpdateUser(dbContext);
                    break;
                case ConsoleKey.NumPad4:
                case ConsoleKey.D4:
                    DeleteUser(dbContext);
                    break;
                case ConsoleKey.NumPad5:
                case ConsoleKey.D5:
                    GenerateRandomUsers(dbContext);
                    break;
                default:
                    Console.WriteLine("Didn't really work that way, eh?");
                    break;
            }
        }

        static void CreateUser(AppDbContext dbContext)
        {
            Console.WriteLine("Enter name: ");
            string name = Console.ReadLine()!;

            Console.WriteLine("Enter Email: ");
            string email = Console.ReadLine()!;

            Console.WriteLine("Enter Age: ");
            int age = Convert.ToInt32(Console.ReadLine());


            var user = new User { Name = name, Email = email, Age = age};
            dbContext.Users.Add(user);
            dbContext.SaveChanges();
            Console.WriteLine("User added successfully");

            
            

        }

        static void ReadUsers(AppDbContext dbContext)
        {
            var users = dbContext.Users.ToList();
            Console.WriteLine("\nUsers:");
            foreach (var user in users) 
            {
                Console.WriteLine($"ID: {user.Id}, Name: {user.Name} Email: {user.Email} Age: {user.Age}");
            }
        }

        static void UpdateUser(AppDbContext dbContext)
        {
            Console.WriteLine("Enter the Id of the user you want to update: ");
            int id = int.Parse(Console.ReadLine()!);

            var user = dbContext.Users.Find(id);
            if (user == null)
            {
                Console.WriteLine("User not found");
                return;
            }

            Console.WriteLine("Enter a new name: ");
            user.Name = Console.ReadLine()!;

            Console.WriteLine("Enter a new email: ");
            user.Email = Console.ReadLine()!;

            Console.WriteLine("Enter a new age: ");
            user.Age = Convert.ToInt32(Console.ReadLine());

            dbContext.SaveChanges();
            Console.WriteLine("Changes saved successfully");
        }

        static void DeleteUser(AppDbContext dbContext)
        {
            Console.WriteLine("Enter the Id of the user you want to delete");
            int id = int.Parse(Console.ReadLine()!);

            var user = dbContext.Users.Find(id);
            if (user == null)
            {
                Console.WriteLine("User not found");
                return;
            }

            dbContext.Users.Remove(user);
            dbContext.SaveChanges();
            Console.WriteLine("User deleted successfully!");

        }
    }

    private static void GenerateRandomUsers(AppDbContext dbContext)
    {
        Console.WriteLine("How many users would you like to generate?");
        int numOfUsers = int.Parse(Console.ReadLine()!);
        RandomGenerator.GenerateUsers(numOfUsers);

        foreach(var user in RandomGenerator.users)
        {
            dbContext.Users.Add(user);
        }
        dbContext.SaveChanges();
        RandomGenerator.users.Clear();

    }


}