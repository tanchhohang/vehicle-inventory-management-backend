using Microsoft.AspNetCore.Mvc;
using vehicle_management_backend.Models;

namespace vehicle_management_backend.Controllers;

[ApiController]
[Route("api/reviews")]
public class ReviewController : ControllerBase
{
    private static readonly List<Review> Reviews = new();

    [HttpGet]
    public ActionResult<IEnumerable<Review>> GetAll()
    {
        return Ok(Reviews);
    }

    [HttpPost]
    public ActionResult<Review> Create([FromBody] Review review)
    {
        if (review.Rating is < 1 or > 5)
        {
            return BadRequest("Rating must be between 1 and 5.");
        }

        review.Id = Reviews.Count == 0 ? 1 : Reviews.Max(r => r.Id) + 1;
        review.CreatedAt = review.CreatedAt == default ? DateTime.UtcNow : review.CreatedAt;

        Reviews.Add(review);

        return CreatedAtAction(nameof(GetAll), review);
    }
}
