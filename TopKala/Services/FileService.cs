using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.VisualBasic.FileIO;
using TopKala.Enums;
using TopKala.Services.Interfaces;

namespace TopKala.Services
{
    public class FileService : IFileService
    {

        private readonly IWebHostEnvironment env;
        public static string UploadBaseDir = "upload" + Path.DirectorySeparatorChar;

        public FileService(IWebHostEnvironment env)
        {
            this.env = env;
        }

        public string Upload(IFormFile file, UploadType type = null)
        {
            var fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);
            var filePath = Path.Combine(GetDirectory(type), fileName);
            var filePathRelative = GetRelativePath(filePath);

            using (var stream = File.Create(filePath))
            {
                file.CopyTo(stream);
            }

            return filePathRelative;
        }

        public string UploadCreate(byte[] data, string extention, UploadType type = null)
        {
            var filename = Guid.NewGuid() +  extention;
            var filePath = Path.Combine(GetDirectory(type), filename);
            var filePathRelative = GetRelativePath(filePath);

            using (var stream = File.Create(filePath))
            {
                stream.Write(data);
            }

            return filePathRelative;
        }

        public bool Delete(string filename, UploadType type = null)
        {
            var filePath = Path.Combine(GetDirectory(type), filename);

            try
            {
                File.Delete(filePath);
            }
            catch (IOException)
            {
                return false;   
            }
            return true;
        }

        public string GetPath(string filename, UploadType type = null)
        {
            var filePath = Path.Combine(GetDirectory(type), filename);
            
            if (File.Exists(filePath))
                return GetRelativePath(filePath);

            return null;
        }

        public string GetPath(string filename, FileType type)
            => type.Value + '/' + filename;

        public string GetDirectory(UploadType type)
        {
            var uploadType = type != null ? type.Value + Path.DirectorySeparatorChar : UploadType.Unspecified.Value;
            var uploadPath = Path.Combine(env.WebRootPath, UploadBaseDir, uploadType);

            Directory.CreateDirectory(uploadPath);
            
            return uploadPath;
        }

        public string GetRelativePath(string absolutePath)
            => absolutePath.Replace(env.WebRootPath, "").Replace(Path.DirectorySeparatorChar, '/');
    }
}