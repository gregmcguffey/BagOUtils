

public void GuardIsTrue( this bool value, string item )
{
    // Note that setting up the guard is two step process:
    // The prepare statement creates a factory, then
    // the ThrowingException method on the factory is used
    // to actually create the guard. Have to do factory value
    // first, then exception to get most out of fluent interface.
    // I'll need a factory for each type (I think), because in 
    // order to make it generic, we're back to needing the exception.
    // Thus, each ThrowingException method will return the correct
    // type of guard.
    value
        .PrepareMessageTemplateGuard()
        .ThrowingException<ArgumentException>()
        .TestToExecute( () => value )
        .ExceptionBuilder( message => new InvalidOperationException(message)))
        .TemplateUsed( "Cannot complete operation. {0} must be true." )
        .NameTemplateUsed( "Cannot complete operation. {item} must be true." )
        .ForItem( item )
        .Guard();
}

public void GuardIsTrue( this bool value, string item, string message )
{
    value
        .PrepareMessageGuard()
        .TestToExecute( () => value )
        .ExceptionBuilder( () => new InvalidOperationException(message)))
        .Guard();
}

public string GuardIsSet( this string text, string item )
{
    text
        .PrepareItemTemplateGuard()
        .TestToExecute( () => !string.IsNullOrEmpty(text) )
        .ExceptionBuilder( (item, message) => new ArgumentException(message, item)))
        .TemplateUsed( "{0} must be set to a non-null, non-empty string." )
        .NameTemplateUsed( "{item} must be set to a non-null, non-empty string." )
        .ForItem( item )
        .Guard();
}

public string GuardIsSet( this string text, string item, string message )
{
    text
        .PrepareMessageGuard()
        .TestToExecute( () => !string.IsNullOrEmpty(text) )
        .ExceptionBuilder( () => new ArgumentException(message)))
        .Guard();
}

public int GuardMinimum( this int value, int min, string variableName )
{
    value
        .PrepareLimitGuard()
        // exception if test returns true or false? I think false.
        .CheckMin(min)
            .ExceptionThrown( message => new ArgumentOutOfRangeException( variableName, message) )
        .TemplateUsed( "{0} has a value of {1}, which is lower than the allowed limit of {2}" )
        .NamedTemplateused( "{item} has a value of {value}, which is lower than the allowed limit of {min}" )
        .ForItem( item )
        .Guard();
}

public int GuardMinimum( this int value, int min, string variableName, string message )
{
    value
        .PrepareMessageGuard()
        .TestToExecute( () => value >= min )
        .ExceptionThrown( () => new ArgumentOutOfRangeException( variableName, message) )
        .Guard();
}

// Example of how to implement factoryvoid Main()
{
    var value = 3;
    
    var g = value
        .PrepareGuard()
        .ThrowingException<ArgumentException>();
        
    g.Dump();
}

// Define other methods and classes here
public class Guard<T, E>
    where E : Exception
{
    public Guard(T value)
    {
        this.value = value;
    }

    private T value { get; set; }
}

public class AnotherGuard<T, E>
{
    public AnotherGuard(T value)
    {
        this.value = value;
    }

    private T value { get; set; }
}

public class Factory<T>
{
    private T value;
    
    public Factory(T value)
    {
        this.value = value;
    }

    public Guard<T, E> ThrowingException<E>()
        where E : Exception
    {
        var guard = new Guard<T, E>(this.value);
        return guard;
    }
}

public static class Extensions
{
    public static Factory<T> PrepareGuard<T>(this T value)
    {
        return new Factory<T>(value);
    }
}
