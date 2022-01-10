using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace WisePoll.Services.Tools
{
    public class CheckUniqueEmailFromString : ValidationAttribute
    {
        private readonly string _splitOption;

        public CheckUniqueEmailFromString(string splitOption)
            : base("Please enter unique email for the field {0}")
        {
            _splitOption = splitOption;
        }
        
        public override bool IsValid(object value)
        {
            var emailStringList = value as string;
            
            if (string.IsNullOrEmpty(emailStringList)) return false;
            
            var emailList = emailStringList.Split(_splitOption,StringSplitOptions.TrimEntries);
            
            return emailList.Distinct().Count() == emailList.Length;
        }
    }
}