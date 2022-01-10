using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using WisePoll.Services.Tools;

namespace WisePoll.Services.ViewModels
{
    public class CreatePollViewModel
    {
        [Required(ErrorMessage = "The poll title is required")]
        [MaxLength(100,ErrorMessage = "The poll title can't have more than 100 Characters")]
        public string Title { get; set; }
        [MaxLength(552,ErrorMessage = "The poll description can't have more than 552 Characters")]
        public string Text { get; set; }
        [DefaultValue(0)]
        public bool Multiple { get; set; }
        [
            RequiredEnumerable(ErrorMessage = "All added choices are required"), 
            MinLength(2, ErrorMessage = "At least two choices are required to create a poll"),
            MaxLength(5, ErrorMessage = "Only 5 Choices can be added")
        ]
        public IEnumerable<string> PollFields { get; set;}
        [
            Required(ErrorMessage = "At least one participant required to create a poll"), 
            CheckMultipleEmailFromString(",",ErrorMessage = "Please enter valid participants email"),
            CheckUniqueEmailFromString(",",ErrorMessage = "Please enter uniques participants email")
        ]
        public string Members { get; set; }
    }
}