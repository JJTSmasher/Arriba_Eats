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
                        ViewOrders(customer);
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

        // Add this to store all orders for the customer
        public List<List<MenuItem>> orders = new List<List<MenuItem>>();

        private static void RestaurantSort(Customer customer) {
            // Get all clients
            List<Client> clients = Login.users
                .OfType<Client>()
                .Where(c => !string.IsNullOrEmpty(c.restaurantName))
                .ToList();

            // Build list of Restaurant objects
            List<Restaurant> restaurants = clients.Select(c => new Restaurant {
                Name = c.restaurantName,
                Style = c.restaurantStyles.Values.FirstOrDefault() ?? "",
                AverageRating = (double)c.restaurantRating,
                x = c.Location.x,
                y = c.Location.y,
                MenuItems = c.menu != null ? [.. c.menu] : new List<MenuItem>()
            }).ToList();

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

            // Sort based on user input
            switch (choice) {
                case 1:
                    restaurants = restaurants.OrderBy(r => r.Name).ToList();
                    break;
                case 2:
                    restaurants = restaurants.OrderBy(r =>
                        Math.Sqrt(Math.Pow(r.x - customer.Location.x, 2) + Math.Pow(r.y - customer.Location.y, 2))
                    ).ToList();
                    break;
                case 3:
                    restaurants = restaurants.OrderBy(r => r.Style).ToList();
                    break;
                case 4:
                    restaurants = restaurants.OrderByDescending(r => r.AverageRating).ToList();
                    break;
                case 5:
                    return;
            }

            // Table header
            Console.WriteLine("You can order from the following restaurants:");
            Console.WriteLine("{0,-3} {1,-20} {2,-10} {3,-13} {4,-15} {5,-6}", 
                "", "Restaurant Name", "Loc", "Dist", "Style", "Rating");

            // Table rows
            int index = 1;
            foreach (var r in restaurants) {
                int dist = Math.Abs(r.x - customer.Location.x) + Math.Abs(r.y - customer.Location.y);
                string ratingDisplay = r.AverageRating == 0 ? "-" : r.AverageRating.ToString("F1");
                Console.WriteLine("{0,-3}: {1,-20} {2,-10} {3,-13} {4,-15} {5,-6:F1}",
                    index, r.Name, $"{r.x},{r.y}", dist, r.Style, ratingDisplay);
                index++;
            }

            // Return to previous menu row
            Console.WriteLine("{0,-3}: {1}", index, "Return to the previous menu");
            Console.WriteLine($"Please enter a choice between 1 and {index}:");

            // Get user selection
            int selection;
            while (!int.TryParse(Console.ReadLine(), out selection) || selection < 1 || selection > index) {
                Console.WriteLine("Invalid choice.");
            }

            if (selection == index) {
                return;
            } else {
                // Show menu for the selected restaurant
                var selectedRestaurant = restaurants[selection - 1];
                Console.WriteLine($"\nPlacing order from {selectedRestaurant.Name}.");
                while (true) {
                    Console.WriteLine("1: See this restaurant's menu and place an order");
                    Console.WriteLine("2: See reviews for this restaurant");
                    Console.WriteLine("3: Return to main menu");
                    Console.WriteLine("Please enter a choice between 1 and 3:");

                    int subChoice;
                    while (!int.TryParse(Console.ReadLine(), out subChoice) || subChoice < 1 || subChoice > 3) {
                        Console.WriteLine("Invalid choice. Please enter 1, 2, or 3:");
                    }

                    switch (subChoice) {
                        case 1:
                            List<MenuItem> order = new List<MenuItem>();
                            decimal orderTotal = 0m;
                            bool ordering = true;
                            while (ordering)
                            {
                                Console.WriteLine($"\nCurrent order total: ${orderTotal:F2}");
                                if (selectedRestaurant.MenuItems != null && selectedRestaurant.MenuItems.Count > 0) {
                                    int menuIndex = 1;
                                    foreach (var item in selectedRestaurant.MenuItems) {
                                        Console.WriteLine($"{menuIndex}: ${item.Price:F2} {item.Name}");
                                        menuIndex++;
                                    }
                                    Console.WriteLine($"{menuIndex}: Complete order");
                                    Console.WriteLine($"{menuIndex + 1}: Cancel order");
                                    Console.WriteLine($"Please enter a choice between 1 and {menuIndex}:");

                                    int menuChoice;
                                    while (!int.TryParse(Console.ReadLine(), out menuChoice) || menuChoice < 1 || menuChoice > menuIndex + 1) {
                                        Console.WriteLine("Invalid choice.");
                                    }

                                    if (menuChoice == menuIndex) {
                                        // Complete order
                                        if (order.Count == 0) {
                                            Console.WriteLine("You have not selected any items.");
                                        } else {
                                            customer.orders.Add([.. order]);
                                            int orderNumber = customer.orders.Count;
                                            Console.WriteLine($"Your order has been placed. Your order number is #{orderNumber}.");
                                        }
                                        ordering = false;
                                    }
                                    else if (menuChoice == menuIndex + 1) {
                                        ordering = false;
                                    } else {
                                        var selectedItem = selectedRestaurant.MenuItems[menuChoice - 1];
                                        Console.WriteLine($"Adding {selectedItem.Name} to order.");
                                        Console.WriteLine("Please enter quantity (0 to cancel):");
                                        int quantity;
                                        while (!int.TryParse(Console.ReadLine(), out quantity) || quantity < 0) {
                                            Console.WriteLine("Invalid quantity.");
                                        }
                                        for (int i = 0; i < quantity; i++) {
                                            order.Add(selectedItem);
                                            orderTotal += selectedItem.Price;
                                        }
                                        Console.WriteLine($"Added {quantity}x {selectedItem.Name} to order.");
                                    }
                                } else {
                                    Console.WriteLine("No items available");
                                    ordering = false;
                                }
                            }
                            break;
                        case 2:

                            break;
                        case 3:
                            return;
                    }
                }
            }
        }


        private static void ViewOrders(Customer customer) {
            if (customer.orders.Count == 0) {
                Console.WriteLine("You have not placed any orders.");
                return;
            }
            Console.WriteLine("Your previous orders:");
            int orderNum = 1;
            foreach (var order in customer.orders) {
                Console.WriteLine($"Order #{orderNum}:");
                var grouped = order.GroupBy(i => i.Name);
                foreach (var group in grouped) {
                    decimal itemTotal = group.Count() * group.First().Price;
                    Console.WriteLine($"- {group.Key} x{group.Count()} (${itemTotal:F2})");
                }
                decimal total = order.Sum(i => i.Price);
                Console.WriteLine($"Total: ${total:F2}\n");
                orderNum++;
            }
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