using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WisePoll.Data.Models
{
    public class PollFields
    {
        public PollFields()
        {
            Users = new List<Users>();
        }
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Label { get; set; }
        public int PollsId { get; set; }
        public List<Users> Users { get; set; }
    }
}
