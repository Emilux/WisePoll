﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WisePoll.Data.Models
{
    public class Members
    {
        public Members()
        {
            PollFields = new List<PollFields>();
            Users = new List<Users>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Email { get; set; }
        public List<Users> Users { get; set; }
        public ICollection<PollFields> PollFields { get; set; }
    }
}
