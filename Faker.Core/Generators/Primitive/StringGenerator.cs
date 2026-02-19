namespace Faker.Core.Generators.Primitive;

public class StringGenerator : IValueGenerator
{
    public bool CanGenerate(Type type) => type == typeof(string);

    public object Generate(Type typeToGenerate, GeneratorContext context)
    {
        int length = context.Random.Next(5, 15);
        const string chars = "abcdefghijklmnopqrstuvwxyz";
        return new string(Enumerable.Range(0, length)
            .Select(_ => chars[context.Random.Next(chars.Length)])
            .ToArray());
    }
}