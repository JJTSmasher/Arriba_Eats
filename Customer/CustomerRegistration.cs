using System.Text.RegularExpressions;

namespace Arriba_Eats {
    class Customer_Registration : Registration {
        public override void Register() {
            base.Register();
            
            string location = GetValidatedLocation();
            Console.WriteLine($"You have been successfully registered as a customer, {name}!");
        }
        
        protected override string GetRole() {
            return "customer";
        }

        private string GetValidatedLocation() {
            while (true) {
                Console.WriteLine("Please enter your location (in the form of X,Y) :");
                string location = Console.ReadLine();

                // Regex to validate the location format
                if (!string.IsNullOrEmpty(location) && Regex.IsMatch(location, @"^-?\d+,-?\d+$")) {
                    return location;
                }

                Console.WriteLine("Invalid location.");
            }
        }
    }
}