namespace Arriba_Eats {
    class Order : Customer {
        public int orderID = 0;
        public string order = ""; // Placeholder for order details
        public Dictionary<int, string> orderCookStatus = new Dictionary<int, string>();
        public bool orderTaken; // Indicates if the order is claimed by a driver

        // Constructor for the Order class
        public Order(string email, string password, string role, string name, string phone, int age)
            : base(email, password, role, name, phone, age) {
        }
    }
}