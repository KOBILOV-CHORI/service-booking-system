using Domain.Dtos;
using Domain.Filters;
using Infrastructure.Responses;

namespace Infrastructure.Services.ReviewServices;

public interface IReviewService
{
    PaginationResponse<IEnumerable<ReviewReadDto>> GetAllReviews(ReviewFilter filter);
    ReviewReadDto? GetReviewById(int id);
    bool CreateReview(ReviewCreateDto createDto);
    bool UpdateReview(ReviewUpdateDto updateDto);
    bool DeleteReview(int id);
}