using Domain.Dtos;
using Domain.Filters;
using Infrastructure.Responses;
using Infrastructure.Services.ReviewServices;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("/api/reviews")]
public sealed class ReviewController(IReviewService reviewService) : ControllerBase
{
    [HttpGet]
    public IActionResult GetReviews([FromQuery] ReviewFilter filter)
        => Ok(ApiResponse<PaginationResponse<IEnumerable<ReviewReadDto>>>.Success(null,
            reviewService.GetAllReviews(filter)));

    [HttpGet("{id:int}")]
    public IActionResult GetReviewById(int id)
    {
        ReviewReadDto? res = reviewService.GetReviewById(id);
        return res != null
            ? Ok(ApiResponse<ReviewReadDto?>.Success(null, res))
            : NotFound(ApiResponse<ReviewReadDto?>.Fail(null, null));
    }

    [HttpPost]
    public IActionResult CreateReview([FromBody] ReviewCreateDto reviewCreateDto)
    {
        ReviewCreateDto info = reviewCreateDto;
        bool res = reviewService.CreateReview(info);
        return res
            ? Ok(ApiResponse<bool>.Success(null, res))
            : BadRequest(ApiResponse<bool>.Fail(null, res));
    }

    [HttpPut]
    public IActionResult UpdateReview(ReviewUpdateDto info)
    {
        bool res = reviewService.UpdateReview(info);
        return res
            ? Ok(ApiResponse<bool>.Success(null, res))
            : NotFound(ApiResponse<bool>.Fail(null, res));
    }

    [HttpDelete("{id:int}")]
    public IActionResult DeleteReview(int id)
    {
        bool res = reviewService.DeleteReview(id);
        return res
            ? Ok(ApiResponse<bool>.Success(null, res))
            : NotFound(ApiResponse<bool>.Fail(null, res));
    }
}