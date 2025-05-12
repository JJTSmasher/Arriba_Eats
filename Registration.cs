namespace Arriba_Eats {
    class Registration {
        static void Main(string[] args) {
            Console.WriteLine("1: Customer");
            Console.WriteLine("2: Deliverer");
            Console.WriteLine("3: Client");
            Console.WriteLine("4: Return to previous menu");
            Console.WriteLine("Please enter a choice between 1 and 4:");
            Console.ReadLine();
            // on 4 - return to login menu

            Console.WriteLine("Please enter your name:");
            Console.ReadLine();

            Console.WriteLine("Please enter your age (18-100):");
            Console.ReadLine();

            Console.WriteLine("Please enter your email address:");
            Console.ReadLine();

            Console.WriteLine("Please enter your mobile phone number:");
            Console.ReadLine();

            Console.WriteLine("Your password must:");
            Console.WriteLine("- be at least 8 characters long");
            Console.WriteLine("- contain a number");
            Console.WriteLine("- contain a lowercase letter");
            Console.WriteLine("- contain an uppercase letter");
            Console.WriteLine("Please enter a password:");
            Console.ReadLine();

            Console.WriteLine("Please confirm your password:");
            Console.ReadLine();

        }
    }

    class Customer_Registration : Registration {
        static void Main(string[] args) {
            Console.WriteLine("Please enter your location (in the form of X,Y):");
            Console.ReadLine();

            Console.WriteLine("You have been successfully registered as a customer, {name}!");
        }
    }

    class Deliverer_Registration : Registration {
        static void Main(string[] args) {
            Console.WriteLine("Please enter your licence plate:");
            Console.ReadLine();

            Console.WriteLine("You have been successfully registered as a deliverer, {name}!");
        }
    }

    class Client_Registration : Registration {
        static void Main(string[] args) {
            Console.WriteLine("Please enter your restaurant's name:");
            Console.ReadLine();

            Console.WriteLine("Please select your restaurant's style:");
            Console.WriteLine("1: Italian");
            Console.WriteLine("2: French");
            Console.WriteLine("3: Chinese");
            Console.WriteLine("4: Japanese");
            Console.WriteLine("5: American");
            Console.WriteLine("6: Australian");
            Console.WriteLine("Please enter a choice between 1 and 6:");
            Console.ReadLine();

            Console.WriteLine("Please enter your location (in the form of X,Y):");
            Console.ReadLine();

            Console.WriteLine("You have been successfully registered as a client, {name}!");
            Console.ReadLine();
        }
    }
}