using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUDapp2
{
    internal class RandomGenerator
    {
        public static Random random = new Random();
        //list to be able to save the userdata.
        public static List<User> users = new List<User>();
        //list to save the phones
        public static List<Phone> phones = new List<Phone>();
        //list for saving user ID's
        public static List<int> user_Ids = new List<int>();


    public static void GenerateUsers(int usersNumber) //function that generates users based on the amount that the user inputs.
        {
            for (int i = 0; i < usersNumber; i++)
            {
                string firstName = firstNames[random.Next(firstNames.Count)]; //takes random places from email, first- and lastname lists. Combines first and lastname for name and also randomizes an age
                string lastName = lastNames[random.Next(lastNames.Count)];
                string name = $"{firstName} {lastName}";
                int age = random.Next(15, 90);
                string email = emails[random.Next(emails.Count)];


                User user = new User();
                user.Name = name;
                user.Age = age;
                user.Email = email;

                users.Add(user);

            }
        }
        public static void GeneratePhones(int phonesNumber) //function that generates users based on the amount that the user inputs.
        {
            for (int i = 0; i < phonesNumber; i++)
            {
                int phonetype_Id = random.Next(1, 3);
                int user_Id = user_Ids[random.Next(user_Ids.Count)]; //takes a random place in user_Ids list to make sure the user_Id actually exists.
                long phonenumber = random.Next(11111111, 99999999) * random.Next(1111111, 9999999);


                Phone phone = new Phone();
                phone.Phonetype_Id = phonetype_Id;
                phone.User_Id = user_Id;
                phone.Phonenumber = phonenumber;

                phones.Add(phone);
            }
        }


        //lists that contains the data that will be randomized from
        public static List<string> firstNames = new List<string>{"James", "Mary", "Michael", "Patricia", "Robert", "Jennifer", "John", "Linda", "David",
            "Elizabeth", "William", "Barbara", "Richard", "Susan", "Joseph", "Jessica", "Thomas", "Karen", "Christopher", "Sarah", "Charles",
            "Lisa", "Daniel", "Nancy", "Matthew", "Sandra", "Anthony", "Betty", "Mark", "Ashley", "Donald", "Emily", "Steven", "Kimberly",
            "Andrew", "Margaret", "Paul", "Donna", "Joshua", "Kenneth", "Carol", "Kevin", "Amanda", "Brian", "Melissa", "Timothy", "Deborah", "Ronald", "Stephanie"};

        public static List<string> lastNames = new List<string>{"Andersson", "Johansson", "Karlsson", "Nilsson", "Eriksson", "Larsson", "Olsson", "Persson",
            "Svensson", "Gustafsson", "Pettersson", "Jonsson", "Jansson", "Hansson", "Bengtsson", "Jönsson", "Lindberg", "Jakobsson", "Magnusson",
            "Lindström", "Olofsson", "Lindqvist", "Lindgren", "Berg", "Axelsson", "Bergström", "Lundberg", "Lind", "Lundgren", "Mattsson",
            "Berglund", "Fredriksson", "Sandberg", "Henriksson", "Ali", "Forsberg", "Mohamed", "Sjöberg", "Wallin", "Engström", "Eklund",
            "Danielsson", "Lundin", "Håkansson", "Björk", "Bergman", "Gunnarsson", "Wikström", "Holm" };

        public static List<string> emails = new List<string> {"Önskeringsvägen@hotmail.com", "Villagatan@live.se", " Vidargatan@gmail.com", "Grönviksvägen@gmail.se",
            "Ehrenstrahlsvägen@hotmail.com", "Margaretavägen@hotmail.com", "Narvavägen@hotmail.com", "ViktorRydbergsgatan@hotmail.com", "Vikbyvägen@hotmail.com", "Skansvägen@hotmail.com",
            "Önstanväg@hotmail.com", "Doktorsvägen@hotmail.com", "Kungsgårdsvägen@hotmail.com", "Åsgatan@hotmail.com", "DuRietzvägen@hotmail.com", "TimmerbyVillage@hotmail.com", "Visborgsgatan@live.se",
            "Fyrbåksvägen@hotmail.com", "Parkettgatan@hotmail.com"};


    }
}
