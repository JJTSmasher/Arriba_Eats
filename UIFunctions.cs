namespace Arriba_Eats
{
    /// <summary>
    /// Provides utility functions for user interface interactions.
    /// </summary>
    static class UIFunctions
    {
        /// <summary>
        /// Displays a message to the user.
        /// </summary>
        public static void DisplayString(string message)
        {
            Console.WriteLine(message);
        }

        /// <summary>
        /// Reads a line of input from the user.
        /// </summary>
        public static string ReadString()
        {
            return Console.ReadLine();
        }

        /// <summary>
        /// Prompts the user for a choice and validates input.
        /// </summary>
        public static int GetChoice(int FirstOp, int LastOp)
        {
            int choice;
            while (!int.TryParse(ReadString(), out choice) || choice < FirstOp || choice > LastOp)
            {
                DisplayString("Invalid choice.");
            }
            return choice;
        }
    }
}