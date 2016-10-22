using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BagOUtils.Guards.Framework
{
    /// <summary>
    /// Guard composer that uses an exception that requires both the
    /// item name and a message. The message is built based
    /// on a template that uses the item name.
    /// </summary>
    /// <remarks>
    /// Compare to a MessageGuard, which also uses the item
    /// name, but the exception only needs a message.
    /// </remarks>
    public class ItemTemplateComposer<TValue>
    {
        private readonly TValue value;

        private Func<bool> test;
        private Func<string, string, Func<Exception>> exceptionBuilder;
        private string template;
        private string nameTemplate;
        private string itemName;

        public ItemTemplateComposer(TValue value)
        {
            this.value = value;
        }

        public ItemTemplateComposer<TValue> TestToExecute(Func<bool> test)
        {
            this.test = test;
            return this;
        }

        public ItemTemplateComposer<TValue> ExceptionBuilder(Func<string, string, Func<Exception>> builder)
        {
            this.exceptionBuilder = builder;
            return this;
        }

        public ItemTemplateComposer<TValue> TemplateUsed(string template)
        {
            this.template = template;
            return this;
        }

        public ItemTemplateComposer<TValue> NameTemplateUsed(string nameTemplate)
        {
            this.nameTemplate = nameTemplate;
            return this;
        }

        public ItemTemplateComposer<TValue> ForItem(string itemName)
        {
            this.itemName = itemName;
            return this;
        }

        public TValue Guard()
        {
            var message = BuildMessage(this.itemName, this.template, this.nameTemplate);
            var guard = new Guard<TValue>(this.value)
                .ExceptionBuilderUsed(this.exceptionBuilder(this.itemName, message))
                .Test(this.test);
            return guard.ValidateAndReturn();
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
