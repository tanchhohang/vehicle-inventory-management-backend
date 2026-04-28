using Microsoft.AspNetCore.Mvc;
using vehicle_management_backend.Models;

namespace vehicle_management_backend.Controllers;

[ApiController]
[Route("api/part-requests")]
public class PartRequestController : ControllerBase
{
    private static readonly List<PartRequest> PartRequests = new();

    [HttpGet]
    public ActionResult<IEnumerable<PartRequest>> GetAll()
    {
        return Ok(PartRequests);
    }

    [HttpPost]
    public ActionResult<PartRequest> Create([FromBody] PartRequest partRequest)
    {
        partRequest.Id = PartRequests.Count == 0 ? 1 : PartRequests.Max(pr => pr.Id) + 1;
        partRequest.RequestedAt = partRequest.RequestedAt == default ? DateTime.UtcNow : partRequest.RequestedAt;

        PartRequests.Add(partRequest);

        return CreatedAtAction(nameof(GetAll), partRequest);
    }
}
