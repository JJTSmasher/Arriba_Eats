namespace Arriba_Eats {
    class Client_Registration : Registration {
        public override void Register() {
            base.Register(); // Call base registration for common user info.

            // Prompt for and validate the restaurant name.
            string restaurantName = GetValidatedRName();

            // Prompt user to select a restaurant style.
            Console.WriteLine("Please select your restaurant's style:");
            Console.WriteLine("1: Italian");
            Console.WriteLine("2: French");
            Console.WriteLine("3: Chinese");
            Console.WriteLine("4: Japanese");
            Console.WriteLine("5: American");
            Console.WriteLine("6: Australian");
            Console.WriteLine("Please enter a choice between 1 and 6:");

            // Get and validate the style choice.
            int styleChoice;
            while (!int.TryParse(UIFunctions.ReadString(), out styleChoice) || styleChoice < 1 || styleChoice > 6) {
                Console.WriteLine("Invalid choice. Please try again.");
            }

            // Set the user choice to a style string.
            string[] styles = { "Italian", "French", "Chinese", "Japanese", "American", "Australian" };
            string restaurantStyle = styles[styleChoice - 1];

            // Prompt for and validate the restaurant location.
            string locationInput = GetValidatedLocation();
            string[] coordinates = locationInput.Split(',');
            int x = int.Parse(coordinates[0]);
            int y = int.Parse(coordinates[1]);

            // Create a new Client object with the collected info.
            Client client = new(email, password, GetRole(), name, phone, age) {
                RestaurantName = restaurantName,
                Style = restaurantStyle,
                Location = new RestaurantLocation(x, y)
            };
            // Add the selected style to the client RestaurantStyles dictionary.
            client.RestaurantStyles.Add(styleChoice, restaurantStyle);

            // Add the new client to the system.
            Login.AddUser(client);

            Console.WriteLine($"You have been successfully registered as a client, {name}!");
            Login.ShowMenu(); // Show the main menu after registration.
        }

        // Returns the role string for this registration type.
        protected override string GetRole() {
            return "client";
        }

        // Prompts the user for a valid restaurant name.
        protected static string GetValidatedRName() {
            while (true) {
                UIFunctions.DisplayString("Please enter your restaurant's name:");
                string RestaurantName = UIFunctions.ReadString();

                if (!string.IsNullOrWhiteSpace(RestaurantName)) {
                    return RestaurantName;
                }

                UIFunctions.DisplayString("Invalid restaurant name.");
            }
        }
    }
}