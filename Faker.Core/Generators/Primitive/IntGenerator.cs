namespace Faker.Core.Generators.Primitive;

public class IntGenerator : IValueGenerator
{
    public bool CanGenerate(Type type) => type == typeof(int);

    public object Generate(Type typeToGenerate, GeneratorContext context)
        => context.Random.Next();
}