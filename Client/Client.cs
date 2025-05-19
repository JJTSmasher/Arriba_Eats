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

        

        public List<MenuItem> menu = new List<MenuItem>();

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
                if (decimal.TryParse(priceInput, out itemPrice) && itemPrice > 0 && itemPrice <= 999.99m ) {
                    break;
                }
                Console.WriteLine("Invalid price.");
            }

            menu.Add(new MenuItem(itemName, itemPrice));
            Console.WriteLine($"Successfully added {itemName} (${itemPrice:F2}) to menu.");
        }

        private void CurrentOrders() {
            var orders = Login.users
                .OfType<Customer>()
                .SelectMany(c => c.Orders, (c, o) => new { Customer = c, Order = o })
                .Where(x => x.Order.RestaurantName == restaurantName && x.Order.Status != "Delivered")
                .ToList();

            if (orders.Count == 0) {
                Console.WriteLine("Your restaurant has no current orders.");
                return;
            }

            foreach (var entry in orders) {
                Console.WriteLine($"Order #{entry.Order.OrderID} for {entry.Customer.Name}: {entry.Order.Status}");
                var grouped = entry.Order.Items.GroupBy(i => i.Name);
                foreach (var group in grouped) {
                    Console.WriteLine($"{group.Count()} x {group.Key}");
                }
                Console.WriteLine();
            }
        }

        private void StartCookingOrder() {
            var orders = Login.users
                .OfType<Customer>()
                .SelectMany(c => c.Orders, (c, o) => new { Customer = c, Order = o })
                .Where(x => x.Order.RestaurantName == restaurantName && x.Order.Status == "Ordered")
                .ToList();

            if (orders.Count == 0) {
                Console.WriteLine("There are no orders to start cooking.");
                return;
            }

            Console.WriteLine("Select an order once you are ready to start cooking:");
            int idx = 1;
            foreach (var entry in orders) {
                Console.WriteLine($"{idx}: Order #{entry.Order.OrderID} for {entry.Customer.Name}");
                idx++;
            }
            Console.WriteLine($"{idx}: Return to the previous menu");
            Console.WriteLine($"Please enter a choice between 1 and {idx}:");

            int selection;
            while (!int.TryParse(Console.ReadLine(), out selection) || selection < 1 || selection > idx) {
                Console.WriteLine("Invalid choice.");
            }
            if (selection == idx) return;

            var chosen = orders[selection - 1];
            chosen.Order.Status = "Cooking";
            Console.WriteLine($"Order #{chosen.Order.OrderID} is now marked as cooking. Please prepare the order, then mark it as finished cooking:");
            var grouped = chosen.Order.Items.GroupBy(i => i.Name);
            foreach (var group in grouped) {
                Console.WriteLine($"{group.Count()} x {group.Key}");
            }
            Console.WriteLine();
        }

        private void FinishCookingOrder() {
            var orders = Login.users
                .OfType<Customer>()
                .SelectMany(c => c.Orders, (c, o) => new { Customer = c, Order = o })
                .Where(x => x.Order.RestaurantName == restaurantName && x.Order.Status == "Cooking")
                .ToList();

            if (orders.Count == 0) {
                Console.WriteLine("There are no orders to finish cooking.");
                return;
            }

            Console.WriteLine("Select an order once you have finished preparing it:");
            int idx = 1;
            foreach (var entry in orders) {
                Console.WriteLine($"{idx}: Order #{entry.Order.OrderID} for {entry.Customer.Name}");
                idx++;
            }
            Console.WriteLine($"{idx}: Return to the previous menu");
            Console.WriteLine($"Please enter a choice between 1 and {idx}:");

            int selection;
            while (!int.TryParse(Console.ReadLine(), out selection) || selection < 1 || selection > idx) {
                Console.WriteLine("Invalid choice.");
            }
            if (selection == idx) return;

            var chosen = orders[selection - 1];
            chosen.Order.Status = "Cooked";
            Console.WriteLine($"Order #{chosen.Order.OrderID} is now ready for collection.");

            var deliverer = Login.users
                .OfType<Deliverer>()
                .FirstOrDefault(d => d.orderDeliverStatus.ContainsKey(chosen.Order.OrderID) && d.orderDeliverStatus[chosen.Order.OrderID] != "Delivered" && d.orderDeliverStatus[chosen.Order.OrderID] != "Completed");

            if (deliverer == null) {
                Console.WriteLine("No deliverer has been assigned yet.");
            } else {
                string delivererStatus = deliverer.orderDeliverStatus[chosen.Order.OrderID];
                if (delivererStatus == "Arrived") {
                    Console.WriteLine($"Please take it to the deliverer with licence plate {deliverer.licencePlate}, who is waiting to collect it.");
                } else {
                    Console.WriteLine($"The deliverer with licence plate {deliverer.licencePlate} will be arriving soon to collect it.");
                }
            }
            Console.WriteLine();
        }

        private void HandleDelivererArrived() {
            var waitingOrders = Login.users
                .OfType<Customer>()
                .SelectMany(c => c.Orders, (c, o) => new { Customer = c, Order = o })
                .Where(x => x.Order.RestaurantName == restaurantName && x.Order.Status != "Being Delivered" && x.Order.Status != "Delivered")
                .Select(x => {
                    var deliverer = Login.users
                        .OfType<Deliverer>()
                        .FirstOrDefault(d => d.orderDeliverStatus.ContainsKey(x.Order.OrderID) && d.orderDeliverStatus[x.Order.OrderID] == "Arrived");
                    return new { x.Customer, x.Order, Deliverer = deliverer };
                })
                .Where(x => x.Deliverer != null)
                .ToList();

            Console.WriteLine("These deliverers have arrived and are waiting to collect orders.");
            Console.WriteLine("Select an order to indicate that the deliverer has collected it:");
            int idx = 1;
            foreach (var entry in waitingOrders) {
                Console.WriteLine($"{idx}: Order #{entry.Order.OrderID} for {entry.Customer.Name} (Deliverer licence plate: {entry.Deliverer.licencePlate}) (Order status: {entry.Order.Status})");
                idx++;
            }
            Console.WriteLine($"{idx}: Return to the previous menu");
            Console.WriteLine($"Please enter a choice between 1 and {idx}:");

            int selection;
            while (!int.TryParse(Console.ReadLine(), out selection) || selection < 1 || selection > idx) {
                Console.WriteLine("Invalid choice.");
            }
            if (selection == idx) return;

            var chosen = waitingOrders[selection - 1];
            if (chosen.Order.Status != "Cooked") {
                Console.WriteLine("This order has not yet been cooked.");
                return;
            }

            chosen.Order.Status = "Being Delivered";
            chosen.Deliverer.orderDeliverStatus[chosen.Order.OrderID] = "Being Delivered";
            Console.WriteLine($"Order #{chosen.Order.OrderID} is now marked as being delivered.");
        }

        public static void UpdateRestaurantRating(string restaurantName)
        {
            var client = Login.users.OfType<Client>().FirstOrDefault(c => c.restaurantName == restaurantName);
            if (client == null) return;

            var reviews = Login.Reviews.Where(r => r.RestaurantName == restaurantName).ToList();
            if (reviews.Count == 0)
            {
                client.restaurantRating = 0;
            }
            else
            {
                client.restaurantRating = (decimal)reviews.Average(r => r.Rating);
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