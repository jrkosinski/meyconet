using Azure.Storage.Files.Shares;
using Azure.Storage.Files.Shares.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Threading.Tasks;

namespace WSGUtilitieslib
{
    public static class AzureFileStore
    {
        private static string connectionString;

        static AzureFileStore()
        {
            connectionString = ConfigurationManager.AppSettings["AzureStorageConnectionString"];

            try
            {
                var serviceClient = new ShareServiceClient(connectionString);
                Azure.Storage.Files.Shares.ShareClient shareClient = serviceClient.GetShareClient("meycostore-fileshare");
            }
            catch (Exception e)
            {
                string s = e.ToString();
            }
        }

        public static string[] ListFilesInDirectory(string shareName, string directoryName)
        {
            ShareDirectoryClient dirClient = GetDirectoryClient(shareName, directoryName);
            List<string> fileNames = new List<string>();

            if (dirClient.Exists())
            {
                var result = dirClient.GetFilesAndDirectories();
                foreach (var item in result)
                {
                    fileNames.Add(item.Name);
                }
            }

            return fileNames.ToArray();
        }

        public static async Task<byte[]> ReadFile(string shareName, string directoryName, string fileName)
        {
            ShareDirectoryClient dirClient = GetDirectoryClient(shareName, directoryName);
            ShareFileClient fileClient = dirClient.GetFileClient(fileName);

            // Download the file
            if (fileClient.Exists().Value)
            {
                ShareFileDownloadInfo download = await fileClient.DownloadAsync();

                // Convert the stream to a byte array
                using (MemoryStream stream = new MemoryStream())
                {
                    await download.Content.CopyToAsync(stream);
                    return stream.ToArray();
                }
            }

            return null;
        }

        public static async Task WriteFile(string shareName, string directoryName, string localPath, string fileName)
        {
            try
            {
                ShareDirectoryClient dirClient = GetDirectoryClient(shareName, directoryName);

                // Get a reference to the file client
                Azure.Storage.Files.Shares.ShareFileClient fileClient = dirClient.GetFileClient(fileName);

                // Upload the file
                using (System.IO.FileStream stream = System.IO.File.OpenRead(Path.Combine(new string[] { localPath, fileName })))
                {
                    try
                    {
                        await fileClient.CreateAsync(stream.Length);
                        await fileClient.UploadAsync(stream);
                    }
                    catch (Exception e)
                    {
                        System.Diagnostics.Debug.WriteLine(e.ToString());
                    }
                }
            }
            catch (Exception e)
            {
                string s = e.ToString();
            }
        }

        private static ShareDirectoryClient GetDirectoryClient(string shareName, string directoryName)
        {
            var serviceClient = new ShareServiceClient(connectionString);
            Azure.Storage.Files.Shares.ShareClient shareClient = serviceClient.GetShareClient(shareName);

            ShareDirectoryClient dirClient = null;
            if (String.IsNullOrEmpty(directoryName))
                dirClient = shareClient.GetRootDirectoryClient();
            else
                dirClient = shareClient.GetDirectoryClient(directoryName);

            return dirClient;
        }
    }
}
