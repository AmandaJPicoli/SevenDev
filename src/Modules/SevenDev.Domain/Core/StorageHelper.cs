using SevenDev.Domain.Core.Interfaces;
using SevenDev.Domain.Entities.ValueObject;
using Microsoft.Extensions.Configuration;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Amazon.S3;
using Amazon;
using Microsoft.AspNetCore.Http;

namespace SevenDev.Domain.Core
{
    public class StorageHelper : IStorageHelper
    {
        private string _AwsAccessKeyId;
        private string _AwsSecretAccesKey;
        string AWS_bucketName = "amandapicoli";
        string AWS_defaultFolder = "sevenDev";

        public StorageHelper(IConfiguration configuration)
        {
            _AwsAccessKeyId = configuration.GetSection("aws_access_key_id").Value;
            _AwsSecretAccesKey = configuration.GetSection("aws_secret_access_key").Value;
        }

        public async Task<ImageBlob> Upload(IFormFile stream, string nameFile)
        {
            var newNameFile = $"{Guid.NewGuid()}.{nameFile.Split('.')[1]}";
            var url = await UploadFileToAWSAsync(stream);
            
            return new ImageBlob()
            {
                Url = url,
            };
        }

        public bool IsImage(string nameFile)
        {
            string[] formats = new string[] { ".jpg", ".png", ".gif", ".jpeg" };
            return formats.Any(item => nameFile.EndsWith(item, StringComparison.OrdinalIgnoreCase));
        }

      
        protected async Task<string> UploadFileToAWSAsync(IFormFile myfile, string subFolder = "")
        {
            var result = "";
            try
            {
                var s3Client = new AmazonS3Client(_AwsAccessKeyId, _AwsSecretAccesKey, RegionEndpoint.USEast1);
                var bucketName = AWS_bucketName;
                var keyName = AWS_defaultFolder;
                if (!string.IsNullOrEmpty(subFolder))
                    keyName = keyName + "/" + subFolder.Trim();
                keyName = keyName + "/" + myfile.FileName;

                var fs = myfile.OpenReadStream();
                var request = new Amazon.S3.Model.PutObjectRequest
                {
                    BucketName = bucketName,
                    Key = keyName,
                    InputStream = fs,
                    ContentType = myfile.ContentType,
                    CannedACL = S3CannedACL.PublicRead
                };
                await s3Client.PutObjectAsync(request);

                result = string.Format("http://{0}.s3.amazonaws.com/{1}", bucketName, keyName);
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }
            return result;
        }
    }
}
