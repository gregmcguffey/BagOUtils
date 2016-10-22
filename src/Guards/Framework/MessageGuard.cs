using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BagOUtils.Guards.Framework
{
    /// <summary>
    /// Guard that uses an explicitly set message when
    /// raising an exception.
    /// </summary>
    public class MessageGuard<TValue, TException>
        where TException : Exception
    {
        private readonly TValue value;

        private Func<bool> test;
        private Func<TException> exceptionBuilder;

        public MessageGuard(TValue value)
        {
            this.value = value;
        }

        public MessageGuard<TValue, TException> TestToExecute(Func<bool> test)
        {
            this.test = test;
            return this;
        }

        public MessageGuard<TValue, TException> ExceptionBuilder(Func<TException> builder)
        {
            this.exceptionBuilder = builder;
            return this;
        }

        public TValue Guard()
        {
            // Desired result is a true test.
            if (!this.test())
            {
                var ex = this.exceptionBuilder();
                throw ex;
            }
            return this.value;
        }
    }
}
