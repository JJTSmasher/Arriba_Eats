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
        public List<Order> Orders { get; } = new List<Order>();

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
                    restaurants = restaurants
                        .OrderBy(r => Math.Abs(r.x - customer.Location.x) + Math.Abs(r.y - customer.Location.y))
                        .ThenBy(r => r.Name)
                        .ToList();
                    break;
                case 3:
                    string[] styleOrder = ["Italian", "French", "Chinese", "Japanese", "American", "Australian"];
                    restaurants = restaurants
                        .OrderBy(r => {
                            int idx = Array.IndexOf(styleOrder, r.Style);
                            return idx == -1 ? int.MaxValue : idx;
                        })
                        .ThenBy(r => r.Name)
                        .ToList();
                    break;
                case 4:
                    restaurants = restaurants
                        .OrderBy(r => r.AverageRating == 0 ? 1 : 0)
                        .ThenByDescending(r => r.AverageRating)
                        .ThenBy(r => r.Name)
                        .ToList();
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
                                    Console.WriteLine($"Please enter a choice between 1 and {menuIndex+1}:");

                                    int menuChoice;
                                    while (!int.TryParse(Console.ReadLine(), out menuChoice) || menuChoice < 1 || menuChoice > menuIndex + 1) {
                                        Console.WriteLine("Invalid choice.");
                                    }

                                    if (menuChoice == menuIndex) {
                                        // Complete order
                                        if (order.Count == 0) {
                                            Console.WriteLine("You have not selected any items.");
                                        } else {
                                            Order newOrder = new Order(Order.GlobalOrderCount + 1, selectedRestaurant.Name, new List<MenuItem>(order), orderTotal);
                                            customer.Orders.Add(newOrder);
                                            customer.ordersMade++;
                                            customer.moneySpent += orderTotal;
                                            Console.WriteLine($"Your order has been placed. Your order number is #{newOrder.OrderID}.");
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
                                        if (quantity == 0) {
                                        } else {
                                            for (int i = 0; i < quantity; i++) {
                                                order.Add(selectedItem);
                                                orderTotal += selectedItem.Price;
                                            }
                                            Console.WriteLine($"Added {quantity}x {selectedItem.Name} to order.");
                                        }
                                    }
                                } else {
                                    Console.WriteLine("No items available");
                                    ordering = false;
                                }
                            }
                            break;
                        case 2:
                            SeeReviewsForRestaurant(selectedRestaurant.Name);
                            break;
                        case 3:
                            return;
                    }
                }
            }
        }


        private static void ViewOrders(Customer customer) {
            if (customer.Orders.Count == 0) {
                Console.WriteLine("You have not placed any orders.");
                return;
            }
            foreach (var order in customer.Orders) {
                Console.WriteLine($"Order #{order.OrderID} from {order.RestaurantName}: {order.Status}");

                // Find the deliverer for this order, if any
                var deliveredBy = Login.users
                    .OfType<Deliverer>()
                    .FirstOrDefault(d => d.orderDeliverStatus.ContainsKey(order.OrderID) && d.orderDeliverStatus[order.OrderID] == "Delivered");

                if (deliveredBy != null) {
                    Console.WriteLine($"This order was delivered by {deliveredBy.Name} (licence plate: {deliveredBy.licencePlate})");
                }

                var grouped = order.Items.GroupBy(i => i.Name);
                foreach (var group in grouped) {
                    decimal itemTotal = group.Count() * group.First().Price;
                    Console.WriteLine($"{group.Count()} x {group.Key}");
                }
            }
        }

        private static void RateRestaurant()
        {
            if (Login.users.OfType<Customer>().FirstOrDefault(c => c.Email == User.CurrentUserEmail) is not Customer customer)
            {
                Console.WriteLine("Unable to find customer profile.");
                return;
            }

            var ratedOrderIds = Login.Reviews
                .Where(r => r.CustomerName == customer.Name)
                .Select(r => r.RestaurantName + "|" + r.Comment)
                .ToHashSet();

            var unratedOrders = customer.Orders
                .Where(o => o.Status == "Delivered" &&
                    !Login.Reviews.Any(r => r.CustomerName == customer.Name && r.RestaurantName == o.RestaurantName && r.Comment.Contains($"Order#{o.OrderID}")))
                .ToList();

            Console.WriteLine("Select a previous order to rate the restaurant it came from:");
            int idx = 1;
            foreach (var order in unratedOrders)
            {
                Console.WriteLine($"{idx}: Order #{order.OrderID} from {order.RestaurantName}");
                idx++;
            }
            Console.WriteLine($"{idx}: Return to the previous menu");
            Console.WriteLine($"Please enter a choice between 1 and {idx}:");

            int selection;
            while (!int.TryParse(Console.ReadLine(), out selection) || selection < 1 || selection > idx)
            {
                Console.WriteLine("Invalid choice.");
            }
            if (selection == idx) return;

            var selectedOrder = unratedOrders[selection - 1];
            Console.WriteLine($"You are rating order #{selectedOrder.OrderID} from {selectedOrder.RestaurantName}:");
            var grouped = selectedOrder.Items.GroupBy(i => i.Name);
            foreach (var group in grouped)
            {
                Console.WriteLine($"{group.Count()} x {group.Key}");
            }

            int rating;
            while (true)
            {
                Console.WriteLine("Please enter a rating for this restaurant (1-5, 0 to cancel):");
                if (int.TryParse(Console.ReadLine(), out rating) && rating >= 0 && rating <= 5)
                {
                    break;
                }
                Console.WriteLine("Invalid rating.");
            }
            if (rating == 0) return;

            Console.WriteLine("Please enter a comment to accompany this rating:");
            string comment = Console.ReadLine();

            Login.Reviews.Add(new Review(selectedOrder.RestaurantName, customer.Name, rating, comment));
            Client.UpdateRestaurantRating(selectedOrder.RestaurantName);
            Console.WriteLine($"Thank you for rating {selectedOrder.RestaurantName}.");
        }

        private static void SeeReviewsForRestaurant(string restaurantName) {
            var reviews = Login.Reviews
                .Where(r => r.RestaurantName == restaurantName)
                .ToList();

            if (reviews.Count == 0) {
                Console.WriteLine("No reviews have been left for this restaurant.");
                return;
            }

            foreach (var review in reviews) {
                Console.WriteLine($"Reviewer: {review.CustomerName}");
                Console.WriteLine($"Rating: {new string('*', review.Rating)}");
                Console.WriteLine($"Comment: {review.Comment}");
                Console.WriteLine();
            }
        }

        public int ordersMade = 0;
        public decimal moneySpent = 0;
    }
}