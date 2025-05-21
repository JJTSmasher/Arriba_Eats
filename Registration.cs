using System.Text.RegularExpressions;

namespace Arriba_Eats {
    class Registration {
        public static void ShowMenu() {
            while (true) {
                UIFunctions.DisplayString("Which type of user would you like to register as?");
                UIFunctions.DisplayString("1: Customer");
                UIFunctions.DisplayString("2: Deliverer");
                UIFunctions.DisplayString("3: Client");
                UIFunctions.DisplayString("4: Return to the previous menu");
                UIFunctions.DisplayString("Please enter a choice between 1 and 4:");

                // Handle user type selection.
                switch (UIFunctions.GetChoice(1, 5)) {
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
                        UIFunctions.DisplayString("Returning to the previous menu.");
                        return;
                }
            }
        }

        // Prompts the user for input with prompt.
        protected static string GetInput(string prompt) {
            UIFunctions.DisplayString(prompt);
            return Console.ReadLine();
        }

        // Prompts for and validates the user's name.
        protected static string GetValidatedName() {
            while (true) {
                UIFunctions.DisplayString("Please enter your name:");
                string nameInput = Console.ReadLine();

                // Name must start with a letter 
                // and can include spaces, hyphens, and apostrophes.
                if (!string.IsNullOrEmpty(nameInput) && Regex.IsMatch(nameInput, @"^[a-zA-Z][a-zA-Z\s'-]*$")) {
                    return nameInput;
                }

                UIFunctions.DisplayString("Invalid name.");
            }
        }

        // Prompts for and validates the user's age 
        // must be 18-100.
        protected static int GetValidatedAge() {
            while (true) {
                UIFunctions.DisplayString("Please enter your age (18-100):");
                if (int.TryParse(Console.ReadLine(), out int age) && age >= 18 && age <= 100) {
                    return age;
                }
                UIFunctions.DisplayString("Invalid age.");
            }
        }

        // Prompts for and validates the user email address.
        protected static string GetValidatedEmail() {
            while (true) {
                UIFunctions.DisplayString("Please enter your email address:");
                string email = Console.ReadLine();

                // Validate email format and check for uniqueness.
                if (!string.IsNullOrEmpty(email) && Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$")) {
                    if (!Login.IsEmailInUse(email)) {
                        return email;
                    }
                    UIFunctions.DisplayString("This email address is already in use.");
                } else { // Error handling
                    UIFunctions.DisplayString("Invalid email address.");
                }
            }
        }

        // Prompts for and validates the user's phone number 
        // must be 10 digits, starting with 0.
        protected static string GetValidatedPhone() {
            while (true) {
                UIFunctions.DisplayString("Please enter your mobile phone number:");
                string phone = Console.ReadLine();
                if (!string.IsNullOrEmpty(phone) && Regex.IsMatch(phone, @"^0\d{9}$")) {
                    return phone;
                }
                UIFunctions.DisplayString("Invalid phone number.");
            }
        }

        // Prompts for and validates the user's password 
        // length, number, upper/lowercase.
        protected static string GetValidatedPassword() {
            while (true) {
                UIFunctions.DisplayString("Your password must:");
                UIFunctions.DisplayString("- be at least 8 characters long");
                UIFunctions.DisplayString("- contain a number");
                UIFunctions.DisplayString("- contain a lowercase letter");
                UIFunctions.DisplayString("- contain an uppercase letter");
                UIFunctions.DisplayString("Please enter a password:");
                string password = Console.ReadLine();

                // Check password requirements.
                if (password.Length >= 8 &&
                    Regex.IsMatch(password, @"\d") &&
                    Regex.IsMatch(password, @"[a-z]") &&
                    Regex.IsMatch(password, @"[A-Z]")) {
                    UIFunctions.DisplayString("Please confirm your password:");
                    if (Console.ReadLine() == password) {
                        return password;
                    }
                    UIFunctions.DisplayString("Passwords do not match.");
                } else {
                    UIFunctions.DisplayString("Invalid password.");
                }
            }
        }

        // Prompts for and validates a location in the form X,Y.
        protected static string GetValidatedLocation() {
            while (true) {
                UIFunctions.DisplayString("Please enter your location (in the form of X,Y) :");
                string location = Console.ReadLine();

                // Regex to validate the location format
                if (!string.IsNullOrEmpty(location) && Regex.IsMatch(location, @"^-?\d+,-?\d+$")) {
                    return location;
                }

                UIFunctions.DisplayString("Invalid location.");
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