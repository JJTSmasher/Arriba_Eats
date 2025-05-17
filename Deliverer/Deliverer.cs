namespace Arriba_Eats {
    class Deliverer : User {
        public Deliverer(string email, string password, string role, string name, int phone, int age)
            : base(email, password, role, name, phone, age) {}

        public string licensePlate = "";
        public Dictionary<int, string> orderDeliverStatus = new Dictionary<int, string>();
        public bool delivererStatus; // at restaurant or not
        public struct DelivererLocation {
            public int x;
            public int y;

            // Constructor for DelivererLocation
            public DelivererLocation(int x, int y) {
                this.x = x;
                this.y = y;
            }
        }
        public int travelDistance = 0;
    }
}