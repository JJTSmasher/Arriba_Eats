namespace Arriba_Eats {
    class Customer : User 
    {
        public struct CustomerLocation 
        {
            public int x;
            public int y;
        }
        public struct OrginalLocation 
        {
            public int x;
            public int y;
        }
        public int ordersMade = 0;
        public decimal moneySpent = 0;
    }
}