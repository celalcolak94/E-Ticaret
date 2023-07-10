namespace E_Ticaret.Data.Entities
{
    public class Role
    {
        public string Id { get; init; }
        public Role()
        {
            Id = Guid.NewGuid().ToString();
        }
        public string RoleName { get; set; }
        public List<User> Users { get; set; }
    }
}
