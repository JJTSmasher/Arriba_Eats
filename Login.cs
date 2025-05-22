namespace Arriba_Eats {
    class Login {
        // List of all registered users in the system.
        public static List<User> users = [];
        // List of all reviews in the system.
        public static List<Review> Reviews = [];

        // Displays the main menu for login and registration.
        public static void ShowMenu() {
            while (true) {
                UIFunctions.DisplayString("Please make a choice from the menu below:");
                UIFunctions.DisplayString("1: Login as a registered user");
                UIFunctions.DisplayString("2: Register as a new user");
                UIFunctions.DisplayString("3: Exit");
                UIFunctions.DisplayString("Please enter a choice between 1 and 3:");

                // Validate user input.
                if (!int.TryParse(UIFunctions.ReadString(), out int choice) || choice < 1 || choice > 3) {
                    UIFunctions.DisplayString("Invalid choice.");
                    continue;
                }

                // Handle menu selection.
                switch (choice) {
                    case 1:
                        Authenticate();
                        break;
                    case 2:
                        Registration.ShowMenu(); // Call the Registration menu
                        return;
                    case 3:
                        UIFunctions.DisplayString("Thank you for using Arriba Eats!");
                        return; // Exit the program
                }
            }
        }

        // Checks if an email is already in use by another user.
        public static bool IsEmailInUse(string email) {
            return users.Any(user => user.Email.Equals(email, StringComparison.OrdinalIgnoreCase));
        }

        // Adds a new user to the system.
        public static void AddUser(User user) {
            users.Add(user);
        }

        // Handles user authentication and role-based menu redirection.
        public static void Authenticate() {
            UIFunctions.DisplayString("Email:");
            string email = UIFunctions.ReadString();
            UIFunctions.DisplayString("Password:");
            string password = UIFunctions.ReadString();
            
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password)) {
                UIFunctions.DisplayString("Email or password cannot be empty.");
                return;
            }

            // Validate user credentials.
            User user = IsValidUser(email, password);
            if (user != null) {
                UIFunctions.DisplayString($"Welcome back, {user.Name}!");
                User.CurrentUserEmail = user.Email;
                // Redirect to the appropriate menu based on user role.
                switch (user.Role.ToLower()) {
                    case "customer":
                        CustomerService.CustomerMenu((Customer)user);
                        return;
                    case "client":
                        ClientService.ClientMenu((Client)user);
                        return;
                    case "deliverer":
                        DelivererService.DelivererMenu((Deliverer)user);
                        return;
                    default:
                        UIFunctions.DisplayString("Unknown role. Returning to the main menu.");
                        return;
                }
            } else {
                UIFunctions.DisplayString("Invalid email or password.");
            }
        }

        // Checks if the provided email and password match a registered user.
        private static User IsValidUser(string email, string password) {
            return users.FirstOrDefault(user => user.Email == email && user.Password == password);
        }
    }
}
