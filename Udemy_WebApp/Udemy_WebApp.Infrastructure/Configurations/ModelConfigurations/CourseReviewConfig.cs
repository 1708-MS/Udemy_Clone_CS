using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Udemy_WebApp.Domain.Models;

namespace Udemy_WebApp.Infrastructure.Configurations.ModelConfigurations
{
    public class CourseReviewConfig : IEntityTypeConfiguration<CourseReview>
    {
        public void Configure(EntityTypeBuilder<CourseReview> builder)
        {
            builder.HasKey(CourseReview => CourseReview.CourseReviewId);
            builder.Property(CourseReview => CourseReview.CourseRating).IsRequired();
            builder.Property(CourseReview => CourseReview.CourseComment).IsRequired();
            builder.Property(CourseReview => CourseReview.CourseReviewCreatedAt).IsRequired();

            // Configure the one-to-many relationship between CourseReview and Course
            builder.HasOne(CourseReview => CourseReview.Course)
                   .WithMany(Course => Course.CourseReviews)
                   .HasForeignKey(CourseReview => CourseReview.CourseId)
                   .OnDelete(DeleteBehavior.Restrict);

            // Configure the many-to-one relationship between CourseReview and ApplicationUser
            builder.HasOne(CourseReview => CourseReview.ApplicationUser)
                   .WithMany(ApplicationUser => ApplicationUser.CourseReviews)
                   .HasForeignKey(CourseReview => CourseReview.UserId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
