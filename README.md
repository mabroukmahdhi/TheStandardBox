# TheStandardBox
TheStandardBox is a .NET library that provides essential code according to [The Standard](https://github.com/hassanhabib/The-Standard). The library allows you to use a standard storage broker, foundation service, and controller, making it easy to generate a controller based on only a model and a storage broker.

- TheStandardBox.Core : [![Nuget](https://img.shields.io/nuget/v/TheStandardBox.Core)](https://www.nuget.org/packages/TheStandardBox.Core/)
- TheStandardBox.Data : [![Nuget](https://img.shields.io/nuget/v/TheStandardBox.Data)](https://www.nuget.org/packages/TheStandardBox.Data/)
- TheStandardBox.Data.Tests.Unit : [![Nuget](https://img.shields.io/nuget/v/TheStandardBox.Data.Tests.Unit)](https://www.nuget.org/packages/TheStandardBox.Data.Tests.Unit/)

#### Installation
To install TheStandardBox, add the NuGet package to your API project:

```shell
NuGet\Install-Package TheStandardBox.Data -Version 1.0.4
```

## Usage

#### Adding Models
To use the library, your models, which represent your database entities, should implement the ```IStandardEntity``` interface of the library. For example, an entity
```WeatherForecast``` can be defined as follows:

```cs
[GeneratedController]
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
The ```[GeneratedController]``` attribute is used to generate a controller for the model
If you want to authorize the controller, you can add the ```[Authorize]``` attribute above the model class.

```cs
[GeneratedController]
[Authorize]
public class WeatherForecast : IStandardEntity
{
   ...
}
```

You can specific the allowed actions that you want to auto generate using the following example:

```cs
[GeneratedController("api/WeatherForecasts",
        allowedActions: new AllowedAction[] { AllowedAction.PostEntity })]
public class WeatherForecast : IStandardEntity
{
   ...
}
```

this can be also applied to an annonymous actions to remove the authorization from specific controller action:
```cs
[GeneratedController("api/WeatherForecasts",
        allowedActions: new AllowedAction[] { AllowedAction.PostEntity }),
        anonymousActions: new AllowedAction[] { AllowedAction.PostEntity })]
public class WeatherForecast : IStandardEntity
{
   ...
}
```
this will make the ``POST`` execute without Authorization

#### IStandardEntity
IStandardEntity, defines the following three properties that must be implemented by any model that implements this interface:

- ```Guid Id:``` A unique identifier for the entity.
- ```DateTimeOffset CreatedDate:``` The date and time the entity was created.
- ```DateTimeOffset UpdatedDate:``` The date and time the entity was last updated.

```cs
    public interface IStandardEntity
    {
        Guid Id { get; set; }
        DateTimeOffset CreatedDate { get; set; }
        DateTimeOffset UpdatedDate { get; set; }
    }
```

#### Standard Storage Broker
To define your ```DbSets```, you need to create a new storage broker that inherits from ```StandardStorageBroker```. For example:

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

By default, TheStandardBox reads the connection string from your configuration by the name ```DefaultConnection```. You can define the name of your connection string using the ```DefaultConnectionName``` property.

You can also override the ```UsesNoTrackingBehavior``` property to use the ```NoTracking``` behavior or not. (By default `true`):
#### Uses NoTracking Behavior

```cs
protected override bool UsesNoTrackingBehavior => false;
```

#### Adding TheStandardBox
Add the library to your project in the ```Program.cs``` file by calling the following:

```cs
builder.Services.AddTheStandardBoxData<StorageBroker>();
```
#### Migrations
Dont forget to add a migration by running `> Add-Migration [MIGRATION_NAME]`.

#### Foundation Service
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
This interface will automatically generate unit tests.

## Support
For any question or information please contact us on: [contact@mahdhi.com](mailto:contact@mahdhi.com)
