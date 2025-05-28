using System.Text.Json;
using System;

public class Program
{
    public class Person
    {
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;

    }

    public static string SerializeUserData(Person person){
        return JsonSerializer.Serialize(person);
    }

    public static void Main()
    {
        Person user = new Person
        {
            Name = "John Doe",
            Email = "john@gmail.com",
            Password = "securepassword123"
        };
        string serializedUser = SerializeUserData(user);
        Console.WriteLine(serializedUser);
        // No security, without encryption or hashing, the password is stored in plain text.
    }
}