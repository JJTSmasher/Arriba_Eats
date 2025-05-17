namespace Arriba_Eats {
    class Deliverer : User {
        public Deliverer(string email, string password, string role) : base(email, password, role) {
        }

        public string licensePlate = "";
        public Dictionary<int, string> orderDeliverStatus = new Dictionary<int, string>();
        public bool delivererStatus; // at restaurant or not
        public struct DelivererLocation {
            public int x;
            public int y;
        }
        public int travelDistance = 0;
    }
}