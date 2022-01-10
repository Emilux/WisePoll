using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WisePoll.Data.Models
{
    public class PollFields
    {
        public PollFields()
        {
            Members = new List<Members>();
        }
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Label { get; set; }

        public List<Members> Members { get; set; }
    }
}
