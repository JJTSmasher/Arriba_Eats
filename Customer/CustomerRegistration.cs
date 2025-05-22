namespace Arriba_Eats {
    /// <summary>
    /// Handles the registration process for a customer user.
    /// </summary>
    class Customer_Registration : Registration {
        /// <summary>
        /// Customer registration, collect and validates all required information.
        /// </summary>
        public override void Register() {
            base.Register(); // Call base registration for common user info.

            // Prompt for and validate the customer's location.
            string locationInput = GetValidatedLocation();
            string[] coordinates = locationInput.Split(',');
            int x = int.Parse(coordinates[0]);
            int y = int.Parse(coordinates[1]);

            // Create a new Customer object with the collected information.
            Customer customer = new(email, password, GetRole(), name, phone, age) {
                // Set the customer's location using the parsed coordinates.
                Location = new Customer.CustomerLocation(x, y)
            };

            // Add the new customer to the system.
            Login.AddUser(customer);

            UIFunctions.DisplayString($"You have been successfully registered as a customer, {name}!");

            // Show the main menu after registration.
            Login.ShowMenu();
        }

        /// <summary>
        /// Returns the role for this registration type.
        /// </summary>
        protected override string GetRole() {
            return "customer";
        }
    }
}