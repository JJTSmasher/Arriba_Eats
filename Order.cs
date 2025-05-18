namespace Arriba_Eats {
    public class Order
    {
        public static int GlobalOrderCount = 0;
        public int OrderID { get; set; }
        public string RestaurantName { get; set; }
        public List<MenuItem> Items { get; set; }
        public decimal Total { get; set; }
        public string Status { get; set; } = "Placed";

        public Order(int orderId, string restaurantName, List<MenuItem> items, decimal total)
        {
            OrderID = orderId;
            RestaurantName = restaurantName;
            Items = items;
            Total = total;
            Status = "Placed";
            GlobalOrderCount++;
        }
    }
}