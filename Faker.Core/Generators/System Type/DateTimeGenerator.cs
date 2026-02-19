namespace Faker.Core.Generators.System_Type;

public class DateTimeGenerator : IValueGenerator
{
    public bool CanGenerate(Type type) => type == typeof(DateTime);

    public object Generate(Type typeToGenerate, GeneratorContext context)
        => DateTime.Now.AddDays(context.Random.Next(-1000, 1000));
}