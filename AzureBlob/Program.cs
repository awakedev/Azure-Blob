using Microsoft.WindowsAzure.Storage; // Namespace for CloudStorageAccount
using Microsoft.WindowsAzure.Storage.Blob; // Namespace for Blob storage types
using System;
using System.IO;
using System.Reflection.Metadata;

namespace AzureBlob
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            // Retrieve storage account from connection string.
            CloudStorageAccount storageAccount =
              CloudStorageAccount.Parse("DefaultEndpointsProtocol=https;AccountName=privkeystorageaccount;AccountKey=R+/m/OmSHqugA4GkJpvh+kXugxNNmeiSI0fkXf7iGt/utGyPlWU2sTTLpQHIDcTctC81jg7tkdJVtmzYzMYqog==;EndpointSuffix=core.windows.net");
            //CloudStorageAccount.Parse(CloudConfigurationManager.GetSetting("StorageConnectionString"));

            // Create the blob client.
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();

            // Retrieve a reference to a container.
            CloudBlobContainer container = blobClient.GetContainerReference("test-blog");

            BlobContinuationToken continuationToken = null;

            // Create the container if it doesn't already exist.
            container.CreateIfNotExistsAsync();

            container.SetPermissionsAsync(new BlobContainerPermissions
            { PublicAccess = BlobContainerPublicAccessType.Blob });

            // Retrieve reference to a blob named "my blob".
            CloudBlockBlob blockBlob = container.GetBlockBlobReference("blobpng.PNG");


            // Save blob contents to a file.
            using (var fileStream = System.IO.File.OpenWrite(@"C:\Users\DELL\Documents\blobpng.PNG"))
            {
                Console.WriteLine("******************************* Downloading Blob * *********************************************");


                blockBlob.DownloadToStreamAsync(fileStream);
                Console.WriteLine("******************************* Downloaded Blob  * *********************************************");


                Console.ReadLine();
            }


            // uploadBlob(blockBlob);
        }

        private static void uploadBlob(CloudBlockBlob blockBlob)
        {
            //Create or overwrite the "my blob" blob with contents from a local file.
            using (var fileStream = System.IO.File.OpenRead
                (@"C:\Users\DELL\Documents\test.txt"))
            {
                Console.WriteLine
                    ("******************************* Uploading Blob **********************************************");
                blockBlob.UploadFromStreamAsync(fileStream);
                Console.WriteLine("file to be uploaded" + fileStream);
                Console.WriteLine("Uploaded ");

                Console.ReadLine();
            }
        }
    }
}