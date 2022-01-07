using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WisePoll.Data.Models
{
    public class Users
    {
        public Users()
        {
            Members = new List<Members>();
            Polls = new List<Polls>();
        }
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
        
        public List<Members> Members { get; set; }
        
        public List<Polls> Polls { get; set;}
    }
}
