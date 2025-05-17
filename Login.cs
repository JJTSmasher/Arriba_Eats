using System;
using System.Collections.Generic;
using System.Linq;

namespace Arriba_Eats {
    class Login {
        public static void ShowMenu() { // Renamed from Main to ShowMenu
            while (true) {
                Console.WriteLine("Please make a choice from the menu below:");
                Console.WriteLine("1: Login as a registered user");
                Console.WriteLine("2: Register as a new user");
                Console.WriteLine("3: Exit");
                Console.WriteLine("Please enter a choice between 1 and 3:");

                if (!int.TryParse(Console.ReadLine(), out int choice) || choice < 1 || choice > 3) {
                    Console.WriteLine("Invalid choice. Please try again.");
                    continue;
                }

                switch (choice) {
                    case 1:
                        Login.Authenticate();
                        break;
                    case 2:
                        Registration.ShowMenu(); // Call the Registration menu
                        break;
                    case 3:
                        Console.WriteLine("Thank you for using Arriba Eats!");
                        return; // Exit the program
                }
            }
        }
        // Simulated in-memory user store
        private static List<User> users = new List<User> {
            new User("test@arribaeats.com", "Password123", "customer"),
            new User("alice@arribaeats.com", "AlicePass", "client"),
            new User("bob@arribaeats.com", "BobPass", "deliverer")
        };

        public static bool IsEmailInUse(string email) {
            return users.Any(user => user.Email.Equals(email, StringComparison.OrdinalIgnoreCase));
        }

        public static void AddUser(User user) {
            users.Add(user);
        }

        public static void Authenticate() {
            Console.WriteLine("Email:");
            string email = Console.ReadLine();
            Console.WriteLine("Password:");
            string password = Console.ReadLine();
            
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password)) {
                Console.WriteLine("Email or password cannot be empty. Returning to the login menu...");
                return;
            }

            User user = IsValidUser(email, password);
            if (user != null) {
                Console.WriteLine($"Login successful! Welcome back, {user.Role}.");
                // Proceed to role-specific menu ---------------
            } else {
                Console.WriteLine("Invalid email or password. Returning to the login menu...");
            }
        }

        private static User IsValidUser(string email, string password) {
            return users.FirstOrDefault(user => user.Email == email && user.Password == password);
        }
    }
}
