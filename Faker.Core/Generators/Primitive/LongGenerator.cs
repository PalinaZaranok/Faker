namespace Faker.Core.Generators.Primitive;

public class LongGenerator : IValueGenerator
{
    public bool CanGenerate(Type type) => type == typeof(long);

    public object Generate(Type typeToGenerate, GeneratorContext context)
        => (long)context.Random.NextInt64();
}