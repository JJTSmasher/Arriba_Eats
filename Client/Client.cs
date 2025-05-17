namespace Arriba_Eats {
    class Client : User
    {
        public Client(string email, string password, string role, string name, int phone, int age)
            : base(email, password, role, name, phone, age) {}
        
        public static void ClientMenu(string name) {
            Console.WriteLine($"Welcome back, {name}!");
            while (true) {
                Console.WriteLine("Please make a choice from the menu below:");
                Console.WriteLine("1: Display your user information");
                Console.WriteLine("2: Add item to restaurant menu");
                Console.WriteLine("3: See current orders");
                Console.WriteLine("4: Start cooking order");
                Console.WriteLine("5: Finish cooking order");
                Console.WriteLine("6: Handle deliverers who have arrived");
                Console.WriteLine("6: Handle deliverers who have arrived");
                Console.WriteLine("Please enter a choice between 1 and 7:");

                if (!int.TryParse(Console.ReadLine(), out int choice) || choice < 1 || choice > 7) {
                    Console.WriteLine("Invalid choice. Please try again.");
                    continue;
                }

                switch (choice) {
                    case 1:

                        return;
                    case 2:

                        return;
                    case 3:

                        return;
                    case 4:

                        return;
                    case 5:
                        
                        return;
                    case 6:

                        return;
                    case 7:
                        Console.WriteLine("You are now logged out.");
                        return;
                }
            }
        }

        private string ShowData() {
            Console.WriteLine("Your user details are as follows:");
            Console.WriteLine($"Name: {Name}");
            Console.WriteLine($"Age: {Age}");
            Console.WriteLine($"Email: {Email}");
            Console.WriteLine($"Mobile: {Phone}");
            //Console.WriteLine($"Restaurant name: {}");
            //Console.WriteLine($"Restaurant style: {}");
            //Console.WriteLine($"Restaurant location: {}");
        }
        
        public string restaurantName = "";
        public Dictionary<int, string> restaurantStyles = new Dictionary<int, string>(); // 1-6 as resturaunt styles
        public struct RestaurantLocation 
        {
            public int x;
            public int y;
        }
        public decimal restaurantRating = 0;
        
    }
}