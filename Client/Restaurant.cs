namespace Arriba_Eats {
    public struct MenuItem {
            public string Name;
            public decimal Price;
            public MenuItem(string name, decimal price) {
                Name = name;
                Price = price;
            }
    }
    public class Restaurant {
        public string Name { get; set; }
        public string Style { get; set; }
        public double AverageRating { get; set; }
        public int x { get; set; }
        public int y { get; set; }
        public List<MenuItem> MenuItems { get; set; } = new List<MenuItem>();
        }
}