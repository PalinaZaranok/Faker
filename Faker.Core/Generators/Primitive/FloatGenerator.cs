namespace Faker.Core.Generators.Primitive;

public class FloatGenerator : IValueGenerator
{
    public bool CanGenerate(Type type) => type == typeof(float);

    public object Generate(Type typeToGenerate, GeneratorContext context)
        => (float)context.Random.NextDouble();
}