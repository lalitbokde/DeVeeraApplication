using Amazon;
using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Transfer;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Services
{
    public class S3BucketService : IS3BucketService
    {
        private readonly IConfiguration _configuration;
        private static readonly RegionEndpoint bucketRegion = RegionEndpoint.USEast1;
        public S3BucketService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<string> UploadFileAsync(Stream file, string filePath, string fileName)
        {
            string url = "";
            var bucketName = _configuration.GetSection("AWSs3Bucket").GetSection("BucketName").Value.ToString();
            string secretkey = _configuration.GetSection("AWSs3Bucket").GetSection("SecretAccessKey").Value.ToString();
            string accesskey = _configuration.GetSection("AWSs3Bucket").GetSection("AccessKeyID").Value.ToString();

            var s3Client = new AmazonS3Client(accesskey, secretkey, bucketRegion);

            var fileTransferUtility = new TransferUtility(s3Client);
            try
            {
                var fileTransferUtilityRequest = new TransferUtilityUploadRequest
                {
                    BucketName = bucketName,
                    FilePath = filePath,
                    StorageClass = S3StorageClass.StandardInfrequentAccess,
                    PartSize = 524288000, // 500 MB.  
                    Key = fileName,
                    CannedACL = S3CannedACL.PublicRead
                };
                fileTransferUtilityRequest.Metadata.Add("title", fileName);
                await fileTransferUtility.UploadAsync(fileTransferUtilityRequest);
                fileTransferUtility.Dispose();


                url = s3Client.GetPreSignedURL(new GetPreSignedUrlRequest
                {
                    BucketName = bucketName,
                    Key = fileName,
                    Expires = DateTime.UtcNow.AddDays(1),


                });
            }

            catch (AmazonS3Exception amazonS3Exception)
            {
                if (amazonS3Exception.ErrorCode != null &&
                    (amazonS3Exception.ErrorCode.Equals("InvalidAccessKeyId")
                    ||
                    amazonS3Exception.ErrorCode.Equals("InvalidSecurity")))
                {
                    //ViewBag.Message = "Check the provided AWS Credentials.";
                }
                else
                {
                    var message = $"Error occurred: {amazonS3Exception.Message}";
                }
            }
            return url;
        }


        public async Task<string> GetPreSignedURL(string key)
        {
            string url = "";

            if (!string.IsNullOrWhiteSpace(key))
            {
                var bucketName = _configuration.GetSection("AWSs3Bucket").GetSection("BucketName").Value.ToString();

                string secretkey = _configuration.GetSection("AWSs3Bucket").GetSection("SecretAccessKey").Value.ToString();

                string accesskey = _configuration.GetSection("AWSs3Bucket").GetSection("AccessKeyID").Value.ToString();

                var s3Client = new AmazonS3Client(accesskey, secretkey, bucketRegion);

                url = s3Client.GetPreSignedURL(new GetPreSignedUrlRequest
                {
                    BucketName = bucketName,
                    Key = key,
                    Expires = DateTime.UtcNow.AddDays(1),
                 });

                return url;
            }
            return url;
           
        }

    }
}
