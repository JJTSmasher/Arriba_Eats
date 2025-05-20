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

                // Validate menu input.
                if (!int.TryParse(Console.ReadLine(), out int choice) || choice < 1 || choice > 4) {
                    Console.WriteLine("Invalid choice.");
                    continue;
                }

                // Handle user type selection.
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
                        Console.WriteLine("Returning to the previous menu.");
                        return;
                }
            }
        }

        // Prompts the user for input with prompt.
        protected static string GetInput(string prompt) {
            Console.WriteLine(prompt);
            return Console.ReadLine();
        }

        // Prompts for and validates the user's name.
        protected static string GetValidatedName() {
            while (true) {
                Console.WriteLine("Please enter your name:");
                string nameInput = Console.ReadLine();

                // Name must start with a letter 
                // and can include spaces, hyphens, and apostrophes.
                if (!string.IsNullOrEmpty(nameInput) && Regex.IsMatch(nameInput, @"^[a-zA-Z][a-zA-Z\s'-]*$")) {
                    return nameInput;
                }

                Console.WriteLine("Invalid name.");
            }
        }

        // Prompts for and validates the user's age 
        // must be 18-100.
        protected static int GetValidatedAge() {
            while (true) {
                Console.WriteLine("Please enter your age (18-100):");
                if (int.TryParse(Console.ReadLine(), out int age) && age >= 18 && age <= 100) {
                    return age;
                }
                Console.WriteLine("Invalid age.");
            }
        }

        // Prompts for and validates the user email address.
        protected static string GetValidatedEmail() {
            while (true) {
                Console.WriteLine("Please enter your email address:");
                string email = Console.ReadLine();

                // Validate email format and check for uniqueness.
                if (!string.IsNullOrEmpty(email) && Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$")) {
                    if (!Login.IsEmailInUse(email)) {
                        return email;
                    }
                    Console.WriteLine("This email address is already in use.");
                } else { // Error handling
                    Console.WriteLine("Invalid email address.");
                }
            }
        }

        // Prompts for and validates the user's phone number 
        // must be 10 digits, starting with 0.
        protected static string GetValidatedPhone() {
            while (true) {
                Console.WriteLine("Please enter your mobile phone number:");
                string phone = Console.ReadLine();
                if (!string.IsNullOrEmpty(phone) && Regex.IsMatch(phone, @"^0\d{9}$")) {
                    return phone;
                }
                Console.WriteLine("Invalid phone number.");
            }
        }

        // Prompts for and validates the user's password 
        // length, number, upper/lowercase.
        protected static string GetValidatedPassword() {
            while (true) {
                Console.WriteLine("Your password must:");
                Console.WriteLine("- be at least 8 characters long");
                Console.WriteLine("- contain a number");
                Console.WriteLine("- contain a lowercase letter");
                Console.WriteLine("- contain an uppercase letter");
                Console.WriteLine("Please enter a password:");
                string password = Console.ReadLine();

                // Check password requirements.
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

        // Prompts for and validates a location in the form X,Y.
        protected static string GetValidatedLocation() {
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

        // Store user registration data.
        protected string name = string.Empty;
        protected string email = string.Empty;
        protected string password = string.Empty;
        protected string phone;
        protected int age;

        // Main registration method for a user 
        // can be overridden by subclasses.
        public virtual void Register() {
            name = GetValidatedName();
            age = GetValidatedAge();
            email = GetValidatedEmail();
            phone = GetValidatedPhone();
            password = GetValidatedPassword();
        }
        
        // Returns the role string for this registration type 
        // can be overridden.
        protected virtual string GetRole() {
            return "user";
        }
    }
}