namespace Arriba_Eats {
    class Customer : User 
    {
        public Customer(string email, string password, string role, string name, int phone, int age)
            : base(email, password, role, name, phone, age) {}

        public static void CustomerMenu(string name) {
            Console.WriteLine($"Welcome back, {name}!");
            while (true) {
                Console.WriteLine("Please make a choice from the menu below:");
                Console.WriteLine("1: Display your user information");
                Console.WriteLine("2: Select a list of restaurants to order from");
                Console.WriteLine("3: See the status of your orders");
                Console.WriteLine("4: Rate a restaurant you've ordered from");
                Console.WriteLine("5: Log out");
                Console.WriteLine("Please enter a choice between 1 and 5:");

                if (!int.TryParse(Console.ReadLine(), out int choice) || choice < 1 || choice > 5) {
                    Console.WriteLine("Invalid choice. Please try again.");
                    continue;
                }

                switch (choice) {
                    case 1:
                        ShowData();
                        return;
                    case 2:

                        return;
                    case 3:

                        return;
                    case 4:

                        return;
                    case 5:
                        Console.WriteLine("You are now logged out.");
                        return;
                }
            }
        }

        private string ShowData() {
            Console.WriteLine("Your user details are as follows:");
            Console.WriteLine($"Name: {Name}");
            Console.WriteLine($"Age: {Age}");
            Console.WriteLine($"Email: {Email}");
            Console.WriteLine($"Mobile: {Phone}");
            Console.WriteLine($"Location: {}");
            Console.WriteLine($"You've made {} order(s) and spent a total of ${} here.");
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