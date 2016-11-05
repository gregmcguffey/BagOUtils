using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BagOUtils.Guards.Messages
{
    /// <summary>
    /// Define a template for a messages that are related to the
    /// length of a string being within a certain range.
    /// </summary>
    public class StringLengthRangeTemplate
    {
        private readonly string template;
        private string item;
        private string value;
        private string minValue;
        private string maxValue;

        public StringLengthRangeTemplate(string template)
        {
            this.template = template;
        }

        public StringLengthRangeTemplate UsingItem(string item)
        {
            this.item = item.NullToEmpty();
            return this;
        }

        public StringLengthRangeTemplate UsingValue(object value)
        {
            this.value = value.NullToEmpty();
            return this;
        }

        public StringLengthRangeTemplate WithMinimum(object min)
        {
            this.minValue = min.NullToEmpty();
            return this;
        }

        public StringLengthRangeTemplate WithMaximum(object max)
        {
            this.maxValue = max.NullToEmpty();
            return this;
        }

        public string Prepare()
        {
            var preparedMessage = this
                .template
                .Replace("{item}", this.item)
                .Replace("{value}", this.value)
                .Replace("{value-len}", (this.value?.Length ?? 0).ToString())
                .Replace("{min}", this.minValue)
                .Replace("{max}", this.maxValue);
            return preparedMessage;
        }
    }
}
