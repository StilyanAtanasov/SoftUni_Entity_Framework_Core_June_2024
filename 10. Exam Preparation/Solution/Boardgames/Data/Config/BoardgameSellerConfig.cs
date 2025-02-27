using Boardgames.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Boardgames.Data.Config;

public class BoardgameSellerConfig : IEntityTypeConfiguration<BoardgameSeller>
{
    public void Configure(EntityTypeBuilder<BoardgameSeller> builder)
    {
        builder.HasKey(bs => new { bs.BoardgameId, bs.SellerId });
    }
}