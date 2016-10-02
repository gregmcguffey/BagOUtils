using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BagOUtils.ConfigurationCache
{
    /// <summary>
    /// Internally used guards. Keep it simple. All guards are on
    /// booleans and take a message to use if a throw is needed.
    /// </summary>
    /// <remarks>
    /// While we have a nice guard library, we want to keep each
    /// library independent.
    /// </remarks>
    internal static class Guards
    {
        public static void GuardIsTrue(this bool flag, string message)
        {
            flag.GuardIsSame(true, message);
        }

        public static void GuardIsFalse(this bool flag, string message)
        {
            flag.GuardIsSame(false, message);
        }

        public static void GuardIsSame(this bool flag, bool comparedTo, string message)
        {
            if (flag != comparedTo)
            {
                throw new InvalidOperationException(message);
            }
        }
    }
}
