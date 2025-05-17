using System;
using System.Text.RegularExpressions;

namespace Arriba_Eats {
    class Registration {
        public static void ShowMenu() {
            while (true) {
                Console.WriteLine("Which type of user would you like to register as?");
                Console.WriteLine("1: Customer");
                Console.WriteLine("2: Deliverer");
                Console.WriteLine("3: Client");
                Console.WriteLine("4: Return to the previous menu");
                Console.WriteLine("Please enter a choice between 1 and 4:");

                if (!int.TryParse(Console.ReadLine(), out int choice) || choice < 1 || choice > 4) {
                    Console.WriteLine("Invalid choice.");
                    continue;
                }

                switch (choice) {
                    case 1:
                        new Customer_Registration().Register();
                        return;
                    case 2:
                        new Deliverer_Registration().Register();
                        return;
                    case 3:
                        new Client_Registration().Register();
                        return;
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

        protected string GetValidatedName() {
            while (true) {
                Console.WriteLine("Please enter your name:");
                string nameInput = Console.ReadLine();

                // Regex to validate the name
                if (!string.IsNullOrEmpty(nameInput) && Regex.IsMatch(nameInput, @"^[a-zA-Z][a-zA-Z\s'-]*$")) {
                    return nameInput;
                }

                Console.WriteLine("Invalid name.");
            }
        }

        protected int GetValidatedAge() {
            while (true) {
                Console.WriteLine("Please enter your age (18-100):");
                if (int.TryParse(Console.ReadLine(), out int age) && age >= 18 && age <= 100) {
                    return age;
                }
                Console.WriteLine("Invalid age.");
            }
        }

        protected string GetValidatedEmail() {
            while (true) {
                Console.WriteLine("Please enter your email address:");
                string email = Console.ReadLine();

                // Validate email format
                if (!string.IsNullOrEmpty(email) && Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$")) {
                    // Check if the email is already in use
                    if (!Login.IsEmailInUse(email)) {
                        return email;
                    }
                    Console.WriteLine("This email address is already in use.");
                } else {
                    Console.WriteLine("Invalid email address.");
                }
            }
        }

        protected int GetValidatedPhone() {
            while (true) {
                Console.WriteLine("Please enter your mobile phone number:");
                string phone = Console.ReadLine();
                if (Regex.IsMatch(phone, @"^0\d{9}$")) {
                    return int.Parse(phone); // Parse phone number as integer
                }
                Console.WriteLine("Invalid phone number.");
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
                    Console.WriteLine("Passwords do not match.");
                } else {
                    Console.WriteLine("Invalid password.");
                }
            }
        }

        protected string GetValidatedLocation() {
            while (true) {
                Console.WriteLine("Please enter your location (in the form of X,Y) :");
                string location = Console.ReadLine();

                // Regex to validate the location format
                if (!string.IsNullOrEmpty(location) && Regex.IsMatch(location, @"^-?\d+,-?\d+$")) {
                    return location;
                }

                Console.WriteLine("Invalid location.");
            }
        }

        protected string name = string.Empty;
        protected string email = string.Empty;
        protected string password = string.Empty;
        protected int phone;
        protected int age;

        public virtual void Register() {
            name = GetValidatedName();
            age = GetValidatedAge();
            email = GetValidatedEmail();
            phone = GetValidatedPhone();
            password = GetValidatedPassword();

            Login.AddUser(new User(email, password, GetRole(), name, phone, age));
            
        }
        
        protected virtual string GetRole() {
            return "user";
        }
    }
}