using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.Utility
{
    public static class GlobalFunction
    {

        public static List<string> IdNotFound(string Id)
        {
            List<string> output = new List<string>();
            output.Add("Not Found Error Message");
            output.Add($"The Role With This Id {Id} Not Found");

            return output;

        }
    }
}
