using System.Text.RegularExpressions;

namespace Arriba_Eats {
    class Deliverer_Registration : Registration {
        public override void Register() {
            base.Register(); // Call base registration for common user info.

            // Prompt for and validate the deliverer's licence plate.
            string licencePlate = GetValidatedLicencePlate();

            // Create a new Deliverer object with the collected information.
            Deliverer deliverer = new(email, password, GetRole(), name, phone, age) {
                licencePlate = licencePlate // Set the deliverer's licence plate.
            };

            // Add the new deliverer to the system.
            Login.AddUser(deliverer);

            UIFunctions.DisplayString($"You have been successfully registered as a deliverer, {name}!");

            // Show the main menu after registration.
            Login.ShowMenu();
        }

        // Prompts the user for a valid licence plate and validates the input.
        private static string GetValidatedLicencePlate() {
            while (true) {
                UIFunctions.DisplayString("Please enter your licence plate:");
                string? licencePlate = Console.ReadLine();
                // Licence plate specifications
                // Must be 1-8 characters, uppercase letters, numbers, or spaces.
                if (!string.IsNullOrEmpty(licencePlate) && Regex.IsMatch(licencePlate, @"^(?!\s*$)[A-Z0-9 ]{1,8}$")) {
                    return licencePlate;
                }
                UIFunctions.DisplayString("Invalid licence plate.");
            }
        }

        // Returns the role string for this registration type.
        protected override string GetRole() {
            return "deliverer";
        }
    }
}