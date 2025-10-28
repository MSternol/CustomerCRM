using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerCRM.App.Helpers
{
    public static class IDGenerator
    {
        public static string GenerateUniqueID()
        {

            return Guid.NewGuid().ToString().Substring(0, 8);
        }
    }
}
