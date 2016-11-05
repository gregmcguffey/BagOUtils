using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BagOUtils.Guards.Messages
{

    /// <summary>
    /// Container for templates that have custom defined replacement values needed.
    /// </summary>
    public static class CustomTemplate
    {
        public static SingleValueComparisonTemplate BelowMinimum
            = new SingleValueComparisonTemplate("The value of '{item}' ({value}) is below the minimum limit of {compare-to}.");

        public static SingleValueComparisonTemplate AboveMaximum
            = new SingleValueComparisonTemplate("The value of '{item}' ({value}) is above the maximum limit of {compare-to}.");

        public static RangeTemplate OutOfRange
            = new RangeTemplate("The value of '{item}' ({value}) is not within the required range of {min} to {max}.");

        public static StringSizeTemplate NotRequiredSize
            = new StringSizeTemplate("The '{item}', with a value of '{value}', must be {required-length} character{required-plural} long, but was {value-length} character{value-plural} long.");

        public static SingleValueComparisonTemplate TooFewElements
            = new SingleValueComparisonTemplate("The collection '{item}' has {value} elements, but must have at least {compare-to} elements.");

        public static StringLengthRangeTemplate TextSizeOutOfRange
            = new StringLengthRangeTemplate("The length of '{item}' ('{value}') was {value-len}. This is not within the required range of {min} to {max}.");
    }
}
