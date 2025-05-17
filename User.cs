namespace Arriba_Eats {
    class User {
        public string Email { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }  // Optional: "customer", "client", "deliverer"

        public User(string email, string password, string role) {
            Email = email;
            Password = password;
            Role = role;
        }
    }
}