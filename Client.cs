namespace Arriba_Eats
{
    class Client : User 
    {
        public string restaurantName = "";
        public Dictionary<int, string> restaurantStyles = new Dictionary<int, string>(); // 1-6 as resturaunt styles
        public struct RestaurantLocation 
        {
            public int x;
            public int y;
        }
        public decimal restaurantRating = 0;
        
    }
}