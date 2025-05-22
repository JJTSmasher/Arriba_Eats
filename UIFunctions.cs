namespace Arriba_Eats {

    static class UIFunctions
    {
        public static void DisplayString(string message)
        {
            Console.WriteLine(message);
        }

        public static string ReadString()
        {
            return Console.ReadLine();
        }

        public static int GetChoice(int FirstOp, int LastOp)
        {
            int choice;
            while (!int.TryParse(Console.ReadLine(), out choice) || choice < FirstOp || choice > LastOp)
            {
                DisplayString("Invalid choice.");
            }
            return choice;
        }

    }
}