using System.Reflection;
using Faker.Core.Generators.Collections;
using Faker.Core.Generators.Primitive;
using Faker.Core.Generators.System_Type;

namespace Faker.Core;

public class Faker : IFaker
{
    private readonly List<IValueGenerator> _generators;
    private readonly FakerConfig _config;
    private readonly Random _random = new();
    private readonly HashSet<Type> _creationStack = new();

    public Faker(FakerConfig config = null)
    {
        _config = config ?? new FakerConfig();

        _generators = new List<IValueGenerator>
        {
            new IntGenerator(),
            new LongGenerator(),
            new DoubleGenerator(),
            new FloatGenerator(),
            new StringGenerator(),
            new DateTimeGenerator(),
            new ListGenerator(),
            new ArrayGenerator()
        };
    }

    public T Create<T>()
    {
        return (T)Create(typeof(T));
    }

    public object Create(Type type)
    {
        if (_creationStack.Contains(type))
            return GetDefault(type);

        var generator = _generators.FirstOrDefault(g => g.CanGenerate(type));
        if (generator != null)
            return generator.Generate(type, new GeneratorContext(_random, this));

        _creationStack.Add(type);

        try
        {
            return CreateComplex(type);
        }
        finally
        {
            _creationStack.Remove(type);
        }
    }

    private object CreateComplex(Type type)
    {
        // ===== STRUCT SUPPORT =====
        if (type.IsValueType && !type.IsPrimitive && !type.IsEnum)
        {
            object structInstance = Activator.CreateInstance(type);
            structInstance = FillMembers(type, structInstance);
            return structInstance;
        }

        var constructors = type
            .GetConstructors(BindingFlags.Public |
                             BindingFlags.NonPublic |
                             BindingFlags.Instance)
            .OrderByDescending(c => c.GetParameters().Length);

        foreach (var ctor in constructors)
        {
            try
            {
                var parameters = ctor.GetParameters()
                    .Select(p => CreateParameter(type, p))
                    .ToArray();

                var instance = ctor.Invoke(parameters);

                instance = FillMembers(type, instance);

                return instance;
            }
            catch
            {
                continue;
            }
        }

        return GetDefault(type);
    }

    private object CreateParameter(Type parentType, ParameterInfo param)
    {
        if (_config.TryGetGenerator(parentType, param.Name,
                out var generator))
        {
            return generator.Generate(param.ParameterType,
                new GeneratorContext(_random, this));
        }

        return Create(param.ParameterType);
    }

    private object FillMembers(Type type, object instance)
    {
        if (instance == null)
            return null;

        // ===== PROPERTIES =====
        var properties = type.GetProperties(BindingFlags.Public |
                                            BindingFlags.Instance)
            .Where(p => p.CanWrite);

        foreach (var prop in properties)
        {
            if (_config.TryGetGenerator(type, prop.Name,
                    out var generator))
            {
                prop.SetValue(instance,
                    generator.Generate(prop.PropertyType,
                        new GeneratorContext(_random, this)));
            }
            else
            {
                prop.SetValue(instance,
                    Create(prop.PropertyType));
            }
        }

        // ===== FIELDS =====
        var fields = type.GetFields(BindingFlags.Public |
                                    BindingFlags.Instance)
            .Where(f => !f.IsInitOnly);

        foreach (var field in fields)
        {
            if (_config.TryGetGenerator(type, field.Name,
                    out var generator))
            {
                field.SetValue(instance,
                    generator.Generate(field.FieldType,
                        new GeneratorContext(_random, this)));
            }
            else
            {
                field.SetValue(instance,
                    Create(field.FieldType));
            }
        }

        return instance;
    }

    private static object GetDefault(Type t)
    {
        return t.IsValueType
            ? Activator.CreateInstance(t)
            : null;
    }
}