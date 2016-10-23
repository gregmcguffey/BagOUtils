using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BagOUtils.Guards.Messages
{
    /// <summary>
    /// Contains any static messages used by Guards.
    /// </summary>
    public static class Message
    {
        public static string BadGuardRequiredLength
            = "Guard configured incorrectly. The required length of a string must be greater than zero.";
    }
}
