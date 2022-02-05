# CoMApplicationDeveloper

## Setup
```
dotnet tool install --global dotnet-ef
dotnet add package Microsoft.EntityFrameworkCore.Design
dotnet ef migrations add InitialCreate
dotnet ef database update
```
## If getting the following error
![image](https://user-images.githubusercontent.com/5420965/152659490-65459c93-5eb5-4eb0-ab18-19b40538459e.png)

```
Add the following to the csproj file:

<PropertyGroup>
  <RollForward>Major</RollForward>
</PropertyGroup>

```
