using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BagOUtils.Guards.Messages
{
    /// <summary>
    /// Define a template that that takes an item name and the value of the item.
    /// </summary>
    public class ItemValueTemplate
    {
        //-------------------------------------------------------------------------
        //
        // Defined Message Templates
        //
        //-------------------------------------------------------------------------


        //-------------------------------------------------------------------------
        //
        // Instance
        //
        //-------------------------------------------------------------------------

        private readonly string template;
        private string item;
        private string value;

        public ItemValueTemplate(string template)
        {
            this.template = template;
        }

        public ItemValueTemplate UsingItem(string item)
        {
            this.item = item.NullToEmpty();
            return this;
        }

        public ItemValueTemplate UsingValue(object value)
        {
            this.value = value.NullToEmpty();
            return this;
        }

        public string Prepare()
        {
            var preparedMessage = this
                .template
                .Replace("{item}", this.item)
                .Replace("{value}", this.value);
            return preparedMessage;
        }
    }
}
