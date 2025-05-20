namespace Arriba_Eats {
    class Client(string email, string password, string role, string name, string phone, int age) 
        : User(email, password, role, name, phone, age) {
        public Dictionary<int, string> RestaurantStyles { get; } = [];
        public string RestaurantName { get; set; } = "";
        public string Style { get; set; }
        public decimal RestaurantRating { get; set; } = 0;
        public double AverageRating { get; set; }
        public List<MenuItem> MenuItems { get; set; } = [];
        public RestaurantLocation Location { get; set; }
    }
    public struct RestaurantLocation(int x, int y)
    {
        public int x = x, y = y;
    }
}