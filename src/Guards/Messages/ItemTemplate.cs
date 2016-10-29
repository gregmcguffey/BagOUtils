﻿using System;
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
        //-------------------------------------------------------------------------
        //
        // Defined Message Templates
        //
        //-------------------------------------------------------------------------

        public static ItemTemplate NotTrue
            = new ItemTemplate("The operation requires that '{item}' be true, but it is false.");

        public static ItemTemplate NotFalse
            = new ItemTemplate("The operation requires that '{item}' be false, but it is true.");

        public static ItemTemplate NoElements
            = new ItemTemplate("The collection, '{item}', has no elements.");

        public static ItemTemplate NoItems
            = new ItemTemplate("The collection, '{item}', has no items.");

        //-------------------------------------------------------------------------
        //
        // Instance 
        //
        //-------------------------------------------------------------------------

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

        public Func<string> PrepareDelegate()
        {
            return this.Prepare;
        }
    }
}
