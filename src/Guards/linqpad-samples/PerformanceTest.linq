<Query Kind="Program">
  <Reference Relative="..\bin\Debug\BagOUtils.Guards.dll">D:\gregmcguffey\BagOUtils\src\Guards\bin\Debug\BagOUtils.Guards.dll</Reference>
  <Namespace>BagOUtils.Guards.Framework</Namespace>
</Query>

/*
    Testing how much overhead using the Guard class adds.
    In general, it is pretty fast, but still 4x slower than
    straight up if.
*/
void Main()
{
    var stopWatch = new Stopwatch();

    Func<int, int, string, Guard<int, ArgumentOutOfRangeException>> guardMaker = (v, min, item) =>
      {
          var guard = new Guard<int, ArgumentOutOfRangeException>(v);
          guard
            .ExceptionBuilderUsed( ()=> new ArgumentOutOfRangeException(item, $"The value, {v}, is below allowed minimum ({min})."))
            .Test( () => v >= min);
          return guard;
      };

    var testCount = 1000000;
    var simpleResults = new List<long>();
    var fluentResults = new List<long>();
    var cachedResults = new List<long>();
    
    var minValue = 0;
    var value = 10;

    // Test simple first. We are interested in time with no exception.
    for (var i = 0; i < testCount; i++)
    {
        stopWatch.Start();
        value.GuardMin(minValue,"any");
        stopWatch.Stop();
        simpleResults.Add(stopWatch.ElapsedTicks);
        stopWatch.Reset();
    }

    // The main cost is creating the guard, but how much?
    for (var i = 0; i < testCount; i++)
    {
        stopWatch.Start();
        var guard = guardMaker(value, minValue, "any");
        guard.ValidateAndReturn();
        stopWatch.Stop();
        fluentResults.Add(stopWatch.ElapsedTicks);
        stopWatch.Reset();
    }

    // Does caching solve the problem?
    var cachedGuard = guardMaker(value, minValue, "any");
    for (var i = 0; i < testCount; i++)
    {
        stopWatch.Start();
        cachedGuard.ValidateAndReturn();
        stopWatch.Stop();
        cachedResults.Add(stopWatch.ElapsedTicks);
        stopWatch.Reset();
    }

    // Results
    TimeSpan.TicksPerMillisecond.Dump("Ticks/Millisecond");
    simpleResults.Average().Dump("Average for simple guard");
    fluentResults.Average().Dump("Average for fluent guard");
    cachedResults.Average().Dump("Average for cached guard");
    
    (fluentResults.Average()/simpleResults.Average()).Dump("fluent/simple");
    
    @"
So, while the fluent is about 4x slower than simple, it is only taking 
about 6/10,000 of a millisecond. So, we'll continue down the fluent path.
    ".Dump();
}

// Define other methods and classes here
public static class SimpleGuards
{
    public static int GuardMin(this int value, int min, string item)
    {
        if (value < min)
        {
            throw new ArgumentOutOfRangeException(item, $"The value, {value}, is below allowed minimum ({min}).");
        }
        return value;
    }
}