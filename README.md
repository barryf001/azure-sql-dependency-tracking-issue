# azure-sql-dependency-tracking-issue

The following variables found in TestFunctions.cs need to be replaced with your enviromnent config.

```csharp
var azSqlDbConnectionString = "{YOUR_AZURE_SQL_DB_CONNECTION_STRING}";
var azSqlTargetTableWithNumericPrimaryKey="{YOUR_AZURE_SQL_DB_TABLE_NAME_WHICH_HAS_A_NUMERIC_PRIMARY_KEY}";
var azSqlTargetPrimaryKeyColumn="{YOUR_AZURE_SQL_DB_TABLE_PRIMARY_KEY}";
```

The provided sql response processing assumes the retrieval of a numeric primary key value from a test table located in an Azure sql database.
