using Domain.Dtos;
using Domain.Entities;
using Domain.Filters;
using Infrastructure.Extensions.MapperExtensions;
using Infrastructure.Responses;

namespace Infrastructure.Services.ReviewServices;

public class ReviewService(DataContext context) : IReviewService
{
    public PaginationResponse<IEnumerable<ReviewReadDto>> GetAllReviews(ReviewFilter filter)
    {
        IQueryable<Review> reviews = context.Reviews;
        if (filter.ServiceId != null)
            reviews = reviews.Where(x => x.ServiceId == filter.ServiceId);
        if (filter.ClientId != null)
            reviews = reviews.Where(x => x.ClientId == filter.ClientId);
        if (filter.MinRating != null)
            reviews = reviews.Where(x => x.Rating >= filter.MinRating);
        if (filter.MaxRating != null)
            reviews = reviews.Where(x => x.Rating <= filter.MaxRating);
        if (!string.IsNullOrEmpty(filter.Comment))
            reviews = reviews.Where(x => x.Comment.ToLower().Contains(filter.Comment.ToLower()));

        int totalRecords = reviews.Count();
        var result = reviews.Skip((filter.PageNumber - 1) * filter.PageSize)
                            .Take(filter.PageSize)
                            .Where(x => !x.IsDeleted)
                            .Select(x => x.ReviewToReadDto())
                            .ToList();

        return PaginationResponse<IEnumerable<ReviewReadDto>>.Create(filter.PageNumber, filter.PageSize, totalRecords, result);
    }

    public ReviewReadDto? GetReviewById(int id)
    {
        return context.Reviews.Where(x => x.IsDeleted == false && x.Id == id)
                              .Select(x => x.ReviewToReadDto())
                              .FirstOrDefault();
    }

    public bool CreateReview(ReviewCreateDto createDto)
    {
        context.Reviews.Add(createDto.CreateDtoToReview());
        context.SaveChanges();
        return true;
    }

    public bool UpdateReview(ReviewUpdateDto updateDto)
    {
        var existingReview = context.Reviews.FirstOrDefault(x => !x.IsDeleted && x.Id == updateDto.Id);
        if (existingReview == null) return false;

        existingReview.UpdateDtoToReview(updateDto);
        context.SaveChanges();
        return true;
    }

    public bool DeleteReview(int id)
    {
        var existingReview = context.Reviews.FirstOrDefault(x => x.Id == id && !x.IsDeleted);
        if (existingReview == null) return false;

        existingReview.IsDeleted = true;
        existingReview.DeletedAt = DateTime.UtcNow;
        context.SaveChanges();
        return true;
    }
}
