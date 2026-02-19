namespace Faker.Core.Generators.Primitive;

public class DoubleGenerator : IValueGenerator
{
    public bool CanGenerate(Type type) => type == typeof(double);

    public object Generate(Type typeToGenerate, GeneratorContext context)
        => context.Random.NextDouble();
}