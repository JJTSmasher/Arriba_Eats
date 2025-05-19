namespace Arriba_Eats {
    class User(string email, string password, string role, string name, string phone, int age)
    {
        public string Email { get; set; } = email;
        public string Password { get; set; } = password;
        public string Role { get; set; } = role;
        public string Name { get; set; } = name;
        public string Phone { get; set; } = phone;
        public int Age { get; set; } = age;
        public static string CurrentUserEmail { get; set; }
    }
}