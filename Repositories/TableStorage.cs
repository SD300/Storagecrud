using Azure.Data.Tables;
using StorageAccount.DOT;

namespace StorageAccount.Repository
{
    public class TableStorage
    {
        static string connectionString="DefaultEndpointsProtocol=https;AccountName=azurestorage20;AccountKey=i8aXLBGaYBH7TLVpNMxc72zm3jzGcDdnk2XT7x1xG6ZGUG44J8f0GWoCnON2puaNvtKLksJIJMHD+AStDasE/g==;EndpointSuffix=core.windows.net";
        public static async Task AddTable(string tableName)
        {
            var data=new TableServiceClient(connectionString);
            var client=data.GetTableClient(tableName);
            await client.CreateIfNotExistsAsync();
        }
        public static async Task<Details> UpdateTable(Details employee,string tableName)
        {
            var data=new TableServiceClient(connectionString);
            var client=data.GetTableClient(tableName);
            await client.UpsertEntityAsync(employee);
            return employee;
        }
        public static async Task<Details> GetTableData(string tableName,string partitionKey,string rowKey)
        {
            var data=new TableServiceClient(connectionString);
            var client=data.GetTableClient(tableName);
            var tableData=await client.GetEntityAsync<Details>(partitionKey,rowKey);
            return tableData;
        }
        public static async Task<TableClient> GetTable(string tableName)
        {
            var data=new TableServiceClient(connectionString);
            var client=data.GetTableClient(tableName);
            return client;
        }
        public static async Task DeleteTableData(string tableName,string partitionKey,string rowKey)
        {
            var data=new TableServiceClient(connectionString);
            var client=data.GetTableClient(tableName);
            await client.DeleteEntityAsync(partitionKey,rowKey);
            return;
        }
        public static async Task DeleteTable(string tableName)
        {
            var data=new TableServiceClient(connectionString);
            await data.DeleteTableAsync(tableName);
        }
    }
}