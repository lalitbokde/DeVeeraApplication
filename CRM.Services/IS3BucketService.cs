using System.IO;
using System.Threading.Tasks;

namespace CRM.Services
{
    public interface IS3BucketService
    {
        Task<string> UploadFileAsync(Stream file,string filePath, string fileName);
        string GetPreSignedURL(string key);
        Task DeleteFile(string key);
    }
}
