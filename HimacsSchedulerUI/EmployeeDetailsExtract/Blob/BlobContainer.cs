using Azure.Storage.Blobs;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace EmployeeDetailsExtract.Blob
{
    public class BlobContainer
    {
        static BlobServiceClient blobServiceClient;
        static BlobContainerClient containerClient;
        static string connectionString;
        public static void AssignBlobObjects()
        {
            try
            {
                BlobServiceClient blobServiceClient = new BlobServiceClient("DefaultEndpointsProtocol=https;AccountName=sqlvagzclugwtw6xpq;AccountKey=ySczh3Zd+sIqxeDzck2p73BBX3vfbY4NUletNwl7gxGYTXlIsxre1RLAcGqhopLFwkWJr0r9PsLwVoVPDS6s7Q==;EndpointSuffix=core.windows.net");
                containerClient = blobServiceClient.GetBlobContainerClient("employee");
            }
            catch(Exception e)
            {
                string s = e.StackTrace;
            }
        }

        public static async System.Threading.Tasks.Task UploadBlocbAsync(string contents)
        {
            try
            {
                string fileName = "employee.csv";
                string trigger = "trigger.trg";
                string localFilePath = Path.Combine(Path.GetTempPath(), fileName);
                string localTriggerPath = Path.Combine(Path.GetTempPath(), trigger);

                // Write text to the file
                await File.WriteAllTextAsync(localFilePath, contents);
                await File.WriteAllTextAsync(localTriggerPath, "");

                // Get a reference to a blob
                BlobClient blobClient = containerClient.GetBlobClient("incoming\\"+fileName);
                Console.WriteLine("Uploading to Blob storage as blob:\n\t {0}\n", blobClient.Uri);
                // Open the file and upload its data
                using FileStream uploadFileStream = File.OpenRead(localFilePath);
                await blobClient.UploadAsync(uploadFileStream, true);
                uploadFileStream.Close();

                // Get a reference to a blob
                blobClient = containerClient.GetBlobClient("incoming\\" + trigger);
                Console.WriteLine("Uploading to Blob storage as blob:\n\t {0}\n", blobClient.Uri);
                // Open the file and upload its data
                using FileStream uploadFileStreamTrigger = File.OpenRead(localTriggerPath);
                await blobClient.UploadAsync(uploadFileStreamTrigger, true);
                uploadFileStreamTrigger.Close();
            }
            catch(Exception e)
            {

                string a = e.StackTrace;
            }
        }
    }
}
