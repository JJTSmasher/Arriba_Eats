using System;
using System.Text.RegularExpressions;

namespace Arriba_Eats {
    class Registration {
        public static void ShowMenu() {
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

        protected string GetValidatedPhone() {
            while (true) {
                Console.WriteLine("Please enter your mobile phone number:");
                string phone = Console.ReadLine();
                if (Regex.IsMatch(phone, @"^\+?\d{10,15}$")) {
                    return phone;
                }
                Console.WriteLine("Invalid phone number. Please try again.");
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

        protected string name;
        public virtual void Register() {
            string name = GetInput("Please enter your name:");
            int age = GetValidatedAge();
            string email = GetValidatedEmail();
            string password = GetValidatedPassword();
        }
    }
}