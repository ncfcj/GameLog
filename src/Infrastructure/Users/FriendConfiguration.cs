using Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Users;

internal sealed class FriendConfiguration : IEntityTypeConfiguration<Friends>
{
    public void Configure(EntityTypeBuilder<Friends> builder)
    {
        builder.HasKey(f => f.Id);
        
        builder.HasOne(p => p.FirstUser)
            .WithMany()
            .HasForeignKey(p => p.FirstUserId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasOne(p => p.SecondUser)
            .WithMany()
            .HasForeignKey(p => p.SecondUserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
