namespace Arriba_Eats {
    // User properties, applied to each customer
    class Customer(string email, string password, string role, string name, string phone, int age)
        : User(email, password, role, name, phone, age)
    {
        // Properties relating to each customer
        public struct CustomerLocation(int x, int y)
        {
            public int x = x;
            public int y = y;
        }
        public CustomerLocation Location { get; set; }
        public List<Order> Orders { get; } = [];
        public decimal moneySpent = 0;
        public int ordersMade = 0;
    }
}