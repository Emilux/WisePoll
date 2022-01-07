using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WisePoll.Data.Models
{
    public class Users
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(25)]
        public string Pseudo { get; set; }

        [Required]
        [StringLength(255)]
        public string Email { get; set; }

        [Required]
        [StringLength(255)]
        public string Password { get; set; }

        [ForeignKey("UsersId")]
        public ICollection<Polls> Polls { get; set; }
    }
}
