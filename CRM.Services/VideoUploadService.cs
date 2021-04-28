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
    public class VideoUploadService : IVideoUploadService
    {
        private readonly IConfiguration _configuration;
        private readonly IVideoMasterService _videoMasterService;
        private static readonly RegionEndpoint bucketRegion = RegionEndpoint.USEast1;
        public VideoUploadService(IConfiguration configuration,
                                  IVideoMasterService videoMasterService)
        {
            _configuration = configuration;
            _videoMasterService = videoMasterService;
        }

        public async Task<string> UploadFileAsync(Stream file, string filePath)
        {
            string url = "";
            string responseBody = "";
            var bucketName = _configuration.GetSection("AWSs3Bucket").GetSection("BucketName").Value.ToString();
            string secretkey = "rrndboC0WNfwPM+q2yHJ2BNQp11046ukIM62t6IW";
            string accesskey = "AKIA57UNQHWZ7C5BG34B";


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
                        Key = "demo",
                        CannedACL = S3CannedACL.PublicRead
                    };
                    fileTransferUtilityRequest.Metadata.Add("title", "demo");
                    fileTransferUtility.Upload(fileTransferUtilityRequest);
                    fileTransferUtility.Dispose();


                url = s3Client.GetPreSignedURL(new GetPreSignedUrlRequest
                {
                    BucketName = bucketName,
                    Key = "demo",
                    Expires = DateTime.UtcNow.AddDays(1),
                    

                 });



                //using (GetObjectResponse response = await s3Client.GetObjectAsync(request))
                //using (Stream responseStream = response.ResponseStream)
                //using (StreamReader reader = new StreamReader(responseStream))
                //{
                //    string title = response.Metadata["x-amz-meta-title"]; // Assume you have "title" as medata added to the object.
                //    string contentType = response.Headers["Content-Type"];
                //    Console.WriteLine("Object metadata, Title: {0}", title);
                //    Console.WriteLine("Content type: {0}", contentType);

                //    responseBody = reader.ReadToEnd(); // Now you process the response body.
                //}
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
                    //ViewBag.Message = "Error occurred: " + amazonS3Exception.Message;
                }
            }
            //return responseBody;
            return url;
        }

    }
}
