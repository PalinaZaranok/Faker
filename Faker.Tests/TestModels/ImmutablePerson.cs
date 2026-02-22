using Faker.Core;

namespace Faker.Tests.TestModels;

public class ImmutablePerson
{
    public string Name { get; }

    public ImmutablePerson(string name)
    {
        Name = name;
    }
}

