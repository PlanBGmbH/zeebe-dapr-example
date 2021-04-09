using System.ComponentModel.DataAnnotations;

using Microsoft.AspNetCore.Http;

namespace Zeebe.Worker.Models.Workflow
{
    public record DeployRequest(
        [Required] IFormFile FileContent,
        [Required] string FileName,
        string FileType);
}