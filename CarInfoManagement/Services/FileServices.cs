﻿namespace CarInfoManagement.Services
{
    public class FileServices: IFileServices
    {
        private readonly IWebHostEnvironment environment;
        public FileServices(IWebHostEnvironment env)
        {
            this.environment = env;
        }

        public Tuple<int, string> SaveImage(IFormFile imageFile)
        {
            try
            {
                var wwwPath = this.environment.WebRootPath;
                var path = Path.Combine(wwwPath, "Uploads");
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                // Check the allowed extenstions
                if(imageFile!=null)
                {
                    var ext = Path.GetExtension(imageFile.FileName);
                    var allowedExtensions = new string[] { ".jpg", ".png", ".jpeg" };
                    if (!allowedExtensions.Contains(ext))
                    {
                        string msg = string.Format("Only {0} extensions are allowed", string.Join(",", allowedExtensions));
                        return new Tuple<int, string>(0, msg);
                    }
                    string uniqueString = Guid.NewGuid().ToString();
                    // we are trying to create a unique filename here
                    var newFileName = uniqueString + ext;
                    var fileWithPath = Path.Combine(path, newFileName);
                    var stream = new FileStream(fileWithPath, FileMode.Create);
                    imageFile.CopyTo(stream);
                    stream.Close();
                    return new Tuple<int, string>(1, newFileName);
                }
                return new Tuple<int, string>(1, "");
            }
            catch (Exception ex)
            {
                return new Tuple<int, string>(0, "Error has occured");
            }
        }

        public bool DeleteImage(string imageFileName)
        {
            try
            {
                var wwwPath = this.environment.WebRootPath;
                var path = Path.Combine(wwwPath, "Uploads\\", imageFileName);
                if (System.IO.File.Exists(path))
                {
                    System.IO.File.Delete(path);
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
