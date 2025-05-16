namespace Arriba_Eats {
    class Deliverer_Registration : Registration {
        public override void Register() {
            base.Register();
            string licencePlate = GetInput("Please enter your licence plate:");
            Console.WriteLine($"You have been successfully registered as a deliverer with licence plate {name}!");
            LoginMenu.ShowMenu();
        }
    }
}