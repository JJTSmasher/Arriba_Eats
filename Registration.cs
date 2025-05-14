using System;
using System.Text.RegularExpressions;

namespace Arriba_Eats {
    class Registration {
        public static void ShowMenu() { // Renamed from Main to ShowMenu
            while (true) {
                Console.WriteLine("1: Customer");
                Console.WriteLine("2: Deliverer");
                Console.WriteLine("3: Client");
                Console.WriteLine("4: Return to previous menu");
                Console.WriteLine("Please enter a choice between 1 and 4:");

                if (!int.TryParse(Console.ReadLine(), out int choice) || choice < 1 || choice > 4) {
                    Console.WriteLine("Invalid choice. Please try again.");
                    continue;
                }

                switch (choice) {
                    case 1:
                        new Customer_Registration().Register();
                        break;
                    case 2:
                        new Deliverer_Registration().Register();
                        break;
                    case 3:
                        new Client_Registration().Register();
                        break;
                    case 4:
                        Console.WriteLine("Returning to the previous menu...");
                        return;
                }
            }
        }

        protected string GetInput(string prompt) {
            Console.WriteLine(prompt);
            return Console.ReadLine();
        }

        protected int GetValidatedAge() {
            while (true) {
                Console.WriteLine("Please enter your age (18-100):");
                if (int.TryParse(Console.ReadLine(), out int age) && age >= 18 && age <= 100) {
                    return age;
                }
                Console.WriteLine("Invalid age. Please try again.");
            }
        }

        protected string GetValidatedEmail() {
            while (true) {
                Console.WriteLine("Please enter your email address:");
                string email = Console.ReadLine();
                if (Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$")) {
                    return email;
                }
                Console.WriteLine("Invalid email. Please try again.");
            }
        }

        protected string GetValidatedPassword() {
            while (true) {
                Console.WriteLine("Your password must:");
                Console.WriteLine("- be at least 8 characters long");
                Console.WriteLine("- contain a number");
                Console.WriteLine("- contain a lowercase letter");
                Console.WriteLine("- contain an uppercase letter");
                Console.WriteLine("Please enter a password:");
                string password = Console.ReadLine();

                if (password.Length >= 8 &&
                    Regex.IsMatch(password, @"\d") &&
                    Regex.IsMatch(password, @"[a-z]") &&
                    Regex.IsMatch(password, @"[A-Z]")) {
                    Console.WriteLine("Please confirm your password:");
                    if (Console.ReadLine() == password) {
                        return password;
                    }
                    Console.WriteLine("Passwords do not match. Please try again.");
                } else {
                    Console.WriteLine("Password does not meet the requirements. Please try again.");
                }
            }
        }

        public virtual void Register() {
            string name = GetInput("Please enter your name:");
            int age = GetValidatedAge();
            string email = GetValidatedEmail();
            string password = GetValidatedPassword();

            Console.WriteLine($"Registration complete! Welcome, {name}.");
        }
    }

    class Customer_Registration : Registration {
        public override void Register() {
            base.Register();
            string location = GetInput("Please enter your location (in the form of X,Y):");
            Console.WriteLine($"You have been successfully registered as a customer, {location}!");
        }
    }

    class Deliverer_Registration : Registration {
        public override void Register() {
            base.Register();
            string licencePlate = GetInput("Please enter your licence plate:");
            Console.WriteLine($"You have been successfully registered as a deliverer with licence plate {licencePlate}!");
        }
    }

    class Client_Registration : Registration {
        public override void Register() {
            base.Register();
            string restaurantName = GetInput("Please enter your restaurant's name:");
            Console.WriteLine("Please select your restaurant's style:");
            Console.WriteLine("1: Italian");
            Console.WriteLine("2: French");
            Console.WriteLine("3: Chinese");
            Console.WriteLine("4: Japanese");
            Console.WriteLine("5: American");
            Console.WriteLine("6: Australian");

            int styleChoice;
            while (!int.TryParse(Console.ReadLine(), out styleChoice) || styleChoice < 1 || styleChoice > 6) {
                Console.WriteLine("Invalid choice. Please try again.");
            }

            string[] styles = { "Italian", "French", "Chinese", "Japanese", "American", "Australian" };
            string restaurantStyle = styles[styleChoice - 1];

            string location = GetInput("Please enter your location (in the form of X,Y):");
            Console.WriteLine($"You have been successfully registered as a client with restaurant '{restaurantName}' ({restaurantStyle}) at location {location}!");
        }
    }
}