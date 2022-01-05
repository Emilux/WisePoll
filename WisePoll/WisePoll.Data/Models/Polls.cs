using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WisePoll.Data.Models
{
    public class Polls
    {
        public Polls()
        {
            Members = new List<Members>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Title { get; set; }

        [Column(TypeName = "text")]
        public string Text { get; set; }

        [DefaultValue(0)]
        public bool Multiple { get; set; }

        [DefaultValue(1)]
        public bool Is_active { get; set; }

        [Required]
        public int UsersId { get; set; }

        public List<Members> Members { get; set; }

    }
}
