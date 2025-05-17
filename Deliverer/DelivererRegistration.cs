using System.Text.RegularExpressions;

namespace Arriba_Eats {
    class Deliverer_Registration : Registration {
        public override void Register() {
            base.Register();
            string licencePlate = GetValidatedLicencePlate();
            Console.WriteLine($"You have been successfully registered as a deliverer with licence plate {licencePlate}!");
            LoginMenu.ShowMenu();
        }

        private string GetValidatedLicencePlate() {
            while (true) {
                Console.WriteLine("Please enter your licence plate (1-8 characters, uppercase letters, numbers, and spaces only):");
                string licencePlate = Console.ReadLine();
                // Regex to validate the licence plate
                if (Regex.IsMatch(licencePlate, @"^(?!\s*$)[A-Z0-9 ]{1,8}$")) {
                    return licencePlate;
                }
                Console.WriteLine("Invalid licence plate. Please try again.");
            }
        }
    }
}