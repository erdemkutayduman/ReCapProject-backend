using Core.Utilities.Results;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Core.Utilities.Helpers
{
        public class FileHelper : IFileHelper
        {


            public static string DefaultImagePath = Directory.GetCurrentDirectory() + @"\wwwroot\Uploads\34c3aade-ecae-4c3d-9708-8fc1ad2a0711_2_28_2021.jpg";
            public static string UploadImagePath = Directory.GetCurrentDirectory() + @"\wwwroot\Uploads\";


            public static string CreatePath(IFormFile file)
            {

                FileInfo fileInfo = new FileInfo(file.FileName);

                string path = Path.Combine(UploadImagePath);
                string fileExtension = fileInfo.Extension;
                string uniqueFilename = Guid.NewGuid().ToString() + fileExtension;
                string result = $@"{path}\{uniqueFilename}";

                return result;

            }


            public static string AddFile(IFormFile file)
            {

                string result;

                try
                {
                    if (file == null)
                    {
                        result = DefaultImagePath;

                        return result;
                    }
                    else
                    {
                        result = CreatePath(file);

                        var sourcePath = Path.GetTempFileName();

                        using (var stream = new FileStream(sourcePath, FileMode.Create))
                        {
                            file.CopyTo(stream);
                        }

                        File.Move(sourcePath, result);

                        return result;
                    }
                }
                catch (Exception exception)
                {
                    return exception.Message;
                }

            }


            public static string DeleteFile(string imagePath)
            {

                try
                {
                    File.Delete(imagePath);

                    return "Deleted";
                }
                catch (Exception exception)
                {
                    return exception.Message;
                }

            }


            public static string UpdateFile(IFormFile file, string oldImagePath)
            {

                string result;

                try
                {
                    if (file == null)
                    {
                        File.Delete(oldImagePath);

                        result = DefaultImagePath;

                        return result;
                    }
                    else
                    {
                        result = CreatePath(file);

                        var sourcePath = Path.GetTempFileName();

                        using (var stream = new FileStream(sourcePath, FileMode.Create))
                        {
                            file.CopyTo(stream);
                        }

                        File.Move(sourcePath, result);

                        File.Delete(oldImagePath);

                        return result;
                    }
                }
                catch (Exception exception)
                {
                    return exception.Message;
                }

            }

        public string Add(IFormFile file, string path)
        {
            var sourcepath = Path.GetTempFileName();

            if (file.Length > 0)
                using (var stream = new FileStream(sourcepath, FileMode.Create))
                {
                    file.CopyTo(stream);
                }

            File.Move(sourcepath, path);
            return path;
        }

        public string Update(string pathToUpdate, IFormFile file, string path)
        {
            if (path.Length > 0)
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    file.CopyTo(stream);
                }

            File.Delete(pathToUpdate);
            return path;
        }

        public void Delete(string path)
        {
            if (File.Exists(path))
                File.Delete(path);
        }
    }
}
