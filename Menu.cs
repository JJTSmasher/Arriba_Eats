namespace Arriba_Eats {
    class Menu : Client 
    {
        public string itemName = "";
        public decimal itemPrice = 0;
    }

    class LoginMenu {
        static void Main(string[] args) {
            Console.WriteLine("Welcome to Arriba Eats!");
            Console.WriteLine("Please make a choice from the menu below:");
            Console.WriteLine("1: Login as a registered user");
            Console.WriteLine("2: Register as a new user");
            Console.WriteLine("3: Exit");
            Console.WriteLine("Please enter a choice between 1 and 3");
            // if resp == 3
                // Console.WriteLine("Thank you for using Arriba Eats")
                // terminate program
        }
    }

    class Login {
        static void Main(string[] args) {
            Console.WriteLine("Email:");
            Console.ReadLine();
            Console.WriteLine("Password:");
            Console.ReadLine();
            // if email / PW dont match
                // Console.WriteLine("Invalid email or password.");
                // return to login menu
        }
    }

    
}