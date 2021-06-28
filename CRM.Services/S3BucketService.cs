using Amazon;
using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Transfer;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Threading.Tasks;

namespace CRM.Services
{
    public class S3BucketService : IS3BucketService
    {
        private readonly IConfiguration _configuration;
        private readonly string _bucketName;
        private readonly string _secretkey;
        private readonly string _accesskey;
        private static readonly RegionEndpoint bucketRegion = RegionEndpoint.USEast1;
        public S3BucketService(IConfiguration configuration)
        {
            _configuration = configuration;
            _bucketName = _configuration.GetSection("AWSs3Bucket").GetSection("BucketName").Value.ToString();
            _secretkey = _configuration.GetSection("AWSs3Bucket").GetSection("SecretAccessKey").Value.ToString();
            _accesskey = _configuration.GetSection("AWSs3Bucket").GetSection("AccessKeyID").Value.ToString();
        }

        public async Task<string> UploadFileAsync(Stream file, string filePath, string fileName)
        {
            string url = "";

            var s3Client = new AmazonS3Client(_accesskey, _secretkey, bucketRegion);

            var fileTransferUtility = new TransferUtility(s3Client);
            try
            {
                var fileTransferUtilityRequest = new TransferUtilityUploadRequest
                {
                    BucketName = _bucketName,
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
                    BucketName = _bucketName,
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


        public string GetPreSignedURL(string key)
        {
            string url = "";

            if (!string.IsNullOrWhiteSpace(key))
            {

                var s3Client = new AmazonS3Client(_accesskey, _secretkey, bucketRegion);

                url = s3Client.GetPreSignedURL(new GetPreSignedUrlRequest
                {
                    BucketName = _bucketName,
                    Key = key,
                    Expires = DateTime.UtcNow.AddDays(1),
                 });

                return url;
            }
            return url;
           
        }


        public async Task DeleteFile(string key)
        {
            if (!string.IsNullOrWhiteSpace(key))
            {
                var s3Client = new AmazonS3Client(_accesskey, _secretkey, bucketRegion);

                var request = new DeleteObjectRequest
                {
                    BucketName = _bucketName,
                    Key = key
                };

                await s3Client.DeleteObjectAsync(request);
            }
        }

    }
}
