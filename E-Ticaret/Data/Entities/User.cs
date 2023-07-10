namespace E_Ticaret.Data.Entities
{
    public class User
    {
        public string Id { get; init; }
        public User()
        {
            Id = Guid.NewGuid().ToString();
        }
        public string Name { get; set; }
        public string SurName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public List<Role> Roles { get; set; }
    }
}
