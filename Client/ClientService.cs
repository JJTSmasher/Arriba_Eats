namespace Arriba_Eats {
    // MenuItem struct containing item name and price.
    public struct MenuItem(string name, decimal price)
    {
        public string Name = name;
        public decimal Price = price;
    }

    static class ClientService {
        public static void ClientMenu(Client client) {
            while (true) {
                // Display menu options.
                Console.WriteLine("Please make a choice from the menu below:");
                Console.WriteLine("1: Display your user information");
                Console.WriteLine("2: Add item to restaurant menu");
                Console.WriteLine("3: See current orders");
                Console.WriteLine("4: Start cooking order");
                Console.WriteLine("5: Finish cooking order");
                Console.WriteLine("6: Handle deliverers who have arrived");
                Console.WriteLine("7: Log out");
                Console.WriteLine("Please enter a choice between 1 and 7:");

                // Validate user input.
                if (!int.TryParse(Console.ReadLine(), out int choice) || choice < 1 || choice > 7) {
                    Console.WriteLine("Invalid choice.");
                    continue;
                }

                // Handle menu selection.
                switch (choice) {
                    case 1:
                        ShowData(client);
                        break;
                    case 2:
                        AddMenuItem(client);
                        break;
                    case 3:
                        CurrentOrders(client);
                        break;
                    case 4:
                        StartCookingOrder(client);
                        break;
                    case 5:
                        FinishCookingOrder(client);
                        break;
                    case 6:
                        HandleDelivererArrived(client);
                        break;
                    case 7:
                        Console.WriteLine("You are now logged out.");
                        return;
                }
            }
        }

        // Displays the client user and restaurant information.
        private static void ShowData(Client client) {
            Console.WriteLine("Your user details are as follows:");
            Console.WriteLine($"Name: {client.Name}");
            Console.WriteLine($"Age: {client.Age}");
            Console.WriteLine($"Email: {client.Email}");
            Console.WriteLine($"Mobile: {client.Phone}");
            Console.WriteLine($"Restaurant name: {client.RestaurantName}");

            // Display restaurant style if available.
            if (client.RestaurantStyles.Count > 0) {
                Console.WriteLine($"Restaurant style: {client.RestaurantStyles.Values.First()}");
            } else if (!string.IsNullOrEmpty(client.Style)) {
                Console.WriteLine($"Restaurant style: {client.Style}");
            } else {
                Console.WriteLine("No restaurant style defined."); // error handling if no style set
            }

            Console.WriteLine($"Restaurant location: {client.Location.x},{client.Location.y}");
        }

        // add new item to restaurant.
        private static void AddMenuItem(Client client) {
            Console.WriteLine("This is your restaurant's current menu:");
            foreach (var item in client.MenuItems) {
                Console.WriteLine($"  ${item.Price:F2} {item.Name}");
            }

            // Get item name.
            string itemName;
            while (true) {
                Console.WriteLine("Please enter the name of the new item (blank to cancel):");
                itemName = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(itemName)) {
                    return;
                }
                break;
            }

            // Get item price.
            decimal itemPrice;
            while (true) {
                Console.WriteLine("Please enter the price of the new item (without the $):");
                string priceInput = Console.ReadLine();
                if (decimal.TryParse(priceInput, out itemPrice) && itemPrice > 0 && itemPrice <= 999.99m) {
                    break;
                }
                Console.WriteLine("Invalid price.");
            }

            // Add the new menu item to the client's menu.
            client.MenuItems.Add(new MenuItem(itemName, itemPrice));
            Console.WriteLine($"Successfully added {itemName} (${itemPrice:F2}) to menu.");
        }

        // Displays all current orders for the client's restaurant.
        private static void CurrentOrders(Client client) {
            // Get all orders for this restaurant that are not delivered.
            var orders = Login.users
                .OfType<Customer>()
                .SelectMany(c => c.Orders, (c, o) => new { Customer = c, Order = o })
                .Where(x => x.Order.RestaurantName == client.RestaurantName && x.Order.Status != "Delivered")
                .ToList();

            if (orders.Count == 0) {
                Console.WriteLine("Your restaurant has no current orders.");
                return;
            }

            // Display each order and its items.
            foreach (var entry in orders) {
                Console.WriteLine($"Order #{entry.Order.OrderID} for {entry.Customer.Name}: {entry.Order.Status}");
                var grouped = entry.Order.Items.GroupBy(i => i.Name);
                foreach (var group in grouped) {
                    Console.WriteLine($"{group.Count()} x {group.Key}");
                }
                Console.WriteLine();
            }
        }

        // Allows the client to mark an order as "Cooking".
        private static void StartCookingOrder(Client client) {
            // Get all orders for this restaurant that are "Ordered".
            var orders = Login.users
                .OfType<Customer>()
                .SelectMany(c => c.Orders, (c, o) => new { Customer = c, Order = o })
                .Where(x => x.Order.RestaurantName == client.RestaurantName && x.Order.Status == "Ordered")
                .ToList();

            if (orders.Count == 0) {
                Console.WriteLine("There are no orders to start cooking.");
                return;
            }

            // Display orders to choose from.
            Console.WriteLine("Select an order once you are ready to start cooking:");
            int idx = 1;
            foreach (var entry in orders) {
                Console.WriteLine($"{idx}: Order #{entry.Order.OrderID} for {entry.Customer.Name}");
                idx++;
            }
            Console.WriteLine($"{idx}: Return to the previous menu");
            Console.WriteLine($"Please enter a choice between 1 and {idx}:");

            // Get user choice.
            int selection;
            while (!int.TryParse(Console.ReadLine(), out selection) || selection < 1 || selection > idx) {
                Console.WriteLine("Invalid choice.");
            }
            if (selection == idx) return;

            // Mark the chosen order as "Cooking".
            var chosen = orders[selection - 1];
            chosen.Order.Status = "Cooking";
            Console.WriteLine($"Order #{chosen.Order.OrderID} is now marked as cooking. Please prepare the order, then mark it as finished cooking:");
            var grouped = chosen.Order.Items.GroupBy(i => i.Name);
            foreach (var group in grouped) {
                Console.WriteLine($"{group.Count()} x {group.Key}");
            }
            Console.WriteLine();
        }

        // Allows the client to mark an order as "Cooked".
        private static void FinishCookingOrder(Client client) {
            // Get all orders for this restaurant that are "Cooking".
            var orders = Login.users
                .OfType<Customer>()
                .SelectMany(c => c.Orders, (c, o) => new { Customer = c, Order = o })
                .Where(x => x.Order.RestaurantName == client.RestaurantName && x.Order.Status == "Cooking")
                .ToList();

            if (orders.Count == 0) {
                Console.WriteLine("There are no orders to finish cooking.");
                return;
            }

            // Display orders to choose from.
            Console.WriteLine("Select an order once you have finished preparing it:");
            int idx = 1;
            foreach (var entry in orders) {
                Console.WriteLine($"{idx}: Order #{entry.Order.OrderID} for {entry.Customer.Name}");
                idx++;
            }
            Console.WriteLine($"{idx}: Return to the previous menu");
            Console.WriteLine($"Please enter a choice between 1 and {idx}:");

            // Get user choice.
            int selection;
            while (!int.TryParse(Console.ReadLine(), out selection) || selection < 1 || selection > idx) {
                Console.WriteLine("Invalid choice.");
            }
            if (selection == idx) return;

            // Mark the chosen order as "Cooked".
            var chosen = orders[selection - 1];
            chosen.Order.Status = "Cooked";
            Console.WriteLine($"Order #{chosen.Order.OrderID} is now ready for collection.");

            // Find the deliverer assigned to this order.
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

        // Allows the client to handle orders that the deliverer has arrived to collect.
        private static void HandleDelivererArrived(Client client) {
            // Find all orders that are "Cooked" and have a deliverer with status "Arrived".
            var waitingOrders = Login.users
                .OfType<Customer>()
                .SelectMany(c => c.Orders, (c, o) => new { Customer = c, Order = o })
                .Where(x => x.Order.RestaurantName == client.RestaurantName && x.Order.Status == "Cooked")
                .Select(x => {
                    var deliverer = Login.users
                        .OfType<Deliverer>()
                        .FirstOrDefault(d =>
                            d.orderDeliverStatus.ContainsKey(x.Order.OrderID) &&
                            d.orderDeliverStatus[x.Order.OrderID] == "Arrived"
                        );

                    return new { x.Customer, x.Order, Deliverer = deliverer };
                })
                .Where(x => x.Deliverer != null)
                .ToList();

            Console.WriteLine("These deliverers have arrived and are waiting to collect orders.");
            Console.WriteLine("Select an order to indicate that the deliverer has collected it:");
            int idx = 1;
            foreach (var entry in waitingOrders) { // display each order
                Console.WriteLine($"{idx}: Order #{entry.Order.OrderID} for {entry.Customer.Name} (Deliverer licence plate: {entry.Deliverer.licencePlate}) (Order status: {entry.Order.Status})");
                idx++;
            }
            Console.WriteLine($"{idx}: Return to the previous menu");
            Console.WriteLine($"Please enter a choice between 1 and {idx}:");

            // Get user choice.
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

            // Mark the order and deliverer as "Being Delivered".
            chosen.Order.Status = "Being Delivered";
            chosen.Deliverer.orderDeliverStatus[chosen.Order.OrderID] = "Being Delivered";
            Console.WriteLine($"Order #{chosen.Order.OrderID} is now marked as being delivered.");
        }

        // Updates the restaurant's average rating based on reviews.
        public static void UpdateRestaurantRating(string restaurantName) {
            var client = Login.users.OfType<Client>().FirstOrDefault(c => c.RestaurantName == restaurantName);
            if (client == null) return;

            var reviews = Login.Reviews.Where(r => r.RestaurantName == restaurantName).ToList();
            if (reviews.Count == 0) {
                client.RestaurantRating = 0;
            } else {
                client.RestaurantRating = (decimal)reviews.Average(r => r.Rating);
            }
        }
    }
}