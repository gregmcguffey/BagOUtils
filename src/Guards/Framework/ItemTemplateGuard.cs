using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BagOUtils.Guards.Framework
{
    /// <summary>
    /// Guard that uses an exception that requires both the
    /// item name and a message. The message is built based
    /// on a template that uses the item name.
    /// </summary>
    /// <remarks>
    /// Compare to a MessageGuard, which also uses the item
    /// name, but the exception only needs a message.
    /// </remarks>
    public class ItemTemplateGuard<TValue, TException>
        where TException : Exception
    {
        private readonly TValue value;

        private Func<bool> test;
        private Func<string, string, TException> exceptionBuilder;
        private string template;
        private string nameTemplate;
        private string itemName;

        public ItemTemplateGuard(TValue value)
        {
            this.value = value;
        }

        public ItemTemplateGuard<TValue, TException> TestToExecute(Func<bool> test)
        {
            this.test = test;
            return this;
        }

        public ItemTemplateGuard<TValue, TException> ExceptionBuilder(Func<string, string, TException> builder)
        {
            this.exceptionBuilder = builder;
            return this;
        }

        public ItemTemplateGuard<TValue, TException> TemplateUsed(string template)
        {
            this.template = template;
            return this;
        }

        public ItemTemplateGuard<TValue, TException> NameTemplateUsed(string nameTemplate)
        {
            this.nameTemplate = nameTemplate;
            return this;
        }

        public ItemTemplateGuard<TValue, TException> ForItem(string itemName)
        {
            this.itemName = itemName;
            return this;
        }

        public TValue Guard()
        {
            // Desired result is a true test.
            if (!this.test())
            {
                var message = BuildMessage(this.itemName, this.template, this.nameTemplate);
                var ex = this.exceptionBuilder(this.itemName, message);
                throw ex;
            }
            return this.value;
        }

        private string BuildMessage(string itemName, string template, string nameTemplate)
        {
            Func<string> formatBuilder = () => string.Format(template, itemName);
            Func<string> nameBuilder = () => nameTemplate.Replace("{item}", itemName);

            Func<string, bool> isSet = t => !string.IsNullOrWhiteSpace(t);

            // If both templates are set, use the string.Format one,
            // which likely is faster.
            var message = isSet(template)
                ? formatBuilder()
                : nameBuilder();

            return message;
        }
    }
}
