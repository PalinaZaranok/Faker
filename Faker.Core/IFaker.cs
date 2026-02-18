namespace Faker.Core;
using System;

public interface IFaker
{
    T Create<T>();
    object Create(Type type);
}