using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BagOUtils.Guards.Messages
{
    /// <summary>
    /// Class that defines templates that take the name of an item.
    /// </summary>
    public class ItemTemplate
    {
        private readonly string template;
        private string item;

        public ItemTemplate(string template)
        {
            this.template = template;
        }

        public ItemTemplate UsingItem(string item)
        {
            this.item = item.NullToEmpty();
            return this;
        }

        public string Prepare()
        {
            var preparedMessage = this
                .template
                .Replace("{item}", this.item);
            return preparedMessage;
        }
    }
}
