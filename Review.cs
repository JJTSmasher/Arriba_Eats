namespace Arriba_Eats {
    public class Review(string restaurantName, string customerName, string customerEmail, int orderId, int rating, string comment)
    {
        public string RestaurantName { get; set; } = restaurantName;
        public string CustomerName { get; set; } = customerName;
        public string CustomerEmail { get; set; } = customerEmail;
        public int OrderID { get; set; } = orderId;
        public int Rating { get; set; } = rating;
        public string Comment { get; set; } = comment;
    }
}