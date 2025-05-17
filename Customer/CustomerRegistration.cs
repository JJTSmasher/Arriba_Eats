using System.Text.RegularExpressions;

namespace Arriba_Eats {
    class Customer_Registration : Registration {
        public override void Register() {
            base.Register();

            string locationInput = GetValidatedLocation();
            string[] coordinates = locationInput.Split(',');
            int x = int.Parse(coordinates[0]);
            int y = int.Parse(coordinates[1]);

            Customer customer = new Customer(email, password, GetRole(), name, phone, age) {
                Location = new Customer.CustomerLocation(x, y)
            };

            Console.WriteLine($"You have been successfully registered as a customer, {name}!");
            Login.AddUser(customer);

            Login.ShowMenu();
        }

        protected override string GetRole() {
            return "customer";
        }
    }
}