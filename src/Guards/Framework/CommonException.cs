using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BagOUtils.Guards.Framework
{
    /// <summary>
    /// Repository of exception builders used by Guard for the 
    /// most common exceptions.
    /// </summary>
    /// <remarks>
    /// Note that these are delegates that take info needed by
    /// the argument constructor and then return a delegate to
    /// build the argument. I.e. they do not return an exception
    /// object, but a delegate that builds an exception object.
    /// </remarks>
    public static class CommonException
    {
        public static Func<string, Func<ArgumentException>> SimpleArgument = message => () => new ArgumentException(message);
        public static Func<string, string, Func<ArgumentException>> Argument = (item, message) => () => new ArgumentException(message, item);

        public static Func<string, Func<ArgumentNullException>> SimpleArgumentNull = item => () => new ArgumentNullException(item);
        public static Func<string, string, Func<ArgumentNullException>> ArgumentNull = (item, message) => () => new ArgumentNullException(item, message);

        public static Func<string, Func<ArgumentOutOfRangeException>> SimpleOutOfRange = item => () => new ArgumentOutOfRangeException(item);
        public static Func<string, string, Func<ArgumentOutOfRangeException>> OutOfRange = (item, message) => () => new ArgumentOutOfRangeException(item, message);

        public static Func<string, Func<InvalidOperationException>> InvalidOperation = message => () => new InvalidOperationException(message);
    }
}
