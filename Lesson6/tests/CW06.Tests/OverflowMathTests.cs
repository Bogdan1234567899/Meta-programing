using Xunit;

namespace CW06.Tests;

public class OverflowMathTests
{
    [Fact]
    public void AddChecked_NormalAddition_Works()
    {
        Assert.Equal(7, CW06.OverflowMath.AddChecked(3, 4));
    }

    [Fact]
    public void AddChecked_Overflow_Throws()
    {
        Assert.Throws<OverflowException>(() => CW06.OverflowMath.AddChecked(int.MaxValue, 1));
    }

    [Fact]
    public void AddWrapped_Overflow_WrapsToMinValue()
    {
        Assert.Equal(int.MinValue, CW06.OverflowMath.AddWrapped(int.MaxValue, 1));
    }

    [Fact]
    public void AddWrapped_Underflow_WrapsToMaxValue()
    {
        Assert.Equal(int.MaxValue, CW06.OverflowMath.AddWrapped(int.MinValue, -1));
    }
}
