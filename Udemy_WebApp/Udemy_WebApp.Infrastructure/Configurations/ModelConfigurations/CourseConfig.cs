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
    public class CourseConfig : IEntityTypeConfiguration<Course>
    {
        public void Configure(EntityTypeBuilder<Course> builder)
        {
            builder.HasKey(Course => Course.CourseId);
            builder.Property(Course => Course.CourseTitle).IsRequired();
            builder.Property(Course => Course.CourseDescription).IsRequired();
            builder.Property(Course => Course.CourseListPrice).HasColumnType("decimal(7, 2)").IsRequired();
            builder.Property(Course => Course.CourseCreatedAt).IsRequired();
            builder.Property(Course => Course.CourseImageUrl).IsRequired();
            builder.Property(Course => Course.CourseDurationTime).HasColumnName("DurationTime(in Hours)").HasColumnType("decimal(5, 2)");

            // Configuration of the relationship with CourseCategory
            builder.HasOne(Course => Course.CourseCategory)
                .WithMany(CourseCategory => CourseCategory.Courses)
                .HasForeignKey(Course => Course.CourseCategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            // Configure the one-to-many relationship between Course and Level
            builder.HasOne(Course => Course.Level)
                   .WithMany(Level => Level.Courses)
                   .HasForeignKey(Course => Course.LevelId)
                   .OnDelete(DeleteBehavior.Restrict);

            // Configure the one-to-many relationship between Course and Language
            builder.HasOne(Course => Course.Language)
                   .WithMany(Language => Language.Courses)
                   .HasForeignKey(Course => Course.LanguageId)
                   .OnDelete(DeleteBehavior.Restrict);

            // Configure the one to many relationship between Course and CourseVideo
            builder.HasMany(Course => Course.CourseVideos)
                   .WithOne(CourseVideo => CourseVideo.Course)
                   .HasForeignKey(CourseVideo => CourseVideo.CourseId)
                   .OnDelete(DeleteBehavior.Restrict);

            // Configure the one-to-many relationship between Course and CourseReview
            builder.HasMany(Course => Course.CourseReviews)
                   .WithOne(CourseReview => CourseReview.Course)
                   .HasForeignKey(CourseReview => CourseReview.CourseId)
                   .OnDelete(DeleteBehavior.Restrict);

        }
    }
}