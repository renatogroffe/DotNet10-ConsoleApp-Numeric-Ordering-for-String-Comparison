using System.Globalization;
using System.Runtime.InteropServices;
using System.Text.Json;

Console.WriteLine("***** Testes com .NET 10 | Numeric Ordering for String Comparison *****");
Console.WriteLine($"Versao do .NET em uso: {RuntimeInformation
    .FrameworkDescription} - Ambiente: {Environment.MachineName} - Kernel: {Environment
    .OSVersion.VersionString}");

Console.WriteLine();
Console.WriteLine("*** Testando Equals() ***");

StringComparer numericStringComparer = StringComparer.Create(
    CultureInfo.CurrentCulture, CompareOptions.NumericOrdering);

var numbersEquality = new (string value1, string value2)[]
{
    ("007", "7"),
    ("8", "8"),
    ("020.5", "20.5"),
    ("30.49", "30.489")
};
foreach (var numbers in numbersEquality)
    Console.WriteLine($"{numbers.value1} == {numbers.value2} ? " +
        numericStringComparer.Equals(numbers.value1, numbers.value2));

Console.WriteLine();
Console.WriteLine("*** Testando Order() ***");

var dotnetVersions = new[] { "10.0", "7.0", "9", "8.0", "6" };
Console.WriteLine($"{nameof(dotnetVersions)} = {JsonSerializer.Serialize(dotnetVersions)}");

var dotnetVersionsOrdered = dotnetVersions.Order(numericStringComparer);
Console.WriteLine($"{nameof(dotnetVersionsOrdered)} = {JsonSerializer.Serialize(dotnetVersionsOrdered)}");

Console.WriteLine();
Console.WriteLine("*** Testando Contains() ***");

var csharpVersions = new HashSet<string>(numericStringComparer) { "08", "009", "0010", "011", "12", "13" };
for (int v = 7; v <= 14; v++)
    Console.WriteLine($"A versao {v} do C# esta na lista? {csharpVersions.Contains($"{v}")}");