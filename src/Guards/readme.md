# Guards
This library provides a set of validation extension methods that guard against conditions
not allowed for a function parameters or an object's state. They allow a natural calling
style for validation:


```c#
public void MyFunction( string name, int age)
{
    name.GuardIsSet();
    age.GuardInRange( 0, 109);
    //...
}
```

These are implemented as *guards*. This means that some validation logic is checked
and if the logic fails, an exception is throuwn.

## API
The following *guards* are available and they use a standard exception.

Type | Method | Use
-----|--------|-------
bool | GuardIsTrue | Guards that a condition is true.
bool | GuardIsFalse | Guards that a condition is false.
collection | GuardHasElements | Guard that a collection has any elementS. Used with `ICollection`.
collection | GuardHasItems | Guard that a collection has any items. Used with `ICollection<T>`
collection | GuardHasAtLeast | Guard that a collection has at least some required number of elements. Used with `ICollection`.
collection | GuardHasAtLeastItems | Guard that a collection has at least some required number of items. Used with `ICollection<T>`.
numeric | GuardMinimum | Guard that a comparable item is at least the minimum value.
numeric | GuardMaximum | Guard that a comparable item is at most the maximum value.
numeric | GuardInRange | Guard that a comparable item is within a range of values, inclusive.
object | GuardIsNotNull | Guard that an object is not null.
object | GuardIsRequiredForOperation | Guard that an object is not null.
object | GuardIsNotDefault | Guard that an object is not null or equal to the default value.
string | GuardIsSet | Guard that a string is not null, not an empty string and not a string of only white spaced.
string | GuardRequiredLength | Guard that a string is exactly the required length.
string | GuardSize | Guard that length of a string is within required range.

### Custom Exception Messages
All of the guards have a `WithMessage` variant that accepts a lambda that provides the message.