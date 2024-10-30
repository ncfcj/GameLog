using System.Globalization;
using Domain.GameLogs;
using Domain.GameLogs.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace Infrastructure.GameLogs;

internal sealed class GameLogConfiguration : IEntityTypeConfiguration<GameLog>
{
    public void Configure(EntityTypeBuilder<GameLog> builder)
    {
        builder.HasKey(x => x.Id);
        
        builder.Property<string>(x => x.GameName)
            .HasColumnType("varchar(255)")
            .HasMaxLength(255);
        
        builder.Property(x => x.StartDate)
            .HasColumnType("Date")
            .IsRequired(false);
        
        builder.Property(x => x.EndDate)
            .HasColumnType("Date")
            .IsRequired(false);
        
        builder.Property(x => x.Review)
            .HasColumnType("varchar(255)")
            .HasMaxLength(255)
            .IsRequired(false);

        builder.Property(x => x.ReviewLikeCount)
            .HasColumnType("bigint");

        builder.Property(x => x.CreatedAt)
            .HasColumnType("Date");
        
        builder.Property(x => x.Platform)
            .HasColumnType("int");
        
        builder.Property(x => x.Rating)
            .HasColumnType("int")
            .IsRequired(false);
        
        builder.Property(x => x.LogStatus)
            .HasColumnType("int");
        
        builder.Property(x => x.SteamAppId)
            .HasColumnType("varchar(100)")
            .HasMaxLength(100)
            .IsRequired(false);
        
        
        // Relations
        builder
            .Property(g => g.Genres)
            .HasConversion(
                v => string.Join(",", v.Select(s => ((int)s).ToString(CultureInfo.InvariantCulture))),
                v => v.Split(",", StringSplitOptions.RemoveEmptyEntries).Select(s => (Genre)int.Parse(s, CultureInfo.InvariantCulture)).ToList()
            ).Metadata.SetValueComparer(new ValueComparer<List<Genre>>(
                (c1, c2) => c1!.SequenceEqual(c2!),
                c => c.Aggregate(0, (a, e) => HashCode.Combine(a, e.GetHashCode())),
                c => c.ToList()
            ));
        
        builder
            .HasOne(x => x.User)
            .WithMany()
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
