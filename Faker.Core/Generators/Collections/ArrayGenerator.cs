namespace Faker.Core.Generators.Collections;

public class ArrayGenerator : IValueGenerator
{
    public bool CanGenerate(Type type) => type.IsArray;

    public object Generate(Type typeToGenerate, GeneratorContext context)
    {
        var elementType = typeToGenerate.GetElementType();
        int count = context.Random.Next(1, 5);

        var array = Array.CreateInstance(elementType, count);

        for (int i = 0; i < count; i++)
        {
            array.SetValue(
                context.Faker.Create(elementType), i);
        }

        return array;
    }
}