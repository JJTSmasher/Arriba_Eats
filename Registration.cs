using System.Text.RegularExpressions;

namespace Arriba_Eats {
    /// <summary>
    /// Base class for user registration, provides shared validation and menu logic.
    /// </summary>
    class Registration {
        /// <summary>
        /// Shows the registration menu for selecting user type.
        /// </summary>
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

        /// <summary>
        /// Prompts for and validates the user's name.
        /// </summary>
        protected static string GetValidatedName() {
            while (true) {
                UIFunctions.DisplayString("Please enter your name:");
                string nameInput = UIFunctions.ReadString();

                // Name must start with a letter 
                // and can include spaces, hyphens, and apostrophes.
                if (!string.IsNullOrEmpty(nameInput) && Regex.IsMatch(nameInput, @"^[a-zA-Z][a-zA-Z\s'-]*$")) {
                    return nameInput;
                }

                UIFunctions.DisplayString("Invalid name.");
            }
        }

        /// <summary>
        /// Prompts for and validates the user's age.
        /// </summary>
        protected static int GetValidatedAge() {
            while (true) {
                UIFunctions.DisplayString("Please enter your age (18-100):");
                if (int.TryParse(UIFunctions.ReadString(), out int age) && age >= 18 && age <= 100) {
                    return age;
                }
                UIFunctions.DisplayString("Invalid age.");
            }
        }

        /// <summary>
        /// Prompts for and validates the user email address.
        /// </summary>
        protected static string GetValidatedEmail() {
            while (true) {
                UIFunctions.DisplayString("Please enter your email address:");
                string email = UIFunctions.ReadString();

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

        /// <summary>
        /// Prompts for and validates the user's phone number.
        /// </summary>
        protected static string GetValidatedPhone() {
            while (true) {
                UIFunctions.DisplayString("Please enter your mobile phone number:");
                string phone = UIFunctions.ReadString();
                if (!string.IsNullOrEmpty(phone) && Regex.IsMatch(phone, @"^0\d{9}$")) {
                    return phone;
                }
                UIFunctions.DisplayString("Invalid phone number.");
            }
        }

        /// <summary>
        /// Prompts for and validates the user's password.
        /// </summary>
        protected static string GetValidatedPassword() {
            while (true) {
                UIFunctions.DisplayString("Your password must:");
                UIFunctions.DisplayString("- be at least 8 characters long");
                UIFunctions.DisplayString("- contain a number");
                UIFunctions.DisplayString("- contain a lowercase letter");
                UIFunctions.DisplayString("- contain an uppercase letter");
                UIFunctions.DisplayString("Please enter a password:");
                string password = UIFunctions.ReadString();

                // Check password requirements.
                if (password.Length >= 8 &&
                    Regex.IsMatch(password, @"\d") &&
                    Regex.IsMatch(password, @"[a-z]") &&
                    Regex.IsMatch(password, @"[A-Z]")) {
                    UIFunctions.DisplayString("Please confirm your password:");
                    if (UIFunctions.ReadString() == password) {
                        return password;
                    }
                    UIFunctions.DisplayString("Passwords do not match.");
                } else {
                    UIFunctions.DisplayString("Invalid password.");
                }
            }
        }

        /// <summary>
        /// Prompts for and validates a location in the form X,Y.
        /// </summary>
        protected static string GetValidatedLocation() {
            while (true) {
                UIFunctions.DisplayString("Please enter your location (in the form of X,Y) :");
                string location = UIFunctions.ReadString();

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

        /// <summary>
        /// Main registration method for a user. Can be overridden by subclasses.
        /// </summary>
        public virtual void Register() {
            name = GetValidatedName();
            age = GetValidatedAge();
            email = GetValidatedEmail();
            phone = GetValidatedPhone();
            password = GetValidatedPassword();
        }
        
        /// <summary>
        /// Returns the role string for this registration type. Can be overridden.
        /// </summary>
        protected virtual string GetRole() {
            return "user";
        }
    }
}