using System.Dynamic;
using EdblockClient;
using Grpc.Net.Client;

namespace Edblock.Model;

public class ProjectService
{
    private readonly Project.ProjectClient _projectClient;
    
    public ProjectService()
    {
        using var channel = GrpcChannel.ForAddress("https://localhost:7215");
        _projectClient = new Project.ProjectClient(channel);
    }

    public async Task<List<ResponseSymbol>> GetAsync()
    {
        var projectRequest = new ProjectRequest
        {
            IdProject = "6f1b68f9-b7e0-48f2-9b8c-7e9b68f9b7e2"
        };
        
        var projectReply = await _projectClient.GetAsync(projectRequest);
        
        return projectReply.ResponseSymbols.ToList();
    }
}