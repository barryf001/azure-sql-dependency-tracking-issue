using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Data.SqlClient;
using Newtonsoft.Json;

namespace SqlDependencyTrackingIssue
{
    public class TestFunctions
    {
        [FunctionName("TestSqlClientAzSql")]
        public async Task<IActionResult> TestSqlClientAzSql(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "test-sql-client-az-sql")] HttpRequest req, ILogger logger, ExecutionContext context = null)
        {
            logger.LogInformation("Starting test sql client (az sql)...");

            var azSqlDbConnectionString = "{YOUR_AZURE_SQL_DB_CONNECTION_STRING}";
            var azSqlTargetTableWithNumericPrimaryKey="{YOUR_AZURE_SQL_DB_TABLE_NAME_WHICH_HAS_A_NUMERIC_PRIMARY_KEY}";
            var azSqlTargetPrimaryKeyColumn="{YOUR_AZURE_SQL_DB_TABLE_PRIMARY_KEY}";
            var azSqlDbSqlCommandText = $"SELECT {azSqlTargetPrimaryKeyColumn} FROM {azSqlTargetTableWithNumericPrimaryKey}";

            var ids = "";
            using (var connection = new SqlConnection(azSqlDbConnectionString))
            {
                var command = connection.CreateCommand();
                command.CommandText = azSqlDbSqlCommandText;
                await connection.OpenAsync();
                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (reader.Read())
                    {
                        ids += $"{reader.GetInt32(0).ToString()},";
                    }

                    ids = ids.Substring(0, ids.Length - 1);
                }
            }
            return new OkObjectResult("Ids:" + ids);
        }
    }
}
