# CinemaCat

CinemaCat is a Web API for storing and retrieving information about favourite movies and their creators.

## Installation

You need Docker to make it work. There is docker-compose.yaml file to use. Run this command in the directory of the solution:

```console
docker-compose up
```

## Usage
Run these commands in the directory of the CinemaCat.Api project:

```console
dotnet build
dotnet run
```

You can run swagger on URLs your app listens on + /swagger/index.html to see available endpoints.
![image](https://github.com/mila-laurel/CinemaCat/assets/62745003/7d286a97-904b-4f0c-a721-0de10ebacdcd)

You need to be authorized to run endpoints:
 - use /api/Users/signup endpoint to create an account
 - use /api/Users/signin to get a valid token

