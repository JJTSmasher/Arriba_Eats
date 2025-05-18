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
                        client.AddMenuItem();
                        break;
                    case 3:
                        client.CurrentOrders();
                        break;
                    case 4:
                        client.StartCookingOrder();
                        break;
                    case 5:
                        client.FinishCookingOrder();
                        break;
                    case 6:
                        client.HandleDelivererArrived();
                        break;
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
            Console.WriteLine($"Restaurant name: {restaurantName}");
            //Console.WriteLine($"Restaurant Rating: {restaurantRating:F1}");
            

            if (restaurantStyles.Count > 0) {
                Console.WriteLine($"Restaurant style: {restaurantStyles.Values.First()}");
            } else {
                Console.WriteLine("No restaurant style defined.");
            }

            Console.WriteLine($"Restaurant location: {Location.x},{Location.y}");
        }

        public struct MenuItem {
            public string Name;
            public decimal Price;
            public MenuItem(string name, decimal price) {
                Name = name;
                Price = price;
            }
        }

        private List<MenuItem> menu = new List<MenuItem>();

        private void AddMenuItem() {
            Console.WriteLine("This is your restaurant's current menu:");
            foreach (var item in menu) {
                Console.WriteLine($"  ${item.Price:F2} {item.Name}");
            }

            // Get item name
            string itemName;
            while (true) {
                Console.WriteLine("Please enter the name of the new item (blank to cancel):");
                itemName = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(itemName)) {
                    return;
                }
                break;
            }
            
            // Get item price
            decimal itemPrice;
            while (true) {
                Console.WriteLine("Please enter the price of the new item (without the $):");
                string priceInput = Console.ReadLine();
                if (decimal.TryParse(priceInput, out itemPrice) && itemPrice > 0) {
                    break;
                }
                Console.WriteLine("Invalid price.");
            }

            menu.Add(new MenuItem(itemName, itemPrice));
            Console.WriteLine($"Successfully added {itemName} (${itemPrice:F2}) to menu.");
        }

        private void CurrentOrders() {
            Console.WriteLine("");
        }

        private void StartCookingOrder() {
            Console.WriteLine("");
        }

        private void FinishCookingOrder() {
            Console.WriteLine("");
        }

        private void HandleDelivererArrived() {
            Console.WriteLine("");
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