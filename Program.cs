namespace Arriba_Eats {
    /// <summary>
    /// Entry point for Arriba Eats.
    /// </summary>
    class Program
    {
        /// <summary>
        /// Start the application and display the main menu.
        /// </summary>
        public static void Main(string[] args)
        {
            // Welcome user and show login menu
            UIFunctions.DisplayString("Welcome to Arriba Eats!");
            Login.ShowMenu();
        }
    }
}