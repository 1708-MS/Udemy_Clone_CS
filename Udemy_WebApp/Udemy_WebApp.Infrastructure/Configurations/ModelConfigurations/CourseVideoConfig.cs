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
    public class CourseVideoConfig : IEntityTypeConfiguration<CourseVideo>
    {
        public void Configure(EntityTypeBuilder<CourseVideo> builder)
        {
            builder.HasKey(CourseVideo => CourseVideo.CourseVideoId);
            builder.Property(CourseVideo => CourseVideo.CourseVideoTitle).IsRequired();
            builder.Property(CourseVideo => CourseVideo.CourseVideoUrl).IsRequired();

            // Configure the may to one relationship between CourseVideo and Course
            builder.HasOne(CourseVideo => CourseVideo.Course)
                   .WithMany(Course => Course.CourseVideos)
                   .HasForeignKey(CourseVideo => CourseVideo.CourseId)
                   .OnDelete(DeleteBehavior.Restrict);

        }
    }
}