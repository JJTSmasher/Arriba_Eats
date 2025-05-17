namespace Arriba_Eats {
    class Client : User {
        public Client(string email, string password, string role, string name, string phone, int age)
            : base(email, password, role, name, phone, age) {}

        public static void ClientMenu(Client client) {
            while (true) {
                Console.WriteLine("Please make a choice from the menu below:");
                Console.WriteLine("1: Display your user information");
                Console.WriteLine("2: Add item to restaurant menu");
                Console.WriteLine("3: See current orders");
                Console.WriteLine("4: Start cooking order");
                Console.WriteLine("5: Finish cooking order");
                Console.WriteLine("6: Handle deliverers who have arrived");
                Console.WriteLine("7: Log out");
                Console.WriteLine("Please enter a choice between 1 and 7:");

                if (!int.TryParse(Console.ReadLine(), out int choice) || choice < 1 || choice > 7) {
                    Console.WriteLine("Invalid choice.");
                    continue;
                }

                switch (choice) {
                    case 1:
                        client.ShowData();
                        break;
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

        private void ShowData() {
            Console.WriteLine("Your user details are as follows:");
            Console.WriteLine($"Name: {Name}");
            Console.WriteLine($"Age: {Age}");
            Console.WriteLine($"Email: {Email}");
            Console.WriteLine($"Mobile: {Phone}");
            Console.WriteLine($"Restaurant Name: {restaurantName}");
            Console.WriteLine($"Restaurant Rating: {restaurantRating:F1}");
            Console.WriteLine($"Location: {Location.x},{Location.y}");

            if (restaurantStyles.Count > 0) {
                Console.WriteLine("Restaurant Styles:");
                foreach (var style in restaurantStyles) {
                    Console.WriteLine($"Style ID: {style.Key}, Style: {style.Value}");
                }
            } else {
                Console.WriteLine("No restaurant styles defined.");
            }
        }

        public struct RestaurantLocation {
            public int x;
            public int y;

            public RestaurantLocation(int x, int y) {
                this.x = x;
                this.y = y;
            }
        }

        public RestaurantLocation Location { get; set; }

        public string restaurantName = "";
        public Dictionary<int, string> restaurantStyles = new Dictionary<int, string>(); // 1-6 as restaurant styles
        public decimal restaurantRating = 0;
    }
}