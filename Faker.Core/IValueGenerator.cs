namespace Faker.Core;

public interface IValueGenerator
{
    bool CanGenerate(Type type);
    object Generate(Type typeToGenerate, GeneratorContext context);
}