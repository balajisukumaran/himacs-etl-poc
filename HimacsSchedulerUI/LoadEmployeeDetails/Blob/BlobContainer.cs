using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using LoadEmployeeDetails.CSVProfiler;

namespace LoadEmployeeDetails.Blob
{
    public class BlobContainer
    {
        public static async System.Threading.Tasks.Task<StreamReader> download_FromBlobAsync(string filetoDownload, string azure_ContainerName)
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse("DefaultEndpointsProtocol=https;AccountName=sqlvagzclugwtw6xpq;AccountKey=ySczh3Zd+sIqxeDzck2p73BBX3vfbY4NUletNwl7gxGYTXlIsxre1RLAcGqhopLFwkWJr0r9PsLwVoVPDS6s7Q==;EndpointSuffix=core.windows.net");
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
            CloudBlobContainer container = blobClient.GetContainerReference(azure_ContainerName);
            CloudBlockBlob blockBlob = container.GetBlockBlobReference(filetoDownload);

            // provide the file download location below            
            string text = await blockBlob.DownloadTextAsync();
            string localFilePath = Path.Combine(Path.GetTempPath(), "test.csv");
            await File.WriteAllTextAsync(localFilePath, text);
            StreamReader sr = new StreamReader(localFilePath);
            return sr;
        }
    }
}
