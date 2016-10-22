using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BagOUtils.Guards.Messages
{
    /// <summary>
    /// Templates for failed limit validation messages.
    /// </summary>
    public class LimitMessageTemplate
    {
        private static List<Tuple<bool, bool, string>> templateLookup;

        static LimitMessageTemplate()
        {
            templateLookup = new List<Tuple<bool, bool, string>>();
            var minEntry = Tuple.Create(true, false, minTemplate);
            var maxEntry = Tuple.Create(false, true, maxTemplate);
            var rangeEntry = Tuple.Create(true, true, inRangeTemplate);
            var badEntry = Tuple.Create(false, false, invalidTemplate);
            templateLookup.Add(minEntry);
            templateLookup.Add(maxEntry);
            templateLookup.Add(rangeEntry);
            templateLookup.Add(badEntry);
        }

        public static string minTemplate = "The value, {0}, is below the minimum limit of {1}.";
        public static string maxTemplate = "The value, {0}, is above the maximum limit of {2}.";
        public static string inRangeTemplate = "The value, {0}, was not within the expected range of {1} to {2}.";
        public static string invalidTemplate = "Invalid limit test: no limits set.";

        public static string GetTemplate(bool isMinLimitSet, bool isMaxLimitSet)
        {
            return templateLookup
                 .Where(i => i.Item1 == isMinLimitSet && i.Item2 == isMaxLimitSet)
                 .Select(i => i.Item3)
                 .FirstOrDefault();
        }
    }
}
