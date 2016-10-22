

public void GuardIsTrue( this bool value, string item )
{
    value
        .PrepareMessageTemplateGuard()
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
        .TestToExecute( () => value >= min )
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