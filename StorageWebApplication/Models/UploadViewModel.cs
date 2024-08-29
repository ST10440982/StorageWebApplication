using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace StorageWebApplication.Models
{
    public class UploadViewModel
    {
        public IFormFile File { get; set; }
    }
 
}

