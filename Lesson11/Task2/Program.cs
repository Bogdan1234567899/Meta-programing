using System;
using System.Reflection;
using System.Text;

Console.OutputEncoding = Encoding.UTF8;

MathOperations m = new MathOperations();

Type t = typeof(MathOperations);
MethodInfo mi = t.GetMethod("Add");

object[] a = new object[] { 5, 10 };
object result = mi.Invoke(m, a);

Console.WriteLine("Add(5, 10) = " + result);

class MathOperations
{
    public int Add(int x, int y) { return x + y; }
    public int Sub(int x, int y) { return x - y; }
    public int Mul(int x, int y) { return x * y; }
    public int Div(int x, int y) { return x / y; }
}
