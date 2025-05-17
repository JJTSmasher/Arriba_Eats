using System;
using System.Collections.Generic;
using System.Linq;

namespace Arriba_Eats {
    class Login {
        public static void ShowMenu() {
            
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
                        Authenticate();
                        break;
                    case 2:
                        Registration.ShowMenu(); // Call the Registration menu
                        return;
                    case 3:
                        Console.WriteLine("Thank you for using Arriba Eats!");
                        return; // Exit the program
                }
            }
        }
        // Simulated in-memory user store
        private static List<User> users = new List<User> {
            new User("test@arribaeats.com", "Password123", "customer", "Test User", 0123456789, 25),
            new User("alice@arribaeats.com", "AlicePass", "client", "Alice", 0987654321, 30),
            new User("bob@arribaeats.com", "BobPass", "deliverer", "Bob", 0123454321, 35)
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
                Console.WriteLine("Email or password cannot be empty.");
                return;
            }

            User user = IsValidUser(email, password);
            if (user != null) {
                Console.WriteLine($"Welcome back, {user.Name}!");
                switch (user.Role.ToLower()) {
                    case "customer":
                        Customer.CustomerMenu(user.Name);
                        return;
                    case "deliverer":
                        Deliverer.DelivererMenu();
                        return;
                    case "client":
                        Client.ClientMenu();
                        return;
                    default:
                        Console.WriteLine("No role");
                        return;
                }
            } else {
                Console.WriteLine("Invalid email or password.");
            }
        }

        private static User IsValidUser(string email, string password) {
            return users.FirstOrDefault(user => user.Email == email && user.Password == password);
        }
    }
}
