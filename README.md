# CinemaCat

CinemaCat is a Web API for storing and retrieving information about favourite movies and their creators.

## Dependencies

You need Docker to make it work. There is CinemaCat\docker-compose.yaml file to use. Run this command in the directory of the solution:

```console
docker-compose up
```

## Build & Run
Run these commands in the directory of the solution:

```console
dotnet build CinemaCat.Api.csproj
dotnet run CinemaCat.Api.csproj
```

## Unit tests
Run these commands in the directory of the solution:

```console
dotnet test
```

## Usage
You can run swagger on URLs your app listens on + /swagger/index.html to see available endpoints.
![image](https://github.com/mila-laurel/CinemaCat/assets/62745003/7d286a97-904b-4f0c-a721-0de10ebacdcd)

You need to be authorized to run endpoints:
 - use /api/Users/signup endpoint to create an account
 - use /api/Users/signin to get a valid token

