namespace Arriba_Eats {
    class LoginMenu {
        public static void ShowMenu() { // Renamed from Main to ShowMenu
            while (true) {
                Console.WriteLine("Welcome to Arriba Eats!");
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
    }
}