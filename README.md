# webservice

## Settings files

Add a file "quack_api/quack_api/appsettings.json" with contents:

```
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft": "Warning",
      "Microsoft.Hosting.Lifetime": "Information"
    }
  },
  "RecommenderSettings": {
    "RecommenderPath": "",
    "PythonPath": ""
  },
  "AllowedHosts": "*"
}
```

Add your path to the recommender_system repository (RecommenderPath) and python installation (PythonPath).


And a file "quack_api/quack_api/appsettings.Development.json" with contents:

```
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft": "Warning",
      "Microsoft.Hosting.Lifetime": "Information"
    }
  }
}
```
