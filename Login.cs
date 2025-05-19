

namespace Arriba_Eats {
    class Login {
        public static List<User> users = [];
        public static List<Review> Reviews = [];
        public static void ShowMenu() {
            
            while (true) {
                Console.WriteLine("Please make a choice from the menu below:");
                Console.WriteLine("1: Login as a registered user");
                Console.WriteLine("2: Register as a new user");
                Console.WriteLine("3: Exit");
                Console.WriteLine("Please enter a choice between 1 and 3:");

                if (!int.TryParse(Console.ReadLine(), out int choice) || choice < 1 || choice > 3) {
                    Console.WriteLine("Invalid choice.");
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
                User.CurrentUserEmail = user.Email;
                switch (user.Role.ToLower()) {
                    case "customer":
                        CustomerService.CustomerMenu((Customer)user); // cast to Customer
                        return;
                    case "client":
                        ClientService.ClientMenu((Client)user); // cast to Client
                        return;
                    case "deliverer":
                        DelivererService.DelivererMenu((Deliverer)user); // cast to Deliverer
                        return;
                    default:
                        Console.WriteLine("Unknown role. Returning to the main menu.");
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
