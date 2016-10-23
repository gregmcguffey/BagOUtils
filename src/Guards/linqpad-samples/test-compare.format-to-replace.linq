<Query Kind="Statements" />

/*
    Test to validate that using string.Replace is not a huge performance
    hit. This compares using string.Replace to string.Format by performing
    the same replacement for some larget number of runs. Up to 10,000,000
    tests is pretty quick.
    
    Results show it is about .3 ticks slower. Well worth the readibility gain.
*/

// How many tests to run.
var testCount = 1000000;

// Two methods we will test.
Func<string, string, string, string> formatReplacer = (item, value, template) => string.Format(template, item, value);
Func<string, string, string, string> nameReplacer = (item, value, template) =>
{
    return template
        .Replace("{item}", item)
        .Replace("{value}", value);
};

var watch = new Stopwatch();
var testRange = Enumerable.Range(0, testCount - 1);

var testItem = "item";
var testValue = "value";

var formatTemplate = "The item, '{0}', has a value of '{1}'.";
var nameTemplate = "The item, '{item}', has a value of '{value}'.";

// Test using string.Format.
var formatResults = testRange
    .Select(n =>
    {
        watch.Start();
        formatReplacer(testItem, testValue, formatTemplate);
        watch.Stop();
        var replaceTime = watch.ElapsedTicks;
        watch.Reset();
        return replaceTime;
    });

// Test using named tokens via Replace.
var nameResults = testRange
    .Select(n =>
    {
        watch.Start();
        nameReplacer(testItem, testValue, nameTemplate);
        watch.Stop();
        var replaceTime = watch.ElapsedTicks;
        watch.Reset();
        return replaceTime;
    });

// Create a list of differences
var diff = testRange
    .Select(n => nameResults.ToList()[n] - formatResults.ToList()[n] );

// Results
$"Averaged over {testCount} executions".Dump("Test Count");
TimeSpan.TicksPerMillisecond.Dump("Ticks per Millisecond");

var formatAverage = formatResults.Average();
formatAverage.Dump("string.Format Average Ticks");

var nameAverage = nameResults.Average();
nameAverage.Dump("Replace Average Ticks");

var diffAverage = nameAverage - formatAverage;
diffAverage.Dump("Average difference between replace and format. Smaller is better.");