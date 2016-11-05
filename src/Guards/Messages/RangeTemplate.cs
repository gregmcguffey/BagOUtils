using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BagOUtils.Guards.Messages
{
    /// <summary>
    /// Define a template for a messages that are range related. I.e. the
    /// value must be between two values.
    /// </summary>
    public class RangeTemplate
    {
        private readonly string template;
        private string item;
        private string value;
        private string minValue;
        private string maxValue;

        public RangeTemplate(string template)
        {
            this.template = template;
        }

        public RangeTemplate UsingItem(string item)
        {
            this.item = item.NullToEmpty();
            return this;
        }

        public RangeTemplate UsingValue(object value)
        {
            this.value = value.NullToEmpty();
            return this;
        }

        public RangeTemplate WithMinimum(object min)
        {
            this.minValue = min.NullToEmpty();
            return this;
        }

        public RangeTemplate WithMaximum(object max)
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
                .Replace("{min}", this.minValue)
                .Replace("{max}", this.maxValue);
            return preparedMessage;
        }
    }
}
