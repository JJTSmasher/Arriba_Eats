namespace Arriba_Eats {
    class Customer : User 
    {
        public Customer(string email, string password, string role, string name, int phone, int age)
            : base(email, password, role, name, phone, age) {}
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