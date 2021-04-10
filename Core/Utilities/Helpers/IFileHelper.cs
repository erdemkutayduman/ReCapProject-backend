using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Helpers
{
    public interface IFileHelper
    {
        string Add(IFormFile file, string path);
        string Update(string pathToUpdate, IFormFile file, string path);
        void Delete(string path);
    }
}
