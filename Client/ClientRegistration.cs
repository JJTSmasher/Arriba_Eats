namespace Arriba_Eats {
    class Client_Registration : Registration {
        public override void Register() {
            base.Register();
            string restaurantName = GetInput("Please enter your restaurant's name:");

            Console.WriteLine("Please select your restaurant's style:");
            Console.WriteLine("1: Italian");
            Console.WriteLine("2: French");
            Console.WriteLine("3: Chinese");
            Console.WriteLine("4: Japanese");
            Console.WriteLine("5: American");
            Console.WriteLine("6: Australian");

            int styleChoice;
            while (!int.TryParse(Console.ReadLine(), out styleChoice) || styleChoice < 1 || styleChoice > 6) {
                Console.WriteLine("Invalid choice. Please try again.");
            }

            string[] styles = { "Italian", "French", "Chinese", "Japanese", "American", "Australian" };
            string restaurantStyle = styles[styleChoice - 1];

            string restaurantLocation = GetValidatedLocation();
            Console.WriteLine($"You have been successfully registered as a client, {name}");

            Login.ShowMenu();
        }
        protected override string GetRole() {
            return "client";
        }
    }
}