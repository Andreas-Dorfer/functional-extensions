[![NuGet Package](https://img.shields.io/nuget/v/ad.functionalextensions.svg)](https://www.nuget.org/packages/AD.FunctionalExtensions/)
# Functional Extensions for C#
Some basic F# features for C#.
## NuGet Package
    PM> Install-Package AD.FunctionalExtensions -Version 1.0.0
## Option
See [Options](https://docs.microsoft.com/en-us/dotnet/fsharp/language-reference/options) in the F# docs.
```csharp
var a = Option.Some(1);
var none = Option<int>.None;
```
### Get the Value out of an Option
```csharp
var a = Option.Some(1);
if (a.IsSome(out var value))
{
    Console.WriteLine(value);
}
```
## Pattern Matching
```csharp
var a = Option.Some("functional extensions for c#");

var upperText = a.Match(
    onIsSome: value => value.ToUpper(),
    onIsNone: () => string.Empty);
```
## Bind
See [bind](https://msdn.microsoft.com/visualfsharpdocs/conceptual/option.bind%5b%27t%2c%27u%5d-function-%5bfsharp%5d) in the F# docs.
```csharp
var a = Option.Some('C');

var message = a.Bind(value =>
{
    switch (value)
    {
        case 'A': return 10.Some();
        case 'B': return 11.Some();
        case 'C': return 12.Some();
        case 'D': return 13.Some();
        case 'E': return 14.Some();
        case 'F': return 15.Some();
        default: return Option<int>.None;
    }
});
```
## Map
See [map](https://msdn.microsoft.com/en-us/visualfsharpdocs/conceptual/option.map%5b't,'u%5d-function-%5bfsharp%5d) in the F# docs.
```csharp
var a = Option.Some(7);

var b = a.Map(value => value * 2);
```