using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BagOUtils.Guards.Framework
{
    /// <summary>
    /// Guards that a condition is met and if it isn't, raises an exception.
    /// It it is met, return the value.
    /// </summary>
    public class Guard<TValue, TException>
        where TException : Exception
    {
        private readonly TValue value;

        private Func<TException> exceptionBuilder;
        private Func<bool> test;

        public Guard(TValue value)
        {
            this.value = value;

            // Setup defaults that will throw execeptions if not setup.
            this.exceptionBuilder = this.ExceptionBuilderNotSet;
            this.test = this.TestNotSet;
        }

        public Guard<TValue, TException> ExceptionBuilderUsed(Func<TException> exceptionBuilder)
        {
            this.exceptionBuilder = exceptionBuilder;
            return this;
        }

        public Guard<TValue, TException> Test(Func<bool> test)
        {
            this.test = test;
            return this;
        }

        public TValue Perform()
        {
            if(!this.test())
            {
                var ex = this.exceptionBuilder();
                throw ex;
            }
            return this.value;
        }


        //-------------------------------------------------------------------------
        //
        // Default Exception Builder and Test
        //
        //-------------------------------------------------------------------------

        private Func<TException> ExceptionBuilderNotSet
        {
            get
            {
                Func<TException> notSet = () =>
                {
                    throw new InvalidOperationException("Guard is not configured property. No exception builder has been defined.");
                };

                return notSet;
            }
        }

        private Func<bool> TestNotSet
        {
            get
            {
                Func<bool> notSet = () =>
                {
                    throw new InvalidOperationException("Guard is not configured property. No test has been defined.");
                };

                return notSet;
            }
        }
    }
}
