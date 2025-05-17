namespace Arriba_Eats {
    class Customer : User 
    {
        public Customer(string email, string password, string role, string name, int phone, int age)
            : base(email, password, role, name, phone, age) {}

        public static void CustomerMenu(Customer customer) {
            Console.WriteLine($"Welcome back, {customer.Name}!");
            while (true) {
                Console.WriteLine("Please make a choice from the menu below:");
                Console.WriteLine("1: Display your user information");
                Console.WriteLine("2: Select a list of restaurants to order from");
                Console.WriteLine("3: See the status of your orders");
                Console.WriteLine("4: Rate a restaurant you've ordered from");
                Console.WriteLine("5: Log out");
                Console.WriteLine("Please enter a choice between 1 and 5:");

                if (!int.TryParse(Console.ReadLine(), out int choice) || choice < 1 || choice > 5) {
                    Console.WriteLine("Invalid choice.");
                    continue;
                }

                switch (choice) {
                    case 1:
                        Customer.ShowData(customer);
                        break;
                    case 2:

                        break;
                    case 3:

                        break;
                    case 4:

                        break;
                    case 5:
                        Console.WriteLine("You are now logged out.");
                        return;
                }
            }
        }

        private static void ShowData(Customer customer) {
            Console.WriteLine("Your user details are as follows:");
            Console.WriteLine($"Name: {customer.Name}");
            Console.WriteLine($"Age: {customer.Age}");
            Console.WriteLine($"Email: {customer.Email}");
            Console.WriteLine($"Mobile: {customer.Phone}");
            Console.WriteLine($"Orders Made: {customer.ordersMade}");
            Console.WriteLine($"Money Spent: ${customer.moneySpent:F2}");
        }

        public struct CustomerLocation 
        {
            public int x;
            public int y;
        }
        public struct OrginalLocation 
        {
            public int x;
            public int y;
        }
        public int ordersMade = 0;
        public decimal moneySpent = 0;

        
    }
}