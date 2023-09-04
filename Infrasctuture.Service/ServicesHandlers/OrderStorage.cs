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

        public async Task<string> UploadFile(IFormFile file)
        {
            try
            {
                string containerName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                //containerName = containerName.Replace(" ", "").Replace(".", "").ToLower();

                using (var client = new AmazonS3Client(_blobSettings.IDAccessKey, _blobSettings.AccessKey, Amazon.RegionEndpoint.SAEast1))
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
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<string> UploadMedia(IFormFile file, string clientId)
        {
            try
            {
                string containerName = string.Concat(clientId, Guid.NewGuid().ToString(), file.FileName);

                using (var client = new AmazonS3Client(_blobSettings.IDAccessKey, _blobSettings.AccessKey, Amazon.RegionEndpoint.SAEast1))
                {

                    using (var newMemoryStream = new MemoryStream())
                    {
                        file.CopyTo(newMemoryStream);

                        var uploadRequest = new TransferUtilityUploadRequest
                        {
                            InputStream = newMemoryStream,
                            Key = containerName,
                            BucketName = _blobSettings.BucketImageName,
                            CannedACL = S3CannedACL.NoACL
                        };

                        var fileTransferUtility = new TransferUtility(client);
                        await fileTransferUtility.UploadAsync(uploadRequest);
                    }
                }
                return await Task.FromResult(containerName);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
