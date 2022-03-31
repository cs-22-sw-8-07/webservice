# webservice

## Settings files

The project and the test environment uses ASP.NET 6.0

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

Install the correct packages on Python
```
python -m pip install spotipy
```

## Unit test
In order to run the unit test add file "quack_api/quack_api.Test/appsettings.test.json" with contents:

```
{
  "RecommenderSettings": {
    "RecommenderPath": "",
    "PythonPath": ""
  }
}
```
Add your python to the python installation (PythonPath).
Add the path to the python file called "main.py" under "quack_api/quack_api.Test/Resources/Test_recommender"
