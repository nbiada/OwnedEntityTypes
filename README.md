# OwnedEntityTypes
An example how to use Owned Entity Types in EntityFramework Core,\
see it in action https://ownedentitytypes-vitalmente.azurewebsites.net/ !

With **Owned Entity Types** you can create _sub entities_ for your EF Core entities.\
Imagine you have a `Person` entity and you want to add an `Address` entity.

The resulting Person entity will be:
```csharp
public class Person {
    public string Id {get; set;}
    public string FirstName {get; set;}
    public string LastName {get; set;}
    /* Address fields */
    public string Street {get; set;}
    public string City {get; set;}
    public string Country {get; set;}
}
```
Now imagine that you want to create a reusable class for all kind of Address.\
Tipically the solution is to create a joined table with a foreign key.

This is the reason why the **Owned Entity Types** have been invented: to simplify the development of related entities.

In action we have:

```csharp
[Owned]
public class Address {
    public string Street {get; set;}
    public string City {get; set;}
    public string Country {get; set;}
}

public class Person {
    public string Id {get; set;}
    public string FirstName {get; set;}
    public string LastName {get; set;}
    public Address StreetAddress {get; set;}
}
```

Now we have a navigation like `Person.StreetAddress.City`.

On the database the EF Core works its magic:

| Field | Type |
| --- | --- |
| Id | int |
| FirstName | nvarchar(MAX) |
| LastName | nvarchar(MAX) |
| StreetAddress_Street | nvarchar(MAX) |
| StreetAddress_City | nvarchar(MAX) |
| StreetAddress_Country | nvarchar(MAX) |

## Assigning values
Look at this piece of code for an example of assigning values:

```csharp
var person = new Person();
person.FirstName = 'Ted';
person.LastName = 'Wilson';
person.StreetAddress = new Address();
person.StreetAddress.City = 'London';

_context.Person.Add(person);
_context.SaveChanges();
```
or
```csharp
var person = new Person();
person.FirstName = 'Ted';
person.LastName = 'Wilson';

var streetAddress = new Address();
streetAddress.City = 'London';

person.StreetAddress = streetAddress;

_context.Person.Add(person);
_context.SaveChanges();
```
