namespace Arriba_Eats {
    class Customer : User 
    {
        public Customer(string email, string password, string role, string name, string phone, int age)
            : base(email, password, role, name, phone, age) {}

        public static void CustomerMenu(Customer customer) {
            while (true) {
                Console.WriteLine("Please make a choice from the menu below:");
                Console.WriteLine("1: Display your user information");
                Console.WriteLine("2: Select a list of restaurants to order from");
                Console.WriteLine("3: See the status of your orders");
                Console.WriteLine("4: Rate a restaurant you've ordered from");
                Console.WriteLine("5: Log out");
                Console.WriteLine("Please enter a choice between 1 and 5:");

                if (!int.TryParse(Console.ReadLine(), out int choice) || choice < 1 || choice > 5) {
                    Console.WriteLine("Invalid choice.");
                    continue;
                }

                switch (choice) {
                    case 1:
                        ShowData(customer);
                        break;
                    case 2:
                        RestaurantSort(customer);
                        break;
                    case 3:
                        ViewOrders();
                        break;
                    case 4:
                        RateRestaurant();
                        break;
                    case 5:
                        Console.WriteLine("You are now logged out.");
                        return;
                }
            }
        }

        private static void ShowData(Customer customer) {
            Console.WriteLine("Your user details are as follows:");
            Console.WriteLine($"Name: {customer.Name}");
            Console.WriteLine($"Age: {customer.Age}");
            Console.WriteLine($"Email: {customer.Email}");
            Console.WriteLine($"Mobile: {customer.Phone}");
            Console.WriteLine($"Location: {customer.Location.x},{customer.Location.y}");
            Console.WriteLine($"You've made {customer.ordersMade} order(s) and spent a total of ${customer.moneySpent:F2} here.");
        }

        public struct CustomerLocation {
            public int x;
            public int y;

            public CustomerLocation(int x, int y) {
                this.x = x;
                this.y = y;
            }
        }
        public CustomerLocation Location { get; set; }

        private static List<Restaurant> restaurants = new List<Restaurant> {
            new Restaurant { Name = "Pizza Place", Style = "Italian", AverageRating = 4.5, x = 5, y = 10 },
            new Restaurant { Name = "Sushi World", Style = "Japanese", AverageRating = 4.8, x = 12, y = 3 },
            new Restaurant { Name = "Burger Barn", Style = "American", AverageRating = 4.2, x = 8, y = 8 }
        };

        private static void RestaurantSort(Customer customer) {
            Console.WriteLine("How would you like the list of restaurants ordered?");
            Console.WriteLine("1: Sorted alphabetically by name");
            Console.WriteLine("2: Sorted by distance");
            Console.WriteLine("3: Sorted by style");
            Console.WriteLine("4: Sorted by average rating");
            Console.WriteLine("5: Return to the previous menu");
            Console.WriteLine("Please enter a choice between 1 and 5:");

            int choice;
            while (!int.TryParse(Console.ReadLine(), out choice) || choice < 1 || choice > 5) {
                Console.WriteLine("Invalid choice.");
            }
            
            List<Restaurant> sorted = new List<Restaurant>(restaurants);

            switch (choice) {
                case 1:
                    sorted = sorted.OrderBy(r => r.Name).ToList();
                    break;
                case 2:
                    sorted = sorted.OrderBy(r =>
                        Math.Sqrt(Math.Pow(r.x - customer.Location.x, 2) + Math.Pow(r.y - customer.Location.y, 2))
                    ).ToList();
                    break;
                case 3:
                    sorted = sorted.OrderBy(r => r.Style).ToList();
                    break;
                case 4:
                    sorted = sorted.OrderByDescending(r => r.AverageRating).ToList();
                    break;
                case 5:
                    return;
            }

            foreach (var r in sorted) {
                Console.WriteLine($"{r.Name} | Style: {r.Style} | Rating: {r.AverageRating:F1} | Location: {r.x},{r.y}");
            }
        }

        private static void ViewOrders() {
            Console.WriteLine("You have not placed any orders."); // CHANGE WHEN ORDERS ADDED

        }

        private static void RateRestaurant() { // CHANGE WHEN ORDERS ADDED
            Console.WriteLine("Select a previous order to rate the restaurant it came from:");
            Console.WriteLine("1: Return to the previous menu");
            Console.WriteLine("Please enter a choice between 1 and 1:");
            while (true) {
                string input = Console.ReadLine();
                if (input == "1") {
                    return;
                }
                Console.WriteLine("Invalid choice.");
            }

        }

        public int ordersMade = 0;
        public decimal moneySpent = 0;
    }
}