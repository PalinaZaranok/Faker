using System.Linq.Expressions;

namespace Faker.Core;

public class FakerConfig
{
    private readonly Dictionary<(Type, string), IValueGenerator> _generators =
        new();

    public void Add<TClass, TProp, TGenerator>(
        Expression<Func<TClass, TProp>> expression)
        where TGenerator : IValueGenerator, new()
    {
        if (expression.Body is not MemberExpression member)
            throw new ArgumentException("Expression must be a property");

        var key = (typeof(TClass), member.Member.Name);
        _generators[key] = new TGenerator();
    }

    public bool TryGetGenerator(Type type, string memberName,
        out IValueGenerator generator)
    {
        foreach (var key in _generators.Keys)
        {
            if (key.Item1 == type &&
                string.Equals(key.Item2, memberName,
                    StringComparison.OrdinalIgnoreCase))
            {
                generator = _generators[key];
                return true;
            }
        }

        generator = null;
        return false;
    }

}