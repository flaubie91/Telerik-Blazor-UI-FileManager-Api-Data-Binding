using SharedLibrary.Models;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[Route("api/filebrowser")]
[ApiController]
public class FileBrowserController : ControllerBase
{
    private FileBrowserRepository _repository;

    public FileBrowserController(FileBrowserRepository repository)
    {
        _repository = repository;
    }


    [HttpGet("ReadDirectoryAsync/{*RootPath}")]
    public async Task<ActionResult<List<FlatFileEntry>>> ReadDirectoryAsync(string rootPath)
    {
        var path = rootPath;
        try
        {
            var result = await _repository.ReadDirectoryAsync(rootPath);
            return Ok(result);
        }
        catch (Exception ex)
        {
            // log exception here
            //_logger.LogInfo($"Returned all records using {procedureName}");
            /*Logger.Error(msg);
            app.Logger.LogError(
                ex,
                "Could no process a request on machine {Machine}. TraceId: {TraceId}, 
                Environment.MachineName,
                Activity.Current?.Id);
            https://learn.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.http.results.problem?view=aspnetcore-7.0
            return Results.Problem(
				title: "An error occured while processing your request.",
                statusCode: StatusCodes.Status500InternalServerError,
                extensions: new Dictionary<string, object?>
                {
					{ "traceId", Activity.Current?.Id }
                }
            );
			 */
            var msg = ex.Message;
            return StatusCode(500, ex.Message);
        }
    }
}