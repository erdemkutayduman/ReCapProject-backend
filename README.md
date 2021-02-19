# Rent a Car Project
Qualified Software Developer Camp

# Packages
<b>Business</b>
-Autofac(6.1.0)
-Autofac.Extras.DynamicProxy(6.0.0)
-FluentValidation(9.5.1)

<b>Core</b>
-Autofac(6.1.0)
-Autofac.Extensions.DependencyInjection(7.1.0)
-Autofac.Extras.DynamicProxy(6.0.0)
-FluentValidation(9.5.1)
-Microsoft.EntityFrameworkCore.SqlServer(3.1.11)

<b>DataAccess</b>
-Microsoft.EntityFrameworkCore.SqlServer(3.1.11)

<b>WebAPI</b>
-Autofac.Extensions.DependencyInjection(7.1.0)

# Updates
<b>Update 1</b>
-Entities, DataAccess, Business and Console layers were created.
-A Car object was created and Id, BrandId, ColorId, ModelYear, DailyPrice, Description fields were added.
GetById, GetAll, Add, Update, Delete operations were written in InMemory format.

<b>Update 2</b>
-Brand and Color objects are added, Id and Name properties are added to both objects.
-A new database was established on the SQL Server side, its name was determined as RentalCars and the tables of Cars, Brands, Colors were added.
-Generic IEntityRepository infrastructure was written to the system
Entity Framework infrastructure was written for Car, Brand and Color objects.

<b>Update 3</b>
-Core layer has been created.
-Crud operations written for all classes.
-IDto was created and necessary tables were joined.

<b>Update 4</b>
-Result configuration has been added to the Core layer.
-Customers and Users tables were also created and associated with each other.
-Rental table, which holds car rental information, has also been added to the system.

<b>Update 5</b>
Web API layer has been created.

<b>Update 6</b>
-Autofac and FluentValidation support has been added to the project.
-AOP support added to the project.
