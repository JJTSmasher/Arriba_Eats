namespace Arriba_Eats {
    class User {
        public string Email { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Age { get; set; }

        public User(string email, string password, string role) {
            Email = email;
            Password = password;
            Role = role;
            Name = name;
            Phone = phone;
            Age = age;
        }
    }
}