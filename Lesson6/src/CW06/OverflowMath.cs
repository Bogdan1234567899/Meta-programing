namespace CW06;

public static class OverflowMath
{
    public static int AddChecked(int a, int b)
    {
        checked
        {
            return a + b;
        }
    }

    public static int AddWrapped(int a, int b)
    {
        unchecked
        {
            return a + b;
        }
    }
}
