using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WisePoll.Data.Models
{
    public class Members
    {
        public Members()
        {
            Poll_fields = new List<Poll_fields>();
            Polls = new List<Polls>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Email { get; set; }

        [Required]
        public int PollsId { get; set; }

        public List<Poll_fields> Poll_fields { get; set; }

        public List<Polls> Polls { get; set; }
    }
}
