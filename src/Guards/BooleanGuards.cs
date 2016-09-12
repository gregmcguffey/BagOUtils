using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BagOUtils.Guards
{
    /// <summary>
    /// Defend against invalid states/parameters/arguments when the
    /// state/parameter/argument is a boolean. Each of the methods
    /// defines a "guard". The guard validates that the
    /// state/parameter/argument does not have a particular type of
    /// potential error. If the guard finds the
    /// state/parameter/argument to be in error, an exception is
    /// thrown. While most other guards return the original value for
    /// further testing, this is not done with booleans. There are only
    /// two mutually exclusive test, so no chaining is possible.
    /// </summary>
    public static class BooleanGuards
    {
        /// <summary>
        /// Validate that value of the flag is true. This is used to
        /// validate that some expected state is true. This could be
        /// used to validate that some business rule is working
        /// correctly or to ensure that the state of an object is correct.
        /// </summary>
        /// <param name="flag">Bool variable to test.</param>
        /// <param name="message">
        /// Describe why the value must be true.
        /// </param>
        public static void GuardIsTrue(this bool flag, string message)
        {
            if (!flag)
            {
                throw new InvalidOperationException(message);
            }
        }

        /// <summary>
        /// Validate that value of the flag is false. This is used to
        /// validate that some expected state is false. This could be
        /// used to validate that some business rule is working
        /// correctly or to ensure that the state of an object is correct.
        /// </summary>
        /// <param name="flag">Bool variable to test.</param>
        /// <param name="errorMessage">
        /// Describe why the value must be true.
        /// </param>
        public static void GuardIsFalse(this bool flag, string errorMessage)
        {
            if (flag)
            {
                throw new InvalidOperationException(errorMessage);
            }
        }

    }
}
