using System.Text.Json;
using System;

public class Program
{
    public class Person
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

    }

    public static string SerializeUserData(Person person){
        if (
            string.IsNullOrEmpty(person.Name) || 
            string.IsNullOrEmpty(person.Email) || 
            string.IsNullOrEmpty(person.Password)
        )
        {
            Console.WriteLine("Invalid user data. All fields must be filled out. Serialization aborted");
            return string.Empty;
        }
        return JsonSerializer.Serialize(person);
    }

    public static void Main()
    {
        Person user = new Person
        {
            Name = "John Doe",
            Email = "john@gmail.com",
            Password = "password123"
        };
        string serializedUser = SerializeUserData(user);
        Console.WriteLine(serializedUser);

    }
}