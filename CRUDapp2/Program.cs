// See https://aka.ms/new-console-template for more information

using CRUDapp2;
using Microsoft.EntityFrameworkCore;
using System.Numerics;
using static System.Net.Mime.MediaTypeNames;

internal class Program
{
    private static void Main(string[] args)
    {
        using var dbContext = new AppDbContext();
        
        while (true) //infiniteloop so we dont get out of program after every action
        {
            Console.WriteLine("What would you like to do?");
            Console.WriteLine("1. Add user to database\n" +
                "2. Read users from database\n" +
                "3. Update users in database\n" +
                "4. Delete user from database\n" +
                "5. Generate random users.\n" +
                "6. Add a phone\n" +
                "7. Read phones from database\n" +
                "8. Update phones in database\n" +
                "9. Delete phone from database\n" +
                "0. Generate random phones");

            switch (Console.ReadKey().Key) //fallthrough switches that lets user input numbers with numpad or the regular numbers.
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
                case ConsoleKey.NumPad6:
                case ConsoleKey.D6:
                    CreatePhone(dbContext);
                    break;
                case ConsoleKey.NumPad7:
                case ConsoleKey.D7:
                    ReadPhones(dbContext);
                    break;
                case ConsoleKey.NumPad8:
                case ConsoleKey.D8:
                    UpdatePhone(dbContext);
                    break;
                case ConsoleKey.NumPad9:
                case ConsoleKey.D9:
                    DeletePhone(dbContext);
                    break;
                case ConsoleKey.NumPad0:
                case ConsoleKey.D0:
                    GenerateRandomPhones(dbContext);
                    break;
                default:
                    Console.WriteLine("Input not recognized");
                    break;
            }
        }
    }
    static void CreateUser(AppDbContext dbContext) //function that lets the user add a custom user to the table.
    {
        Console.WriteLine("Enter name: ");
        string name = Console.ReadLine()!;

        Console.WriteLine("Enter Email: ");
        string email = Console.ReadLine()!;

        Console.WriteLine("Enter Age: ");
        int age = Convert.ToInt32(Console.ReadLine());


        var user = new User { Name = name, Email = email, Age = age };
        dbContext.Users.Add(user);
        dbContext.SaveChanges();
        Console.WriteLine("User added successfully");




    }

    static void ReadUsers(AppDbContext dbContext) //function that prints the users in the table
    {
        var users = dbContext.Users.ToList();
        Console.WriteLine("\nUsers:");
        foreach (var user in users)
        {
            Console.WriteLine($"ID: {user.Id}, Name: {user.Name} Email: {user.Email} Age: {user.Age}");
        }
    }

    static void UpdateUser(AppDbContext dbContext) //function that updates a specific user in the table
    {
        Console.WriteLine("Enter the Id of the user you want to update: ");
        int id = int.Parse(Console.ReadLine()!);

        var user = dbContext.Users.Find(id);
        if (user == null) //if-check to see that the user exists. If the user does not exists, returns early
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

    static void DeleteUser(AppDbContext dbContext) //function to delete user from table
    {
        Console.WriteLine("Enter the Id of the user you want to delete");
        int id = int.Parse(Console.ReadLine()!);

        var user = dbContext.Users.Find(id);
        if (user == null) //checking that user exists
        {
            Console.WriteLine("User not found");
            return;
        }

        dbContext.Users.Remove(user);
        dbContext.SaveChanges();
        Console.WriteLine("User deleted successfully!");

    }

    private static void GenerateRandomUsers(AppDbContext dbContext) //generates amount of users that gets input, puts them in table
    {
        Console.WriteLine("How many users would you like to generate?");
        int numOfUsers = int.Parse(Console.ReadLine()!);
        RandomGenerator.GenerateUsers(numOfUsers);

        foreach(var user in RandomGenerator.users)
        {
            dbContext.Users.Add(user);
        }
        dbContext.SaveChanges();
        RandomGenerator.users.Clear(); //clears the list of users to make sure they dont get duplicated

    }
    //Phones below

    static void CreatePhone(AppDbContext dbContext)
    {
        Console.WriteLine("Is the phone an Iphone or Samsung?\n" +
            "1. Iphone\n" +
            "2. Samsung");
        int phonetype_Id = Convert.ToInt32(Console.ReadLine()!);

        if (phonetype_Id <= 0 || phonetype_Id <= 3) //returns if phonetype doesnt exist
        {
            Console.WriteLine("phonetype not found");
            return;
        }

        Console.WriteLine("Enter the ID of the user you wanna connect the phone to: ");
        int user_Id = Convert.ToInt32(Console.ReadLine()!);
        var user = dbContext.Users.Find(user_Id);
        if (user == null)  //returns if user ID doesnt exist
        {
            Console.WriteLine("User not found");
            return;
        }

        Console.WriteLine("Enter the phonenumber: ");
        long phonenumber = Convert.ToInt64(Console.ReadLine());


        var phone = new Phone { Phonetype_Id = phonetype_Id, User_Id = user_Id, Phonenumber = phonenumber }; //creates new phone to add
        dbContext.Phone.Add(phone);
        dbContext.SaveChanges();
        Console.WriteLine("Phone added successfully");




    }

    static void ReadPhones(AppDbContext dbContext) //function that prints the phones in table
    {
        var phones = dbContext.Phone.ToList();
        Console.WriteLine("\nPhones:");
        foreach (var phone in phones)
        {
            Console.WriteLine($"ID: {phone.Id}, Type: {phone.Phonetype_Id} User: {phone.User_Id} Phonenumber: {phone.Phonenumber}");
        }
    }

    static void UpdatePhone(AppDbContext dbContext) //function to update a phone in the table
    {
        Console.WriteLine("Enter the Id of the phone you want to update: ");
        int id = int.Parse(Console.ReadLine()!);

        var phone = dbContext.Phone.Find(id);
        if (phone == null)
        {
            Console.WriteLine("Phone not found");
            return;
        }

        Console.WriteLine("Enter a new phonetype: ");
        int phonetype_Id = Convert.ToInt32(Console.ReadLine()!);
        if (phonetype_Id <= 0 || phonetype_Id <= 3) //returns if phonetype doesnt exist
        {
            Console.WriteLine("phonetype not found");
            return;
        }
        phone.Phonetype_Id = phonetype_Id;

        Console.WriteLine("Enter a new User ID: ");
        int user_Id = Convert.ToInt32(Console.ReadLine()!);
        var user = dbContext.Users.Find(user_Id);
        if (user == null) //returns if user doesnt exist
        {
            Console.WriteLine("User not found");
            return;
        }
        phone.User_Id = user_Id;

        Console.WriteLine("Enter a new phonenumber: ");
        phone.Phonenumber = Convert.ToInt64(Console.ReadLine());
        

        dbContext.SaveChanges();
        Console.WriteLine("Changes saved successfully");
    }

    static void DeletePhone(AppDbContext dbContext) //function to delete phone
    {
        Console.WriteLine("Enter the ID of the phone you want to delete");
        int id = int.Parse(Console.ReadLine()!);

        var phone = dbContext.Phone.Find(id);
        if (phone == null) //returns if phone doesnt exist
        {
            Console.WriteLine("Phone not found");
            return;
        }

        dbContext.Phone.Remove(phone);
        dbContext.SaveChanges();
        Console.WriteLine("Phone deleted successfully!");

    }
    private static void GenerateRandomPhones(AppDbContext dbContext) //generates random phones based on inputnumber
    {
        GetUserId(dbContext);
        Console.WriteLine("How many users would you like to generate?");
        int numOfPhones = int.Parse(Console.ReadLine()!);
        RandomGenerator.GeneratePhones(numOfPhones);

        foreach (var phone in RandomGenerator.phones)
        {
            dbContext.Phone.Add(phone);
        }
        dbContext.SaveChanges();
        RandomGenerator.phones.Clear(); //clears list so duplicates dont get put in if done several times
        RandomGenerator.user_Ids.Clear();

    }

    public static void GetUserId(AppDbContext dbContext) //function to get userId's for randomizing phones.
    {
        var users = dbContext.Users.ToList();
        foreach (var user in users)
        {
            RandomGenerator.user_Ids.Add(user.Id);
        }
    }
}
