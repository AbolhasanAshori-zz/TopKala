using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using TopKala.Enums;

namespace TopKala.Services.Interfaces
{
    public interface IFileService
    {
        string Upload(IFormFile file, UploadType type = null);
        string UploadCreate(byte[] data, string extention, UploadType type = null);
        bool Delete(string filename, UploadType type = null);
        string GetPath(string filename, UploadType type = null);
        string GetPath(string filename, FileType type);
        string GetDirectory(UploadType type);
        string GetRelativePath(string absolutePath);
    }
}