namespace Arriba_Eats {
    public class Review {
        public string RestaurantName { get; set; }
        public string CustomerName { get; set; }
        public string CustomerEmail { get; set; }
        public int Rating { get; set; } // 1-5
        public string Comment { get; set; }
        public Review(string restaurantName, string customerName, string customerEmail, int rating, string comment) {
            RestaurantName = restaurantName;
            CustomerName = customerName;
            CustomerEmail = customerEmail;
            Rating = rating;
            Comment = comment;
        }
    }
}