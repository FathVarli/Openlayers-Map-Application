namespace Core.Models
{
    public class UserUpdateModel
    {
        public int Id { get; set; }

        public string UserName { get; set; }

        public string NameSurname { get; set; }

        public string Password { get; set; }
    }
}