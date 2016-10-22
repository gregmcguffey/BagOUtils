using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BagOUtils.Guards.Framework
{
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
