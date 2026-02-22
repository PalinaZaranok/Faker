using Faker.Core;
using Faker.Core.Generators.Primitive;
using Faker.Tests.TestModels;

namespace Faker.Tests;

public class FakerTests
{
    private readonly Core.Faker _faker = new();

    // primitives

    [Fact]
    public void Create_Int_ShouldNotBeDefault()
    {
        int value = _faker.Create<int>();
        Assert.NotEqual(default, value);
    }

    [Fact]
    public void Create_Double_ShouldNotBeDefault()
    {
        double value = _faker.Create<double>();
        Assert.NotEqual(default, value);
    }

    [Fact]
    public void Create_String_ShouldNotBeNullOrEmpty()
    {
        string value = _faker.Create<string>();
        Assert.False(string.IsNullOrEmpty(value));
    }

    [Fact]
    public void Create_DateTime_ShouldNotBeDefault()
    {
        DateTime value = _faker.Create<DateTime>();
        Assert.NotEqual(default, value);
    }
    
    //  class

    [Fact]
    public void Create_Class_ShouldFillProperties()
    {
        var user = _faker.Create<User>();

        Assert.NotNull(user);
        Assert.False(string.IsNullOrEmpty(user.Name));
        Assert.NotEqual(default, user.Age);
    }
    
    // multiple constructions
    

    [Fact]
    public void ShouldUseConstructorWithMostParameters()
    {
        var obj = _faker.Create<MultiCtor>();

        Assert.NotEqual(default, obj.A);
        Assert.NotEqual(default, obj.B);
    }
    
    // struct

    public struct MyStruct
    {
        public int Value { get; set; }
    }

    [Fact]
    public void ShouldCreateStruct()
    {
        var s = _faker.Create<MyStruct>();
        Assert.NotEqual(default, s.Value);
    }
    
    // list

    [Fact]
    public void ShouldCreateList()
    {
        var list = _faker.Create<List<int>>();

        Assert.NotNull(list);
        Assert.NotEmpty(list);
    }
    
    // array

    [Fact]
    public void ShouldCreateArray()
    {
        var array = _faker.Create<int[]>();

        Assert.NotNull(array);
        Assert.NotEmpty(array);
    }

    // nested collection

    [Fact]
    public void ShouldCreateNestedCollections()
    {
        var nested = _faker.Create<List<List<int>>>();

        Assert.NotNull(nested);
        Assert.NotEmpty(nested);
        Assert.NotEmpty(nested[0]);
    }
    
    // cyclic dependency

    [Fact]
    public void ShouldHandleCyclicDependencies()
    {
        var obj = _faker.Create<A>();

        Assert.NotNull(obj);
        Assert.NotNull(obj.B);
        Assert.NotNull(obj.B.C);

        // A внутри C должно быть null из-за разрыва цикла
        Assert.Null(obj.B.C.A);
    }
    
    // custom generator

    [Fact]
    public void ShouldUseCustomGenerator()
    {
        var config = new FakerConfig();
        config.Add<CustomClass, string, FixedStringGenerator>(c => c.Name);

        var faker = new Core.Faker(config);

        var obj = faker.Create<CustomClass>();

        Assert.Equal("CUSTOM_VALUE", obj.Name);

    }
    // immutable class

    [Fact]
    public void ShouldSupportImmutableClass()
    {
        var config = new FakerConfig();
        config.Add<ImmutablePerson, string, FixedStringGenerator>(p => p.Name);

        var faker = new Core.Faker(config);

        var person = faker.Create<ImmutablePerson>();

        Assert.Equal("CUSTOM_VALUE", person.Name);
    }
    
}