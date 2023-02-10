

using Azure.Storage.Queues;
using Azure.Storage.Queues.Models;

namespace StorageAccount.Repository
{
    public class Queue
    {
        static string connectionString="DefaultEndpointsProtocol=https;AccountName=azurestorage20;AccountKey=i8aXLBGaYBH7TLVpNMxc72zm3jzGcDdnk2XT7x1xG6ZGUG44J8f0GWoCnON2puaNvtKLksJIJMHD+AStDasE/g==;EndpointSuffix=core.windows.net";
        public static async Task<bool> CreateQueue(string queueName)
        {
            if(string.IsNullOrEmpty(queueName))
            {
                throw new ArgumentNullException("enter queue name");
            }
            try
            {
                QueueClient container=new QueueClient(connectionString,queueName);
                await container.CreateIfNotExistsAsync();
                if(container.Exists())
                {
                    Console.WriteLine("Queue created:"+container.Name);
                    return true;
                }
                else
                {
                    Console.WriteLine("Check azure connection and try again");
                    return false;
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        public static async Task InsertMessage(string queueName,string msg)
        {
            if(string.IsNullOrEmpty(queueName))
            {
                throw new ArgumentNullException("enter queue nqme");
            }
            QueueClient container=new QueueClient(connectionString,queueName);
            await container.CreateIfNotExistsAsync();
            if(container.Exists())
            {
                var data=container.SendMessage(msg);
                Console.WriteLine("Message sent successfully");
            }
            else
            {
                Console.WriteLine("Queue message not sent");
            }
        }
        public static async Task<PeekedMessage[]> PeekMessage(string queueName)
        {
            QueueClient container=new QueueClient(connectionString,queueName);
            PeekedMessage[] msg=null;
            if(container.Exists())
            {
                msg=container.PeekMessages(2);
            }
            return msg;
        }
        public static async Task UpdateMessage(string queueName,string data)
        {
            QueueClient container=new QueueClient(connectionString,queueName);
            if(container.Exists())
            {
                QueueMessage[] msg=container.ReceiveMessages();
                container.UpdateMessage(msg[0].MessageId,msg[0].PopReceipt,data,TimeSpan.FromSeconds(100));
            }
        }
        public static async Task DequeueMessage(string queueName)
        {
            QueueClient container=new QueueClient(connectionString,queueName);
            if(container.Exists())
            {
                QueueMessage[] msg=container.ReceiveMessages();
                System.Console.WriteLine("Dequeue message"+msg[0].Body);
                container.DeleteMessage(msg[0].MessageId,msg[0].PopReceipt);
            }
        }
        public static async Task DeleteQueue(string queueName)
        {
            QueueClient container=new QueueClient(connectionString,queueName);
            if(container.Exists())
            {
                await container.DeleteAsync();
            }
        }
    }
}