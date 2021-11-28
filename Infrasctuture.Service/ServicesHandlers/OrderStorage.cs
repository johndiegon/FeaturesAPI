using Amazon.S3;
using Amazon.S3.Transfer;
using Infrasctuture.Service.Interfaces;
using Infrasctuture.Service.Interfaces.settings;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Infrasctuture.Service.ServicesHandlers
{
    public class OrderStorage : IStorage
    {
        public IBlobSettings _blobSettings;

        public OrderStorage(IBlobSettings blobSettings)
        {
            _blobSettings = blobSettings;
  
        }
        //public async Task<string> UploadFileToBloc(IFormFile file)
        //{
        //    // Create a BlobServiceClient object which will be used to create a container client
        //    BlobServiceClient blobServiceClient = new BlobServiceClient(_blobSettings.ConnectionString);

        //    //Create a unique name for the container
        //    string containerName = Guid.NewGuid().ToString() + ".csv";
        //    containerName = containerName.Replace(" ", "").Replace(".", "").ToLower();

        //    // Create the container and return a container client object
        //    BlobContainerClient containerClient = await blobServiceClient.CreateBlobContainerAsync(containerName);
        //    BlobClient blobClient = containerClient.GetBlobClient(containerName);

        //    await blobClient.UploadAsync(file.OpenReadStream());

        //    return await Task.FromResult(containerName);
        //}

        public async Task<string> UploadFile(IFormFile file)
        {
            string containerName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName); 
            //containerName = containerName.Replace(" ", "").Replace(".", "").ToLower();

            using (var client = new AmazonS3Client(_blobSettings.IDAccessKey, _blobSettings.AccessKey ,Amazon.RegionEndpoint.SAEast1))
            {

                using (var newMemoryStream = new MemoryStream())
                {
                    file.CopyTo(newMemoryStream);

                    var uploadRequest = new TransferUtilityUploadRequest
                    {
                        InputStream = newMemoryStream,
                        Key = containerName,
                        BucketName = _blobSettings.BucketName,
                        CannedACL = S3CannedACL.PublicRead
                    };

                    var fileTransferUtility = new TransferUtility(client);
                    await fileTransferUtility.UploadAsync(uploadRequest);
                }
            }
            return await Task.FromResult(containerName);
        }
    }
}
