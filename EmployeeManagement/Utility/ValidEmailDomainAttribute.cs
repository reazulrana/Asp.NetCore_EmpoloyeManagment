using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.Utility
{
    public class ValidEmailDomainAttribute:ValidationAttribute
    {
        public readonly string domainName;

        public ValidEmailDomainAttribute(string domainName)
        {
            this.domainName = domainName;
        }

        public override bool IsValid(object value)
        {

            string[] val = value.ToString().Split("@");
            string compareVal = val[1].ToUpper();


            return compareVal == domainName.ToUpper();
        }
    }
}
