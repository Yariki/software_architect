# How to setup secrets

1. Navigate to the folder with project. In out case it's __CatalogService.Api__ or __CartingService__
2. Initialize secrets with command:
```ps
dotnet user-secrets init
```
3. Set the secret with the command:
```ps
dotnet user-secrets set "<KEY>" "<VALUE>"
```
KEY - "ServicecBus:ConnectionString"

4. To show the list of secrets use next command:
```ps
dotnet user-secrets list
```
5. Remove a single secret
```ps
dotnet user-secrets remove "<KEY>"
```
6. Remove all secrets
```ps
dotnet user-secrets clear
```

# Install dotnet-format

```ps
dotnet tool install -g dotnet-format
```