namespace Faker.Core.Generators.Primitive;

public class FixedStringGenerator : IValueGenerator
{
    public bool CanGenerate(Type type) => type == typeof(string);

    public object Generate(Type typeToGenerate, GeneratorContext context)
        => "CUSTOM_VALUE";
}