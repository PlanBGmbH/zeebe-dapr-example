using System.ComponentModel.DataAnnotations;

using Microsoft.AspNetCore.Http;

namespace Zeebe.Worker.Models.Command
{
    public record DeployWorkflowRequest(
        [Required] IFormFile FileContent,
        [Required] string FileName,
        string FileType);
}