using Microsoft.Azure.Cosmos.Table;
using System.Threading.Tasks;

public class TableStorageService<T> where T : TableEntity, new()
{
    private readonly CloudTable _table;

    public TableStorageService(string connectionString, string tableName)
    {
        var storageAccount = CloudStorageAccount.Parse(connectionString);
        var tableClient = storageAccount.CreateCloudTableClient(new TableClientConfiguration());
        _table = tableClient.GetTableReference(tableName);
        _table.CreateIfNotExists();
    }

    public async Task InsertOrMergeEntityAsync(T entity)
    {
        var operation = TableOperation.InsertOrMerge(entity);
        await _table.ExecuteAsync(operation);
    }

    public async Task<T> RetrieveEntityAsync(string partitionKey, string rowKey)
    {
        var operation = TableOperation.Retrieve<T>(partitionKey, rowKey);
        var result = await _table.ExecuteAsync(operation);
        return result.Result as T;
    }
}


