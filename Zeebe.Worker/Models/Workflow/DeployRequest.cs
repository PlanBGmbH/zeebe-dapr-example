using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace Zeebe.Worker.Models
{
    public class DeployRequest
    {
        [Required]
        public IFormFile FileContent { get; set; }

        [Required]
        public string FileName { get; set; }

        public string FileType { get; set; }
    }
}
