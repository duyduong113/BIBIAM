using System.Web;
using Microsoft.Azure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System.IO;
using System.Drawing;
using CMS.Models;
using ServiceStack.OrmLite;
using System.Data;
using System;
using System.Collections.Generic;

namespace CMS.Helpers
{
    public class AzureHelper
    {

        public string UploadFile(string ContainerName, string FileName, string FilePath)
        {
            string link = "";
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(
                                                 CloudConfigurationManager.GetSetting("StorageConnectionString"));
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
            CloudBlobContainer container = blobClient.GetContainerReference(ContainerName.ToLower());
            container.CreateIfNotExists();
            container.SetPermissions(new BlobContainerPermissions { PublicAccess = BlobContainerPublicAccessType.Blob });
            CloudBlockBlob blockBlob = container.GetBlockBlobReference(FileName);
            using (var fileStream = System.IO.File.OpenRead(FilePath))
            {
                blockBlob.UploadFromStream(fileStream);
            }

            foreach (IListBlobItem item in container.ListBlobs(null, false))
            {
                if (item.GetType() == typeof(CloudBlockBlob))
                {
                    CloudBlockBlob blob = (CloudBlockBlob)item;

                    //Console.WriteLine("Block blob of length {0}: {1}", blob.Properties.Length, blob.Uri);
                    if (blob.Name == FileName)
                    {
                        return blob.Uri.AbsoluteUri;
                    }

                }
            }
            return link;
        }

        public string UploadImageToAzure(string FolderName, HttpPostedFileBase file, string UserName)
        {
            string refix = UserName + "_" + DateTime.Now.ToString("yyyyMMddHHmmssfff");
            var fileName = Path.GetFileNameWithoutExtension(file.FileName);
            string extension = Path.GetExtension(file.FileName);
            var img = Image.FromStream(file.InputStream, true, true);
            string FolderPath = System.Web.HttpContext.Current.Server.MapPath("~/Images/" + FolderName + "/");
            var destinationPath = Path.Combine(FolderPath, refix);
            if (!Directory.Exists(FolderPath))
            {
                Directory.CreateDirectory(FolderPath);
            }
            destinationPath +=  extension;
            img.Save(destinationPath);
            return new AzureHelper().UploadFile(FolderName, refix + extension, destinationPath);
        }

    }
}