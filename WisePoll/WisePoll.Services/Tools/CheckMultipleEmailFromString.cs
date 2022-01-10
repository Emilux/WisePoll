

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Microsoft.Extensions.Logging;

namespace WisePoll.Services.Tools
{
    public class CheckMultipleEmailFromString : ValidationAttribute
    {
        private readonly string _splitOption;

        public CheckMultipleEmailFromString(string splitOption)
            : base("Please enter valid email in the {0} field")
        {
            _splitOption = splitOption;
        }
        
        public override bool IsValid(object value)
        {
            EmailAddressAttribute emailAttribute = new ();
            
            var emailStringList = value as string;
            
            if (string.IsNullOrEmpty(emailStringList)) return false;
            
            var emailList = emailStringList.Split(_splitOption,StringSplitOptions.TrimEntries);
            
            return emailList.All(email => emailAttribute.IsValid(email));
        }
    }
}