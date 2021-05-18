using System.ComponentModel.DataAnnotations;

using Microsoft.AspNetCore.Http;

namespace Zeebe.Worker.Models.Command
{
    public record DeployProcessRequest(
        [Required] IFormFile FileContent,
        [Required] string FileName,
        string FileType);
}