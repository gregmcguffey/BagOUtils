using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BagOUtils.Guards.Framework
{
    /// <summary>
    /// Guard composer that uses an explicitly set message when
    /// raising an exception.
    /// </summary>
    public class MessageComposer<TValue>
    {
        private readonly TValue value;

        private Func<bool> test;
        private Func<string, Func<Exception>> exceptionBuilder;
        private string message;

        public MessageComposer(TValue value)
        {
            this.value = value;
        }

        public MessageComposer<TValue> TestToExecute(Func<bool> test)
        {
            this.test = test;
            return this;
        }

        public MessageComposer<TValue> ExceptionBuilder(Func<string, Func<Exception>> builder)
        {
            this.exceptionBuilder = builder;
            return this;
        }

        public MessageComposer<TValue> UsingMessage(string message)
        {
            this.message = message;
            return this;
        }

        public TValue Guard()
        {
            return new Guard<TValue>(this.value)
                .Test(this.test)
                .ExceptionBuilderUsed(this.exceptionBuilder(this.message))
                .ValidateAndReturn();
        }
    }
}
