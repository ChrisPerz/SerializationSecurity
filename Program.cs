using System.Text.Json;
using System.Text;
using System;
using System.Security.Cryptography;

public class Program
{
    public class Person
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Hash { get; set; }

        public string GenerateHash()
        {
            string userData = $"{Name}|{Email}|{Password}";
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(userData));
                return Convert.ToBase64String(hashBytes);
            }
        }

        public void EncodePassword ()
        {
            if (string.IsNullOrEmpty(Password))
            {
                Console.WriteLine("Password cannot be empty. Encryption aborted.");
                return;
            }
            Password = Convert.ToBase64String(Encoding.UTF8.GetBytes(Password));
        }
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
        person.EncodePassword();
        person.Hash = person.GenerateHash();
        return JsonSerializer.Serialize(person);
    }

    public static Person DeserializeUserData(string json, bool isValidSource){
        if (!isValidSource)
        {
            Console.WriteLine("Invalid source. Deserialization aborted.");
            return null;
        }

        Person person = JsonSerializer.Deserialize<Person>(json);

        string expectedHash = person.GenerateHash();

        if (person.Hash != expectedHash)
        {
            Console.WriteLine("Data integrity check failed. Deserialization aborted.");
            return null;
        }

        Console.WriteLine("Data integrity check passed.");
        return person;
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
        Person deserializedUser = DeserializeUserData(serializedUser, true);
        Console.WriteLine(SerializeUserData(deserializedUser));
    }
}