namespace Arriba_Eats {
    class Deliverer : User {
        public Deliverer(string email, string password, string role, string name, int phone, int age)
            : base(email, password, role, name, phone, age) {}

        public static void DelivererMenu(string name) {
            Console.WriteLine($"Welcome back, {name}!");
            while (true) {
                Console.WriteLine("Please make a choice from the menu below:");
                Console.WriteLine("1: Display your user information");
                Console.WriteLine("2: List orders available to deliver");
                Console.WriteLine("3: Arrived at restaurant to pick up order");
                Console.WriteLine("4: Mark this delivery as complete");
                Console.WriteLine("5: Log out");
                Console.WriteLine("Please enter a choice between 1 and 5:");

                if (!int.TryParse(Console.ReadLine(), out int choice) || choice < 1 || choice > 5) {
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
            Console.WriteLine($"Licence plate: {}");
            Console.WriteLine($"Current delivery:");
            Console.WriteLine($"Order {} from {} at {}");
            Console.WriteLine($"To be delivered to {} at {}");

        }
        
        public string licensePlate = "";
        public Dictionary<int, string> orderDeliverStatus = new Dictionary<int, string>();
        public bool delivererStatus; // at restaurant or not
        public struct DelivererLocation {
            public int x;
            public int y;

            // Constructor for DelivererLocation
            public DelivererLocation(int x, int y) {
                this.x = x;
                this.y = y;
            }
        }
        public int travelDistance = 0;
    }
}