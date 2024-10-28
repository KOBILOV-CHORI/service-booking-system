using Domain.Dtos;
using Domain.Entities;

namespace Infrastructure.Extensions.MapperExtensions;

public static class ReviewMapperExtension
{
    public static ReviewReadDto ReviewToReadDto(this Review review)
    {
        return new ReviewReadDto()
        {
            Id = review.Id,
            Comment = review.Comment,
            Rating = review.Rating,
            ServiceId = review.ServiceId,
            ClientId = review.ClientId
        };
    }

    public static Review UpdateDtoToReview(this Review review, ReviewUpdateDto updateDto)
    {
        review.Comment = updateDto.Comment;
        review.Rating = updateDto.Rating;
        review.ServiceId = updateDto.ServiceId;
        review.ClientId = updateDto.ClientId;
        review.Version += 1;
        review.UpdatedAt = DateTime.UtcNow;
        return review;
    }

    public static Review CreateDtoToReview(this ReviewCreateDto createDto)
    {
        return new Review()
        {
            Comment = createDto.Comment,
            Rating = createDto.Rating,
            ServiceId = createDto.ServiceId,
            ClientId = createDto.ClientId,
            CreatedAt = DateTime.UtcNow
        };
    }

    public static Review DeleteDtoToReview(this Review review)
    {
        review.IsDeleted = true;
        review.DeletedAt = DateTime.UtcNow;
        review.Version += 1;
        review.UpdatedAt = DateTime.UtcNow;
        return review;
    }
}