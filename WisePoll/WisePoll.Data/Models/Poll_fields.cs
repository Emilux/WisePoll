using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WisePoll.Data.Models
{
    public class Poll_fields
    {
        public Poll_fields()
        {
            Members = new List<Members>();
        }
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Label { get; set; }

        [Required]
        public int PollsId { get; set; }

        public List<Members> Members { get; set; }
    }
}
