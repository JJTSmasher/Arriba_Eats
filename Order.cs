namespace Arriba_Eats {
    public class Order(int orderId, string restaurantName, List<MenuItem> items, decimal total)
    {
        public int OrderID { get; set; } = orderId;
        public string RestaurantName { get; set; } = restaurantName;
        public List<MenuItem> Items { get; set; } = items;
        public decimal Total { get; set; } = total;
        public string Status { get; set; } = "Placed";
        public DateTime PlacedAt { get; set; } = DateTime.Now;
    }
}