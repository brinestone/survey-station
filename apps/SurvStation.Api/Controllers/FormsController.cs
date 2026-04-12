using Microsoft.AspNetCore.Mvc;
using SurvStation.Infra.Contexts;

namespace SurvStation.Api.Controllers;

/// <summary>
/// Manages survey forms and related operations.
/// </summary>
/// <remarks>
/// This controller provides endpoints for retrieving and managing survey forms.
/// All operations are prefixed with the <c>api/forms</c> route.
/// </remarks>
[ApiController]
[Route("api/forms")]
public class FormsController(ILogger<FormsController> logger, FormsContext<Guid> formsContext) : ControllerBase
{

    [HttpGet]
    [Produces("application/json")]
    public IDictionary<string, string> Foo()
    {
        return new Dictionary<string, string> { { "foo", "bar" } };
    }
}