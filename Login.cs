namespace Arriba_Eats {
    class Login {
        public static void Authenticate() {
            Console.WriteLine("Email:");
            string email = Console.ReadLine();
            Console.WriteLine("Password:");
            string password = Console.ReadLine();

            // Simulate user authentication (replace with actual logic)
            if (IsValidUser(email, password)) {
                Console.WriteLine("Login successful! Welcome back.");
                // Proceed to the next menu or functionality
            } else {
                Console.WriteLine("Invalid email or password. Returning to the login menu...");
            }
        }

        private static bool IsValidUser(string email, string password) {
            // Placeholder for actual authentication logic
            // Replace this with database or file-based user validation
            return email == "test@arribaeats.com" && password == "Password123";
        }
    }
}