using OptionType;
using Shouldly;

namespace OptionTypeTests.Extensions;

public class PartialTests
{
    [Fact]
    public void Partial_AllTwoSet_ShouldReturnExpected()
    {
        // Arrange.
        const int expected = 30;
        const int a = 10;
        const int b = 20;
        Func<int, int> addInitialValue = a.Partial((int x, int y) => SumTwoValues(x, y));

        // Act.
        int total = addInitialValue(b);

        // Assert.
        total.ShouldBe(expected);
    }
    
    [Fact]
    public void Partial_AllThreeSet_ShouldReturnExpected()
    {
        // Arrange.
        const int expected = 42;
        const int a = 10;
        const int b = 20;
        const int c = 12;
        Func<int, Func<int, int>> addInitialValue = a.Partial((int x, int y, int z) => SumThreeValues(x, y, z));
        Func<int, int> addSecondValue = addInitialValue.Partial(b);

        // Act.
        int total = addSecondValue(c);

        // Assert.
        total.ShouldBe(expected);
    }
    
    [Fact]
    public void Partial_AllFourSet_ShouldReturnExpected()
    {
        // Arrange.
        const int expected = 55;
        const int a = 10;
        const int b = 20;
        const int c = 12;
        const int d = 13;
        Func<int, Func<int, Func<int, int>>> addInitialValue = a.Partial((int w, int x, int y, int z) => SumFourValues(w, x, y, z));
        Func<int, Func<int, int>> addSecondValue = addInitialValue.Partial(b);
        Func<int, int> addThirdValue = addSecondValue.Partial(c);

        // Act.
        int total = addThirdValue(d);

        // Assert.
        total.ShouldBe(expected);
    }
    
    [Fact]
    public void Partial_AllFiveSet_ShouldReturnExpected()
    {
        // Arrange.
        const int expected = 75;
        const int a = 10;
        const int b = 20;
        const int c = 12;
        const int d = 13;
        const int e = 20;
        Func<int, Func<int, Func<int, Func<int, int>>>> addInitialValue = a.Partial((int v, int w, int x, int y, int z) => SumFiveValues(v, w, x, y, z));
        Func<int, Func<int, Func<int, int>>> addSecondValue = addInitialValue.Partial(b);
        Func<int, Func<int, int>> addThirdValue = addSecondValue.Partial(c);
        Func<int, int> addFourthValue = addThirdValue.Partial(d);

        // Act.
        int total = addFourthValue(e);

        // Assert.
        total.ShouldBe(expected);
    }
    
    private static int SumTwoValues(int a, int b) => a + b;
    private static int SumThreeValues(int a, int b, int c) => a + b + c;
    private static int SumFourValues(int a, int b, int c, int d) => a + b + c + d;
    private static int SumFiveValues(int a, int b, int c, int d, int e) => a + b + c + d + e;
}
