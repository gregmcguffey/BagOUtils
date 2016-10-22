using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BagOUtils.Guards.Messages;

namespace BagOUtils.Guards.Framework
{
    /// <summary>
    /// Guard that tests numeric limits, such as 
    /// minimums, maximums and ranges.
    /// </summary>
    public class LimitGuard<TValue, TException>
        where TException : Exception
        where TValue : IComparable<TValue>
    {
        private readonly TValue value;

        private List<Func<bool>> tests;
        private TValue min;
        private bool isMinComparison;
        private TValue max;
        private bool isMaxComparison;
        private Func<string, TException> exceptionBuilder;
        private string template;
        private string nameTemplate;
        private string itemName;

        public LimitGuard(TValue value)
        {
            this.value = value;
            this.tests = new List<Func<bool>>();
        }

        public LimitGuard<TValue, TException> CheckMinimum(TValue minimum)
        {
            Func<bool> test = () => this.value.CompareTo(minimum) >= 0;
            this.tests.Add(test);
            this.isMinComparison = true;
            this.min = minimum;
            return this;
        }

        public LimitGuard<TValue, TException> CheckMaximum(TValue maximum)
        {
            Func<bool> test = () => this.value.CompareTo(maximum) <= 0;
            this.tests.Add(test);
            this.isMaxComparison = true;
            this.max = maximum;
            return this;
        }

        public LimitGuard<TValue, TException> CheckRange(TValue minimum, TValue maximum)
        {
            return this
                .CheckMinimum(minimum)
                .CheckMaximum(maximum);
        }

        public LimitGuard<TValue, TException> ExceptionBuilder(Func<string, TException> builder)
        {
            this.exceptionBuilder = builder;
            return this;
        }

        public LimitGuard<TValue, TException> TemplateUsed(string template)
        {
            this.template = template;
            return this;
        }

        public LimitGuard<TValue, TException> NameTemplateUsed(string nameTemplate)
        {
            this.nameTemplate = nameTemplate;
            return this;
        }

        public LimitGuard<TValue, TException> ForItem(string itemName)
        {
            this.itemName = itemName;
            return this;
        }

        public TValue Guard()
        {
            // Desired result is a true test.
            var results = this
                .tests
                .Select(test => test());
            var passed = !results.Contains(false);
            if (!passed)
            {
                var message = this
                    .value
                    .BuildLimitMessage()
                    .MaybeMin(this.isMinComparison, this.min)
                    .MaybeMax(this.isMaxComparison, this.max)
                    .BuildMessage();

                var ex = this.exceptionBuilder(message);
                throw ex;
            }
            return this.value;
        }

    }
}
