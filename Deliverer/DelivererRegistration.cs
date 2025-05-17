using System.Text.RegularExpressions;

namespace Arriba_Eats {
    class Deliverer_Registration : Registration {
        public override void Register() {
            base.Register();
            string licencePlate = GetValidatedLicencePlate();
            Console.WriteLine($"You have been successfully registered as a deliverer, {name}!");

            Login.ShowMenu();
        }

        private string GetValidatedLicencePlate() {
            while (true) {
                Console.WriteLine("Please enter your licence plate:");
                string? licencePlate = Console.ReadLine();
                if (!string.IsNullOrEmpty(licencePlate) && Regex.IsMatch(licencePlate, @"^(?!\s*$)[A-Z0-9 ]{1,8}$")) {
                    return licencePlate;
                }
                Console.WriteLine("Invalid licence plate.");
            }
        }

        protected override string GetRole() {
            return "deliverer";
        }
    }
}