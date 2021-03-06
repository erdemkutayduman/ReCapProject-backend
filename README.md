# Rent a Car Project
Qualified Software Developer Camp

# Packages
<b>Business</b><br>
-Autofac(6.1.0)<br>
-Autofac.Extras.DynamicProxy(6.0.0)<br>
-FluentValidation(9.5.1)<br>
-Microsoft.AspNetCore.Http(2.2.2)<br>
-Microsoft.AspNetCore.Http.Abstractions(2.2.0)<br>

<b>Core</b><br>
-Autofac(6.1.0)<br>
-Autofac.Extensions.DependencyInjection(7.1.0)<br>
-Autofac.Extras.DynamicProxy(6.0.0)<br>
-FluentValidation(9.5.1)<br>
-Microsoft.EntityFrameworkCore.SqlServer(3.1.11)<br>
-Microsoft.AspNetCore.Http(2.2.2)<br>
-System.IdentityModel.Tokens.Jwt(6.8.0)<br>
-Microsoft.IdentityModel.Tokens(6.8.0)<br>

<b>DataAccess</b><br>
-Microsoft.EntityFrameworkCore.SqlServer(3.1.11)<br>

<b>WebAPI</b><br>
-Autofac.Extensions.DependencyInjection(7.1.0)<br>
-Microsoft.AspNetCore.Authentication.JwtBearer(3.1.12)<br>

# Updates<br>
<b>Update 1</b><br>
-Entities, DataAccess, Business and Console layers were created.<br>
-A Car object was created and Id, BrandId, ColorId, ModelYear, DailyPrice, Description fields were added.<br>
GetById, GetAll, Add, Update, Delete operations were written in InMemory format.<br>

<b>Update 2</b><br>
-Brand and Color objects are added, Id and Name properties are added to both objects.<br>
-A new database was established on the SQL Server side, its name was determined as RentalCars and the tables of Cars, Brands, Colors were added.<br>
-Generic IEntityRepository infrastructure was written to the system<br>
-Entity Framework infrastructure was written for Car, Brand and Color objects.<br>

<b>Update 3</b><br>
-Core layer has been created.<br>
-Crud operations written for all classes.<br>
-IDto was created and necessary tables were joined.<br>

<b>Update 4</b><br>
-Result configuration has been added to the Core layer.<br>
-Customers and Users tables were also created and associated with each other.<br>
-Rental table, which holds car rental information, has also been added to the system.<br>

<b>Update 5</b><br>
-Web API layer has been created.<br>

<b>Update 6</b><br>
-Autofac and FluentValidation support has been added to the project.<br>
-AOP support added to the project.<br>

<b>Update 7</b><br>
-CarImage adding option through WebAPI has been attached to the project.<br>
-Microsoft.AspNetCore.Http(2.2.2) support added to the project.

<b>Update 8</b><br>
-JWT Support Added to the project.<br>
-Microsoft.AspNetCore.Http.Abstractions(2.2.0) support added to the project.<br>
-System.IdentityModel.Tokens.Jwt(6.8.0) support added to the project.<br>
-Microsoft.IdentityModel.Tokens(6.8.0) support added to the project.<br>
-Microsoft.AspNetCore.Authentication.JwtBearer(3.1.12) support added to the project.<br>

<b>Update 9</b><br>
-Cache, Transaction and Performance support added to the project.<br>

