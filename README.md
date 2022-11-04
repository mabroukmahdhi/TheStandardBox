# TheStandardBox
A .NET Library for essential code according to [The Standard](https://github.com/hassanhabib/The-Standard)

- TheStandardBox.Core : [![Nuget](https://img.shields.io/nuget/v/TheStandardBox.Core)](https://www.nuget.org/packages/TheStandardBox.Core/)
- TheStandardBox.Data : [![Nuget](https://img.shields.io/nuget/v/TheStandardBox.Data)](https://www.nuget.org/packages/TheStandardBox.Data/)
- TheStandardBox.Data.Tests.Unit : [![Nuget](https://img.shields.io/nuget/v/TheStandardBox.Data.Tests.Unit)](https://www.nuget.org/packages/TheStandardBox.Data.Tests.Unit/)

## Installation
Install the nuget package in your API project:
```shell
NuGet\Install-Package TheStandardBox.Data -Version 1.0.0
```

## How to use?
This library will allow to you to use easly a sort of standard Storage Broker, Foundation Servicer and Controller:

### Add your models
Your models, that represents your database entities, should implement the interface `IStandardEntity` of the library:

```cs
    public interface IStandardEntity
    {
        Guid Id { get; set; }

        DateTimeOffset CreatedDate { get; set; }

        DateTimeOffset UpdatedDate { get; set; }
    }
```

For example an entity `WeatherForecast` looks like:

```cs
    public class WeatherForecast : IStandardEntity
    {
        public Guid Id { get; set; }

        public DateTime Date { get; set; }

        public int TemperatureC { get; set; }

        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);

        public string Summary { get; set; }

        public DateTimeOffset CreatedDate { get; set; }

        public DateTimeOffset UpdatedDate { get; set; }
    }
```

### Standard Storage Broker
In order to define your `DbSets` ou need to create a new storage broker that inhirates from `StandardStorageBroker`:

```cs
    public class StorageBroker : StandardStorageBroker
    {
        public StorageBroker(IConfiguration configuration)
            : base(configuration)
        { }

        public DbSet<WeatherForecast> WeatherForecasts { get; set; } 
    }
```
#### Connection String
By default TheStandardBox reads the connection string from your configuration by the name `DefaultConnection`. 
You can define the name of your connection string:

```cs
    public class StorageBroker : StandardStorageBroker
    {
        public StorageBroker(IConfiguration configuration)
            : base(configuration)
        { }

        protected override string DefaultConnectionName => "NEW_CONNECTION_STRING_NAME";

        public DbSet<WeatherForecast> WeatherForecasts { get; set; } 
    }
```

#### Uses NoTracking Behavior
To use the `NoTracking` behavior or not, override the property `UsesNoTrackingBehavior` (By default `true`):

```cs
    protected override bool UsesNoTrackingBehavior => false;
```

### Add TheStandard Box
Add the library as following in the `Program.cs` file of your API project:

```cs
    builder.Services.AddTheStandardBoxData<StorageBroker>();
```
### Migrations
Dont forget to add a migration by running `> Add-Migration [MIGRATION_NAME]`.

### Foundation Service
For each entity add a standard foundation service as following:

```cs
     builder.Services.AddStandardFoundationService<WeatherForecast>();
```
## Unit testing ?
It is very important to unit test your services, using TheStandardBox this is now very easy. You need only to create your test class as following:

```cs
    public class WeatherServiceTests : StandardServiceTests<WeatherForecast>
    { }
```
## Support
For any question or infromation: contact@mahdhi.com
