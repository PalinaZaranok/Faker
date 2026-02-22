namespace Faker.Tests.TestModels;

public class MultiCtor
{
    public int A { get; }
    public int B { get; }

    public MultiCtor()
    {
    }

    public MultiCtor(int a)
    {
        A = a;
    }

    public MultiCtor(int a, int b)
    {
        A = a;
        B = b;
    }
}