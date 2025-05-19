namespace Arriba_Eats {
    class Deliverer : User {
        public string licencePlate { get; set; }
        public Deliverer(string email, string password, string role, string name, string phone, int age)
            : base(email, password, role, name, phone, age) {}

        public static void DelivererMenu(Deliverer deliverer) {
            while (true) {
                Console.WriteLine("Please make a choice from the menu below:");
                Console.WriteLine("1: Display your user information");
                Console.WriteLine("2: List orders available to deliver");
                Console.WriteLine("3: Arrived at restaurant to pick up order");
                Console.WriteLine("4: Mark this delivery as complete");
                Console.WriteLine("5: Log out");
                Console.WriteLine("Please enter a choice between 1 and 5:");

                if (!int.TryParse(Console.ReadLine(), out int choice) || choice < 1 || choice > 5) {
                    Console.WriteLine("Invalid choice.");
                    continue;
                }

                switch (choice) {
                    case 1:
                        deliverer.ShowData();
                        break;
                    case 2:
                        deliverer.AvailableOrders();
                        break;
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

        private void ShowData() {
            Console.WriteLine("Your user details are as follows:");
            Console.WriteLine($"Name: {Name}");
            Console.WriteLine($"Age: {Age}");
            Console.WriteLine($"Email: {Email}");
            Console.WriteLine($"Mobile: {Phone}");
            Console.WriteLine($"Licence plate: {licencePlate}");

            if (orderDeliverStatus.Count > 0) {
                Console.WriteLine("Current delivery:");
                foreach (var kv in orderDeliverStatus) {
                    int orderId = kv.Key;
                    string status = kv.Value;
                    
                    var customer = Login.users.OfType<Customer>().FirstOrDefault(c => c.Orders.Any(o => o.OrderID == orderId));
                    var order = customer?.Orders.FirstOrDefault(o => o.OrderID == orderId);
                    var client = Login.users.OfType<Client>().FirstOrDefault(cl => cl.restaurantName == order?.RestaurantName);

                    if (order != null && customer != null && client != null) {
                        Console.WriteLine($"Order: #{order.OrderID} from {client.restaurantName} at {client.Location}");
                        Console.WriteLine($"To be delivered to {customer.Name} at {customer.Location}");
                        Console.WriteLine($"Status: {status}");
                    }
                }
            }
        }

        private void AvailableOrders() {
            if (orderDeliverStatus.Any(kv => kv.Value != "Delivered" && kv.Value != "Completed")) {
                Console.WriteLine("You have already selected an order for delivery.");
                return;
            }

            int delivererX, delivererY;
            while (true) {
                Console.WriteLine("Please enter your location (in the form of X,Y):");
                string input = Console.ReadLine();
                var parts = input.Split(',');
                if (parts.Length == 2 && int.TryParse(parts[0], out delivererX) && int.TryParse(parts[1], out delivererY)) {
                    break;
                }
                Console.WriteLine("Invalid location.");
            }

            var availableOrders = new List<(Order order, Customer customer, Client client)>();
            foreach (var user in Login.users.OfType<Customer>()) {
                foreach (var order in user.Orders) {
                    bool taken = Login.users
                        .OfType<Deliverer>()
                        .Any(d => d.orderDeliverStatus.ContainsKey(order.OrderID) && d.orderDeliverStatus[order.OrderID] != "Delivered" && d.orderDeliverStatus[order.OrderID] != "Completed");
                    if (!taken && order.Status == "Ordered") {
                        var client = Login.users
                            .OfType<Client>()
                            .FirstOrDefault(c => c.restaurantName == order.RestaurantName);
                        if (client != null) {
                            availableOrders.Add((order, user, client));
                        }
                    }
                }
            }

            if (availableOrders.Count == 0) {
                Console.WriteLine("There are no orders available for delivery at this time.");
                return;
            }

            Console.WriteLine("The following orders are available for delivery. Select an order to accept it:");
            Console.WriteLine("   {0,-6} {1,-20} {2,-8} {3,-15} {4,-8} {5,-4}", "Order", "Restaurant Name", "Loc", "Customer Name", "Loc", "Dist");
            int idx = 1;
            var orderChoices = new List<(int orderId, string restaurantName, int rx, int ry, string customerName, int cx, int cy, int dist)>();
            foreach (var (order, customer, client) in availableOrders.OrderBy(o => o.order.OrderID)) {
                int rx = client.Location.x;
                int ry = client.Location.y;
                int cx = customer.Location.x;
                int cy = customer.Location.y;
                int dist = Math.Abs(delivererX - rx) + Math.Abs(delivererY - ry) + Math.Abs(rx - cx) + Math.Abs(ry - cy);
                orderChoices.Add((order.OrderID, client.restaurantName, rx, ry, customer.Name, cx, cy, dist));
                Console.WriteLine($"{idx,-3}: {order.OrderID,-6} {client.restaurantName,-20} {rx},{ry,-6} {customer.Name,-15} {cx},{cy,-6} {dist,-4}");
                idx++;
            }
            Console.WriteLine($"{idx,-3}: Return to the previous menu");
            Console.WriteLine($"Please enter a choice between 1 and {idx}:");

            int selection;
            while (!int.TryParse(Console.ReadLine(), out selection) || selection < 1 || selection > idx) {
                Console.WriteLine("Invalid choice.");
            }
            if (selection == idx) {
                return;
            }

            var chosen = orderChoices[selection - 1];
            orderDeliverStatus[chosen.orderId] = "Accepted";
            Console.WriteLine($"Thanks for accepting the order. Please head to {chosen.restaurantName} at {chosen.rx},{chosen.ry} to pick it up.");
        }
        
        public Dictionary<int, string> orderDeliverStatus = new Dictionary<int, string>();
        public bool delivererStatus;
    }
}