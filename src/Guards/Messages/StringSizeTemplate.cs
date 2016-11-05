using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BagOUtils.Guards.Messages;

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
    public class StringSizeTemplate
    {
        private readonly string template;
        private string item;
        private string text;
        private int requierdLength;

        public StringSizeTemplate(string template)
        {
            this.template = template;
        }

        public StringSizeTemplate UsingItem(string item)
        {
            this.item = item.NullToEmpty();
            return this;
        }

        public StringSizeTemplate UsingValue(object value)
        {
            this.text = value.NullToEmpty();
            return this;
        }

        public StringSizeTemplate RequiringLength(int requiredLength)
        {
            this.requierdLength = requiredLength;
            return this;
        }

        public string Prepare()
        {
            var preparedMessage = this
                .template
                .Replace("{item}", this.item)
                .Replace("{value}", this.text)
                .Replace("{value-length", this.text.Length.ToString())
                .Replace("{required-length}", this.requierdLength.ToString())
                .Replace("{value-plural}", this.ValuePluralSuffix)
                .Replace("{required-plural}", this.RequiredPluralSuffix);
            return preparedMessage;
        }

        private string ValuePluralSuffix
        {
            get
            {
                return this.text.Length > 1 ? "s" : "";
            }
        }

        private string RequiredPluralSuffix
        {
            get
            {
                return this.requierdLength > 1 ? "s" : "";
            }
        }
    }
}
