using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace PlantShop.Models
{
    [Table("users")]
    public class UserModel : IdentityUser
    {
        [Column("first_name")]
        public string? FirstName { get; set; }

        [Column("last_name")]
        public string? LastName { get; set; }

        [Column("phone")]
        public string? Phone { get; set; }

        [Column("hash_password")]
        public string? HashPassword { get; set; }

        [Column("avatar")]
        public string? Avatar { get; set; }

        [Column("status")]
        public int Status { get; set; }

        [Column("created_at")]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? CreatedAt { get; set; }
    }
}