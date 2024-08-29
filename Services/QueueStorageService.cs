using Azure.Storage.Queues;
using System.Threading.Tasks;

public class QueueStorageService
{
    private readonly QueueClient _queueClient;

    public QueueStorageService(string connectionString, string queueName)
    {
        _queueClient = new QueueClient(connectionString, queueName);
        _queueClient.CreateIfNotExists();
    }

    public async Task SendMessageAsync(string message)
    {
        await _queueClient.SendMessageAsync(message);
    }

    public async Task<string> ReceiveMessageAsync()
    {
        var message = await _queueClient.ReceiveMessageAsync();
        if (message.Value != null)
        {
            await _queueClient.DeleteMessageAsync(message.Value.MessageId, message.Value.PopReceipt);
            return message.Value.MessageText;
        }
        return null;
    }
}
