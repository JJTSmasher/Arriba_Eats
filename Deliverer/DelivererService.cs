namespace Arriba_Eats {
    static class DelivererService {
        public static void DelivererMenu(Deliverer deliverer) {
            while (true) {
                Console.WriteLine("Please make a choice from the menu below:");
                Console.WriteLine("1: Display your user information");
                Console.WriteLine("2: List orders available to deliver");
                Console.WriteLine("3: Arrived at restaurant to pick up order");
                Console.WriteLine("4: Mark this delivery as complete");
                Console.WriteLine("5: Log out");
                Console.WriteLine("Please enter a choice between 1 and 5:");

                // Validate user input.
                if (!int.TryParse(Console.ReadLine(), out int choice) || choice < 1 || choice > 5) {
                    Console.WriteLine("Invalid choice.");
                    continue;
                }

                // Handle menu selection.
                switch (choice) {
                    case 1:
                        ShowData(deliverer);
                        break;
                    case 2:
                        AvailableOrders(deliverer);
                        break;
                    case 3:
                        ArrivedAtRestaurant(deliverer);
                        break;
                    case 4:
                        MarkDeliveryComplete(deliverer);
                        break;
                    case 5:
                        Console.WriteLine("You are now logged out.");
                        return;
                }
            }
        }

        // Displays the deliverer information and current delivery details.
        private static void ShowData(Deliverer deliverer) {
            Console.WriteLine("Your user details are as follows:");
            Console.WriteLine($"Name: {deliverer.Name}");
            Console.WriteLine($"Age: {deliverer.Age}");
            Console.WriteLine($"Email: {deliverer.Email}");
            Console.WriteLine($"Mobile: {deliverer.Phone}");
            Console.WriteLine($"Licence plate: {deliverer.licencePlate}");

            // Show current delivery information.
            if (deliverer.orderDeliverStatus.Count > 0) {
                Console.WriteLine("Current delivery:");
                foreach (var kv in deliverer.orderDeliverStatus) {
                    int orderId = kv.Key;

                    // Find customer and order for this delivery.
                    var customer = Login.users.OfType<Customer>().FirstOrDefault(c => c.Orders.Any(o => o.OrderID == orderId));
                    var order = customer?.Orders.FirstOrDefault(o => o.OrderID == orderId);
                    var client = Login.users.OfType<Client>().FirstOrDefault(cl => cl.RestaurantName == order?.RestaurantName);

                    if (order != null && customer != null && client != null) {
                        string clientLoc = $"{client.Location.x},{client.Location.y}";
                        string customerLoc = $"{customer.Location.x},{customer.Location.y}";

                        Console.WriteLine($"Order #{order.OrderID} from {client.RestaurantName} at {clientLoc}.");
                        Console.WriteLine($"To be delivered to {customer.Name} at {customerLoc}.");
                    }
                }
            }
        }

        // Lists all available orders for delivery and allows the deliverer to accept one.
        private static void AvailableOrders(Deliverer deliverer) {
            // Prevent accepting a new order if one is already in progress.
            if (deliverer.orderDeliverStatus.Any(kv => kv.Value != "Delivered" && kv.Value != "Completed")) {
                Console.WriteLine("You have already selected an order for delivery.");
                return;
            }

            // Prompt for deliverer's current location.
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

            // Find all available orders that are not already taken.
            var availableOrders = new List<(Order order, Customer customer, Client client)>();
            foreach (var user in Login.users.OfType<Customer>()) {
                foreach (var order in user.Orders) {
                    bool taken = Login.users
                        .OfType<Deliverer>()
                        .Any(d => d.orderDeliverStatus.ContainsKey(order.OrderID) && d.orderDeliverStatus[order.OrderID] != "Delivered" && d.orderDeliverStatus[order.OrderID] != "Completed");
                    if (!taken && (order.Status == "Ordered" || order.Status == "Cooking" || order.Status == "Cooked")) {
                        var client = Login.users
                            .OfType<Client>()
                            .FirstOrDefault(c => c.RestaurantName == order.RestaurantName);
                        if (client != null) {
                            availableOrders.Add((order, user, client));
                        }
                    }
                }
            }

            // Display available orders for selection.
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
                orderChoices.Add((order.OrderID, client.RestaurantName, rx, ry, customer.Name, cx, cy, dist));
                Console.WriteLine($"{idx,-3}: {order.OrderID,-6} {client.RestaurantName,-20} {rx},{ry,-6} {customer.Name,-15} {cx},{cy,-6} {dist,-4}");
                idx++;
            }
            Console.WriteLine($"{idx,-3}: Return to the previous menu");
            Console.WriteLine($"Please enter a choice between 1 and {idx}:");

            // Get user choice.
            int selection;
            while (!int.TryParse(Console.ReadLine(), out selection) || selection < 1 || selection > idx) {
                Console.WriteLine("Invalid choice.");
            }
            if (selection == idx) {
                return;
            }

            // Mark the chosen order as accepted by this deliverer.
            var chosen = orderChoices[selection - 1];
            deliverer.orderDeliverStatus[chosen.orderId] = "Accepted";
            Console.WriteLine($"Thanks for accepting the order. Please head to {chosen.restaurantName} at {chosen.rx},{chosen.ry} to pick it up.");
        }

        // Allows the deliverer to indicate they have arrived at the restaurant.
        private static void ArrivedAtRestaurant(Deliverer deliverer) {
            // Find the current order in progress.
            var current = deliverer.orderDeliverStatus.FirstOrDefault(kv => kv.Value == "Accepted" || kv.Value == "Arrived" || kv.Value == "Being Delivered");
            if (current.Key == 0) {
                Console.WriteLine("You have not yet accepted an order.");
                return;
            }
            int orderId = current.Key;
            string status = current.Value;

            // Prevent marking as arrived if already picked up or already marked as arrived.
            if (status == "Being Delivered") {
                Console.WriteLine("You have already picked up this order.");
                return;
            }
            if (status == "Arrived") {
                Console.WriteLine("You already indicated that you have arrived at this restaurant.");
                return;
            }

            // Find the customer, order, and client for this delivery.
            var customer = Login.users.OfType<Customer>().FirstOrDefault(c => c.Orders.Any(o => o.OrderID == orderId));
            var order = customer?.Orders.FirstOrDefault(o => o.OrderID == orderId);
            var client = Login.users.OfType<Client>().FirstOrDefault(cl => cl.RestaurantName == order?.RestaurantName);

            if (order == null || customer == null || client == null) {
                Console.WriteLine("Order information could not be found.");
                return;
            }

            // Mark the deliverer's status as "Arrived".
            deliverer.orderDeliverStatus[orderId] = "Arrived";
            Console.WriteLine($"Thanks. We have informed {client.RestaurantName} that you have arrived and are ready to pick up order #{order.OrderID}.");
            Console.WriteLine("Please show the staff this screen as confirmation.");

            // Notify if the order is not ready yet.
            if (order.Status == "Ordered" || order.Status == "Cooking") {
                Console.WriteLine("The order is still being prepared, so please wait patiently until it is ready.");
            }
            Console.WriteLine($"When you have the order, please deliver it to {customer.Name} at {customer.Location.x},{customer.Location.y}.");
        }

        // Allows the deliverer to mark the delivery as complete.
        private static void MarkDeliveryComplete(Deliverer deliverer) {
            // Find the current order being delivered.
            var current = deliverer.orderDeliverStatus.FirstOrDefault(kv => kv.Value == "Being Delivered");
            if (current.Key == 0) {
                if (deliverer.orderDeliverStatus.Any(kv => kv.Value == "Accepted" || kv.Value == "Arrived")) {
                    Console.WriteLine("You have not yet picked up this order.");
                } else {
                    Console.WriteLine("You have not yet accepted an order.");
                }
                return;
            }
            int orderId = current.Key;

            // Find the customer and order for this delivery.
            var customer = Login.users.OfType<Customer>().FirstOrDefault(c => c.Orders.Any(o => o.OrderID == orderId));
            var order = customer?.Orders.FirstOrDefault(o => o.OrderID == orderId);

            if (order == null || customer == null) {
                Console.WriteLine("Order information could not be found.");
                return;
            }

            // Mark the order and deliverer's status as delivered.
            Console.WriteLine("Thank you for making the delivery.");
            order.Status = "Delivered";
            deliverer.orderDeliverStatus[orderId] = "Delivered";
        }
    }
}