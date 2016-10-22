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
    public class Guard<TValue> : IValidator<TValue>
    {
        private readonly TValue value;

        private Func<Exception> exceptionBuilder;
        private Func<bool> test;

        public Guard(TValue value)
        {
            this.value = value;

            // Setup defaults that will throw execeptions if not setup.
            this.exceptionBuilder = this.ExceptionBuilderNotSet;
            this.test = this.TestNotSet;
        }

        public Guard<TValue> ExceptionBuilderUsed(Func<Exception> exceptionBuilder)
        {
            this.exceptionBuilder = exceptionBuilder;
            return this;
        }

        public Guard<TValue> Test(Func<bool> test)
        {
            this.test = test;
            return this;
        }

        public TValue ValidateAndReturn()
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

        private Func<Exception> ExceptionBuilderNotSet
        {
            get
            {
                Func<Exception> notSet = () =>
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
