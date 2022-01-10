

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Microsoft.Extensions.Logging;

namespace WisePoll.Services.Tools
{
    public class RequiredEnumerableAttribute : ValidationAttribute
    {
        public RequiredEnumerableAttribute()
            : base("All {0} fields are required"){ }
        
        public override bool IsValid(object value)
        {
            return value is List<string> enumerable && enumerable.Any() && !enumerable.Any(string.IsNullOrEmpty);
        }
    }
}