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
    /// <remarks>
    /// This can be used templates that need the item and then some othe information
    /// also. E.g. the name of the arguemnt and an operation that requires it or the
    /// name of an argument and type of that argument.
    /// </remarks>
    public class ItemValueTemplate
    {
        //-------------------------------------------------------------------------
        //
        // Defined Message Templates
        //
        //-------------------------------------------------------------------------

        public static ItemValueTemplate MissingForOperation
            = new ItemValueTemplate("The operation, '{value}', requires a value for '{item}' which is null.");

        public static ItemValueTemplate DefaultNotAllowed
            = new ItemValueTemplate("The value of '{item}' cannot be the default value for its type ({value}).");

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
