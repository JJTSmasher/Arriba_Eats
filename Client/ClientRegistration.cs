using System.Text.RegularExpressions;

namespace Arriba_Eats {
    class Client_Registration : Registration {
        public override void Register() {
            base.Register();

            string restaurantName = GetValidatedRName();

            Console.WriteLine("Please select your restaurant's style:");
            Console.WriteLine("1: Italian");
            Console.WriteLine("2: French");
            Console.WriteLine("3: Chinese");
            Console.WriteLine("4: Japanese");
            Console.WriteLine("5: American");
            Console.WriteLine("6: Australian");
            Console.WriteLine("Please enter a choice between 1 and 6:");

            int styleChoice;
            while (!int.TryParse(Console.ReadLine(), out styleChoice) || styleChoice < 1 || styleChoice > 6) {
                Console.WriteLine("Invalid choice. Please try again.");
            }

            string[] styles = { "Italian", "French", "Chinese", "Japanese", "American", "Australian" };
            string restaurantStyle = styles[styleChoice - 1];

            string locationInput = GetValidatedLocation();
            string[] coordinates = locationInput.Split(',');
            int x = int.Parse(coordinates[0]);
            int y = int.Parse(coordinates[1]);

            Client client = new Client(email, password, GetRole(), name, phone, age) {
                RestaurantName = restaurantName,
                Style = restaurantStyle,
                Location = new RestaurantLocation(x, y)
            };
            client.RestaurantStyles.Add(styleChoice, restaurantStyle);

            Login.AddUser(client);

            Console.WriteLine($"You have been successfully registered as a client, {name}!");
            Login.ShowMenu();
        }

        protected override string GetRole() {
            return "client";
        }

        protected string GetValidatedRName() {
            while (true) {
                Console.WriteLine("Please enter your restaurant's name:");
                string RestaurantName = Console.ReadLine();

                if (!string.IsNullOrWhiteSpace(RestaurantName)) {
                    return RestaurantName;
                }

                Console.WriteLine("Invalid restaurant name.");
            }
        }
    }
}