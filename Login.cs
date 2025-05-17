using System;
using System.Collections.Generic;

namespace Arriba_Eats {
    class User {
        public string Email { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }  // Optional: "customer", "client", "deliverer"

        public User(string email, string password, string role) {
            Email = email;
            Password = password;
            Role = role;
        }
    }

    class Login {
        // Simulated in-memory user store
        private static List<User> users = new List<User> {
            new User("test@arribaeats.com", "Password123", "customer"),
            new User("alice@arribaeats.com", "AlicePass", "client"),
            new User("bob@arribaeats.com", "BobPass", "deliverer")
        };

        public static void Authenticate() {
            Console.WriteLine("Email:");
            string email = Console.ReadLine();
            Console.WriteLine("Password:");
            string password = Console.ReadLine();

            User user = IsValidUser(email, password);
            if (user != null) {
                Console.WriteLine($"Login successful! Welcome back, {user.Role}.");
                // Proceed to role-specific menu if needed
            } else {
                Console.WriteLine("Invalid email or password. Returning to the login menu...");
            }
        }

        private static User IsValidUser(string email, string password) {
            foreach (User user in users) {
                if (user.Email == email && user.Password == password) {
                    return user;
                }
            }
            return null;
        }
    }
}
