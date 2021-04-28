using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Services
{
    public interface IVideoUploadService
    {
        Task<string> UploadFileAsync(Stream file, string fileName);
    }
}
