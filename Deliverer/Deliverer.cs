namespace Arriba_Eats {
    class Deliverer(string email, string password, string role, string name, string phone, int age) 
        : User(email, password, role, name, phone, age) {
        public string licencePlate { get; set; }
        public Dictionary<int, string> orderDeliverStatus = [];
        public bool delivererStatus;
    }
}