using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Queue;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityAPI.Helpers
{
    public class StorageAccountHelper
    {
        private CloudStorageAccount storageAccount;
        private CloudQueueClient queueClient;

        private string storageConnectionStrings;

        public string StorageConnectionStrings
        {
            get { return this.storageConnectionStrings; }
            set
            {

                this.storageConnectionStrings = value;
                this.storageAccount = CloudStorageAccount.Parse(this.storageConnectionStrings);
            }
        }


        // methods

        public async Task SendMessagesAsync(string messageText, string queueName)
        {
            queueClient = storageAccount.CreateCloudQueueClient();
            var queue = queueClient.GetQueueReference(queueName);
            await queue.CreateIfNotExistsAsync();

            CloudQueueMessage queueMessage = new CloudQueueMessage(messageText);

            await queue.AddMessageAsync(queueMessage);
        }


    }
}
