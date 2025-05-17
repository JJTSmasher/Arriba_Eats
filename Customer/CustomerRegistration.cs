using System.Text.RegularExpressions;

namespace Arriba_Eats {
    class Customer_Registration : Registration {
        public override void Register() {
            base.Register();
            
            string location = GetValidatedLocation();
            Console.WriteLine($"You have been successfully registered as a customer, {name}!");
        }
        
        protected override string GetRole() {
            return "customer";
        }
    }
}