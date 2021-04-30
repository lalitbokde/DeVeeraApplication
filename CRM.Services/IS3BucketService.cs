using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Services
{
    public interface IS3BucketService
    {
        Task<string> UploadFileAsync(Stream file,string filePath, string fileName);
        Task<string> GetPreSignedURL(string key);
        Task DeleteFile(string key);
    }
}
