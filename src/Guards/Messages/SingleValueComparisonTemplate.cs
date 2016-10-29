using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BagOUtils.Guards.Messages
{
    /// <summary>
    /// Template for a message that compares a value to another
    /// value. It requires the item also.
    /// </summary>
    /// <remarks>
    /// This can be used for min/max comparisons or to show that
    /// an item isn't in a required list.
    /// </remarks>
    public class SingleValueComparisonTemplate
    {
        private readonly string template;
        private string item;
        private string value;
        private string comparedTo;

        public SingleValueComparisonTemplate(string template)
        {
            this.template = template;
        }

        public SingleValueComparisonTemplate UsingItem(string item)
        {
            this.item = item.NullToEmpty();
            return this;
        }

        public SingleValueComparisonTemplate UsingValue(object value)
        {
            this.value = value.NullToEmpty();
            return this;
        }

        public SingleValueComparisonTemplate ComparedTo(object comparedTo)
        {
            this.comparedTo = comparedTo.NullToEmpty();
            return this;
        }

        /// <summary>
        /// Prepare the message with the tokens in the template replaced
        /// by the set values.
        /// </summary>
        /// <returns></returns>
        public string Prepare()
        {
            var preparedMessage = this
                .template
                .Replace("{item}", this.item)
                .Replace("{value}", this.value)
                .Replace("{compared-to}", this.comparedTo)
                .Replace("{value-plural}", "s")
                .Replace("{compared-to-plural}", "s");
            return preparedMessage;
        }

        /// <summary>
        /// Return a delegate that will return the message with the tokens
        /// in the template replaced by the previously set values.
        /// </summary>
        /// <returns></returns>
        public Func<string> PrepareDelegate()
        {
            Func<string> messagePreparer = () =>
            {
                return this.Prepare();
            };
            return messagePreparer;
        }
    }
}
