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
                        Customer.ShowData(customer);
                        break;
                    case 2:
                        RestaurantSort();
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

        private static void RestaurantSort() {
            Console.WriteLine("How would you like the list of restaurants ordered?");
            Console.WriteLine("1: Sorted alphabetically by name");
            Console.WriteLine("2: Sorted by distance");
            Console.WriteLine("3: Sorted by style");
            Console.WriteLine("4: Sorted by average rating");
            Console.WriteLine("5: Return to the previous menu");
            Console.WriteLine("Please enter a choice between 1 and 5:");
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