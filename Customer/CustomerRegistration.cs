namespace Arriba_Eats {
    class Customer_Registration : Registration {
        public override void Register() {
            base.Register();
            
            string location = GetInput("Please enter your location (in the form of X,Y):");
            Console.WriteLine($"You have been successfully registered as a customer, {name}!");
            LoginMenu.ShowMenu();
        }
        
        protected override string GetRole() {
            return "customer";
        }
    }
}