using System.Text.RegularExpressions;

namespace Arriba_Eats {
    class Deliverer_Registration : Registration {
        public override void Register() {
            base.Register();
            string licencePlate = GetValidatedLicencePlate();
            Console.WriteLine($"You have been successfully registered as a deliverer with licence plate {licencePlate}!");

            Login.ShowMenu();
        }

        private string GetValidatedLicencePlate() {
            while (true) {
                Console.WriteLine("Please enter your licence plate (1-8 characters, uppercase letters, numbers, and spaces only):");
                string? licencePlate = Console.ReadLine();
                if (!string.IsNullOrEmpty(licencePlate) && Regex.IsMatch(licencePlate, @"^(?!\s*$)[A-Z0-9 ]{1,8}$")) {
                    return licencePlate;
                }
                Console.WriteLine("Invalid licence plate. Please try again.");
            }
        }

        protected override string GetRole() {
            return "deliverer";
        }
    }
}