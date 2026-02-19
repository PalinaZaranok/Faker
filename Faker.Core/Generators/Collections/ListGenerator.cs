using System.Collections;

namespace Faker.Core.Generators.Collections;

public class ListGenerator : IValueGenerator
{
    public bool CanGenerate(Type type)
    {
        return type.IsGenericType &&
               type.GetGenericTypeDefinition() == typeof(List<>);
    }

    public object Generate(Type typeToGenerate, GeneratorContext context)
    {
        var elementType = typeToGenerate.GetGenericArguments()[0];
        var list = (IList)Activator.CreateInstance(typeToGenerate);

        int count = context.Random.Next(1, 5);

        for (int i = 0; i < count; i++)
        {
            list.Add(context.Faker.Create(elementType));
        }

        return list;
    }
}