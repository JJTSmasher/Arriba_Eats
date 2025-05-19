namespace Arriba_Eats {
    class User {
        public string Email { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public int Age { get; set; }
        public static string CurrentUserEmail { get; set; }

        public User(string email, string password, string role, string name, string phone, int age) {
            Email = email;
            Password = password;
            Role = role;
            Name = name;
            Phone = phone;
            Age = age;
        }

        

    }
}