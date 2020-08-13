using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities.Concrete
{
    [Table("dbo.User")]
    public class User:IEntity
    {
        [Column("id")]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Column("name")]
        [StringLength(50, MinimumLength = 3)]
        public string Name { get; set; }
        [Column("email")]
        public string EMail { get; set; }

        [Column("password_salt")]
        public byte[] PasswordSalt { get; set; }
        [Column("password_hash")]
        public byte[] PasswordHash { get; set; }
    }
}