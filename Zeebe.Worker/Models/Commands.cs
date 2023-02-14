namespace Zeebe.Worker.Models;

public static class Commands
{
    public const string Topology = "topology";
    public const string DeployProcess = "deploy-process";
    public const string CreateInstance = "create-instance";
    public const string CancelInstance = "cancel-instance";
    public const string SetVariables = "set-variables";
    public const string ResolveIncident = "resolve-incident";
    public const string PublishMessage = "publish-message";
    public const string ActivateJobs = "activate-jobs";
    public const string CompleteJob = "complete-job";
    public const string FailJob = "fail-job";
    public const string UpdateJobRetries = "update-job-retries";
    public const string ThrowError = "throw-error";
}