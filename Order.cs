namespace Arriba_Eats
{
    class Order : Customer 
    {
        public int orderID = 0;
        public string order = ""; // NEEDS TO BE SOMETHING ELSE
        public Dictionary<int, string> orderCookStatus = new Dictionary<int, string>();
        public bool orderTaken; // if order claimed by driver
    }
}