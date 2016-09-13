using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BagOUtils.Messages
{
    /// <summary>
    /// Defines a message.
    /// </summary>
    /// <remarks>
    /// The message is typically a template and then has methods to
    /// build the message give some parameters.
    /// </remarks>
    public class Message
    {
        public static string DefaultLeftDelimiter { get; } = "{";
        public static string DefaultRightDelimiter { get; } = "}";

        private string leftDelimiter;
        private string rightDelimiter;

        public string Template { get; set; }
        public bool IsStatic { get; set; }
        public bool UsesStandardTokens { get; set; }
        public bool UsesNamedTokens { get; set; }

        public string LeftDelimiter
        {
            get
            {
                if(string.IsNullOrWhiteSpace(this.leftDelimiter))
                {
                    this.leftDelimiter = Message.DefaultLeftDelimiter;
                }
                return this.leftDelimiter;
            }
            set
            {
                this.leftDelimiter = value;
            }
        }
        public string RightDelimiter
        {
            get
            {
                if (string.IsNullOrWhiteSpace(this.rightDelimiter))
                {
                    this.rightDelimiter = Message.DefaultRightDelimiter;
                }
                return this.rightDelimiter;
            }
            set
            {
                this.rightDelimiter = value;
            }
        }

        public string Text()
        {
            if(!this.IsStatic)
            {
                throw new InvalidOperationException($"The message is not a static message. Use the appropriate build method. ({this.Template})");
            }
            return this.Template;
        }

        public string StandardBuild(params object[] args)
        {

            if (!this.UsesStandardTokens)
            {
                throw new InvalidOperationException($"The message is not configured as a standard string format message. Use the appropriate NamedBuild or the Text methods. ({this.Template})");
            }
            return string.Format(this.Template, args);
        }

        public string NamedBuild(string itemName)
        {
            if (!this.UsesStandardTokens)
            {
                throw new InvalidOperationException($"The message is not configured as a named token message. Use the appropriate StandardBuild or the Text methods. ({this.Template})");
            }
            throw new NotImplementedException();
        }
    }
}
