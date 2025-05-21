namespace Arriba_Eats {
    static class CustomerService {
        public static void CustomerMenu(Customer customer) {
            while (true) {
                UIFunctions.DisplayString("Please make a choice from the menu below:");
                UIFunctions.DisplayString("1: Display your user information");
                UIFunctions.DisplayString("2: Select a list of restaurants to order from");
                UIFunctions.DisplayString("3: See the status of your orders");
                UIFunctions.DisplayString("4: Rate a restaurant you've ordered from");
                UIFunctions.DisplayString("5: Log out");
                UIFunctions.DisplayString("Please enter a choice between 1 and 5:");

                // Validate user input.
                if (!int.TryParse(Console.ReadLine(), out int choice) || choice < 1 || choice > 5) {
                    UIFunctions.DisplayString("Invalid choice.");
                    continue;
                }

                // Handle menu selection.
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
                        UIFunctions.DisplayString("You are now logged out.");
                        return;
                }
            }
        }

        // Displays the customer information and order details.
        private static void ShowData(Customer customer) {
            UIFunctions.DisplayString("Your user details are as follows:");
            UIFunctions.DisplayString($"Name: {customer.Name}");
            UIFunctions.DisplayString($"Age: {customer.Age}");
            UIFunctions.DisplayString($"Email: {customer.Email}");
            UIFunctions.DisplayString($"Mobile: {customer.Phone}");
            UIFunctions.DisplayString($"Location: {customer.Location.x},{customer.Location.y}");
            UIFunctions.DisplayString($"You've made {customer.ordersMade} order(s) and spent a total of ${customer.moneySpent:F2} here.");
        }

        // Allows the customer to view and sort the list of restaurants, and place orders.
        private static void RestaurantSort(Customer customer) {
            // Get all clients' restaurants with a valid name.
            List<Client> restaurants = [.. Login.users
                .OfType<Client>()
                .Where(c => !string.IsNullOrEmpty(c.RestaurantName))];

            UIFunctions.DisplayString("How would you like the list of restaurants ordered?");
            UIFunctions.DisplayString("1: Sorted alphabetically by name");
            UIFunctions.DisplayString("2: Sorted by distance");
            UIFunctions.DisplayString("3: Sorted by style");
            UIFunctions.DisplayString("4: Sorted by average rating");
            UIFunctions.DisplayString("5: Return to the previous menu");
            UIFunctions.DisplayString("Please enter a choice between 1 and 5:");

            // Get and validate sorting choice.
            int choice;
            while (!int.TryParse(Console.ReadLine(), out choice) || choice < 1 || choice > 5) {
                UIFunctions.DisplayString("Invalid choice.");
            }

            // Sort the restaurant list based on user selection.
            switch (choice) {
                case 1:
                    restaurants = [.. restaurants.OrderBy(r => r.RestaurantName)];
                    break;
                case 2:
                    restaurants = [.. restaurants
                        .OrderBy(r => Math.Abs(r.Location.x - customer.Location.x) + Math.Abs(r.Location.y - customer.Location.y))
                        .ThenBy(r => r.RestaurantName)];
                    break;
                case 3:
                    string[] styleOrder = { "Italian", "French", "Chinese", "Japanese", "American", "Australian" };
                    restaurants = [.. restaurants
                        .OrderBy(r => {
                            string style = r.RestaurantStyles.Values.FirstOrDefault() ?? r.Style ?? "";
                            int idx = Array.IndexOf(styleOrder, style);
                            return idx == -1 ? int.MaxValue : idx;
                        })
                        .ThenBy(r => r.RestaurantName)];
                    break;
                case 4:
                    restaurants = [.. restaurants
                        .OrderByDescending(r => r.RestaurantRating > 0)
                        .ThenByDescending(r => r.RestaurantRating)
                        .ThenBy(r => r.RestaurantName)];
                    break;
                case 5:
                    return;
            }

            // Display the sorted restaurant table header.
            UIFunctions.DisplayString("You can order from the following restaurants:");
            UIFunctions.DisplayString(string.Format("{0,-3} {1,-20} {2,-10} {3,-13} {4,-15} {5,-6}",
                "", "Restaurant Name", "Loc", "Dist", "Style", "Rating"));

            // Display each restaurant as a row in the table.
            int index = 1;
            foreach (var r in restaurants) {
                int dist = Math.Abs(r.Location.x - customer.Location.x) + Math.Abs(r.Location.y - customer.Location.y);
                string style = r.RestaurantStyles.Values.FirstOrDefault() ?? r.Style ?? "";
                string ratingDisplay = r.RestaurantRating == 0 ? "-" : r.RestaurantRating.ToString("F1");
                UIFunctions.DisplayString(string.Format("{0,-3}: {1,-20} {2,-10} {3,-13} {4,-15} {5,-6}",
                    index, r.RestaurantName, $"{r.Location.x},{r.Location.y}", dist, style, ratingDisplay));
                index++;
            }

            // Display option to return to previous menu.
            UIFunctions.DisplayString(string.Format("{0,-3}: {1}", index, "Return to the previous menu"));
            UIFunctions.DisplayString($"Please enter a choice between 1 and {index}:");

            // Get user selection for a restaurant or return.
            int selection;
            while (!int.TryParse(Console.ReadLine(), out selection) || selection < 1 || selection > index) {
                UIFunctions.DisplayString("Invalid choice.");
            }

            if (selection == index) {
                return;
            } else {
                // Show menu for the selected restaurant.
                var selectedRestaurant = restaurants[selection - 1];
                UIFunctions.DisplayString($"\nPlacing order from {selectedRestaurant.RestaurantName}.");
                while (true) {
                    UIFunctions.DisplayString("1: See this restaurant's menu and place an order");
                    UIFunctions.DisplayString("2: See reviews for this restaurant");
                    UIFunctions.DisplayString("3: Return to main menu");
                    UIFunctions.DisplayString("Please enter a choice between 1 and 3:");

                    int subChoice;
                    while (!int.TryParse(Console.ReadLine(), out subChoice) || subChoice < 1 || subChoice > 3) {
                        UIFunctions.DisplayString("Invalid choice. Please enter 1, 2, or 3:");
                    }

                    switch (subChoice) {
                        case 1:
                            // Begin order placement process.
                            List<MenuItem> order = [];
                            decimal orderTotal = 0m;
                            bool ordering = true;
                            while (ordering)
                            {
                                UIFunctions.DisplayString($"\nCurrent order total: ${orderTotal:F2}");
                                if (selectedRestaurant.MenuItems != null && selectedRestaurant.MenuItems.Count > 0) {
                                    int menuIndex = 1;
                                    foreach (var item in selectedRestaurant.MenuItems) {
                                        UIFunctions.DisplayString($"{menuIndex}: ${item.Price:F2} {item.Name}");
                                        menuIndex++;
                                    }
                                    UIFunctions.DisplayString($"{menuIndex}: Complete order");
                                    UIFunctions.DisplayString($"{menuIndex + 1}: Cancel order");
                                    UIFunctions.DisplayString($"Please enter a choice between 1 and {menuIndex + 1}:");

                                    int menuChoice;
                                    while (!int.TryParse(Console.ReadLine(), out menuChoice) || menuChoice < 1 || menuChoice > menuIndex + 1) {
                                        UIFunctions.DisplayString("Invalid choice.");
                                    }

                                    if (menuChoice == menuIndex) {
                                        // Complete order
                                        if (order.Count == 0) {
                                            UIFunctions.DisplayString("You have not selected any items.");
                                        } else {
                                            Order newOrder = new(Order.GlobalOrderCount + 1, selectedRestaurant.RestaurantName, [.. order], orderTotal);
                                            customer.Orders.Add(newOrder);
                                            customer.ordersMade++;
                                            customer.moneySpent += orderTotal;
                                            UIFunctions.DisplayString($"Your order has been placed. Your order number is #{newOrder.OrderID}.");
                                        }
                                        ordering = false;
                                    }
                                    else if (menuChoice == menuIndex + 1) {
                                        ordering = false;
                                    } else {
                                        var selectedItem = selectedRestaurant.MenuItems[menuChoice - 1];
                                        UIFunctions.DisplayString($"Adding {selectedItem.Name} to order.");
                                        UIFunctions.DisplayString("Please enter quantity (0 to cancel):");
                                        int quantity;
                                        while (!int.TryParse(Console.ReadLine(), out quantity) || quantity < 0) {
                                            UIFunctions.DisplayString("Invalid quantity.");
                                        }
                                        if (quantity == 0) {
                                            // Item is not added.
                                        } else {
                                            for (int i = 0; i < quantity; i++) {
                                                order.Add(selectedItem);
                                                orderTotal += selectedItem.Price;
                                            }
                                            UIFunctions.DisplayString($"Added {quantity}x {selectedItem.Name} to order.");
                                        }
                                    }
                                } else {
                                    UIFunctions.DisplayString("No items available");
                                    ordering = false;
                                }
                            }
                            break;
                        case 2:
                            // Show reviews for the selected restaurant.
                            SeeReviewsForRestaurant(selectedRestaurant.RestaurantName);
                            break;
                        case 3:
                            return;
                    }
                }
            }
        }

        // Displays all orders placed by the customer and their status.
        private static void ViewOrders(Customer customer) {
            if (customer.Orders.Count == 0) {
                UIFunctions.DisplayString("You have not placed any orders.");
                return;
            }
            foreach (var order in customer.Orders) {
                UIFunctions.DisplayString($"Order #{order.OrderID} from {order.RestaurantName}: {order.Status}");

                // Find the deliverer for this order.
                var deliveredBy = Login.users
                    .OfType<Deliverer>()
                    .FirstOrDefault(d => d.orderDeliverStatus.ContainsKey(order.OrderID) && d.orderDeliverStatus[order.OrderID] == "Delivered");

                if (deliveredBy != null) {
                    UIFunctions.DisplayString($"This order was delivered by {deliveredBy.Name} (licence plate: {deliveredBy.licencePlate})");
                }

                var grouped = order.Items.GroupBy(i => i.Name);
                foreach (var group in grouped) {
                    decimal itemTotal = group.Count() * group.First().Price;
                    UIFunctions.DisplayString($"{group.Count()} x {group.Key}");
                }
            }
        }

        // Allows the customer to rate a restaurant after receiving a delivered order.
        private static void RateRestaurant()
        {
            // Find the current customer by email.
            if (Login.users.OfType<Customer>().FirstOrDefault(c => c.Email == User.CurrentUserEmail) is not Customer customer)
            {
                UIFunctions.DisplayString("Unable to find customer profile.");
                return;
            }

            // Find all delivered orders that have not yet been rated by this customer.
            var unratedOrders = customer.Orders
                .Where(o => o.Status == "Delivered" &&
                    !Login.Reviews.Any(r =>
                        r.CustomerEmail == customer.Email &&
                        r.RestaurantName == o.RestaurantName &&
                        r.OrderID == o.OrderID))
                .ToList();

            UIFunctions.DisplayString("Select a previous order to rate the restaurant it came from:");
            int idx = 1;
            foreach (var order in unratedOrders)
            {
                UIFunctions.DisplayString($"{idx}: Order #{order.OrderID} from {order.RestaurantName}");
                idx++;
            }
            UIFunctions.DisplayString($"{idx}: Return to the previous menu");
            UIFunctions.DisplayString($"Please enter a choice between 1 and {idx}:");

            int selection;
            while (!int.TryParse(Console.ReadLine(), out selection) || selection < 1 || selection > idx)
            {
                UIFunctions.DisplayString("Invalid choice.");
            }
            if (selection == idx) return;

            var selectedOrder = unratedOrders[selection - 1];
            UIFunctions.DisplayString($"You are rating order #{selectedOrder.OrderID} from {selectedOrder.RestaurantName}:");
            var grouped = selectedOrder.Items.GroupBy(i => i.Name);
            foreach (var group in grouped)
            {
                UIFunctions.DisplayString($"{group.Count()} x {group.Key}");
            }

            // Get rating from user.
            int rating;
            while (true)
            {
                UIFunctions.DisplayString("Please enter a rating for this restaurant (1-5, 0 to cancel):");
                if (int.TryParse(Console.ReadLine(), out rating) && rating >= 0 && rating <= 5)
                {
                    break;
                }
                UIFunctions.DisplayString("Invalid rating.");
            }
            if (rating == 0) return;

            // Get comment from user.
            UIFunctions.DisplayString("Please enter a comment to accompany this rating:");
            string comment = Console.ReadLine();

            // Add the review to the system.
            Login.Reviews.Add(new Review(
                selectedOrder.RestaurantName,
                customer.Name,
                customer.Email,
                selectedOrder.OrderID,
                rating,
                comment
            ));
            // Update the restaurant's average rating.
            ClientService.UpdateRestaurantRating(selectedOrder.RestaurantName);
            UIFunctions.DisplayString($"Thank you for rating {selectedOrder.RestaurantName}.");
        }

        // Displays all reviews for a given restaurant.
        private static void SeeReviewsForRestaurant(string restaurantName) {
            var reviews = Login.Reviews
                .Where(r => r.RestaurantName == restaurantName)
                .ToList();

            foreach (var review in reviews) {
                UIFunctions.DisplayString($"Reviewer: {review.CustomerName}");
                UIFunctions.DisplayString($"Rating: {new string('*', review.Rating)}");
                UIFunctions.DisplayString($"Comment: {review.Comment}");
                UIFunctions.DisplayString("");
            }
        }
    }
}