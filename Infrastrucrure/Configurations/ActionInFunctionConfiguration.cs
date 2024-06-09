using Domain.Entities.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Domain.Entities.Identity;
using Infrastrucrure.Constant;

namespace Infrastrucrure.Configurations;

internal sealed class ActionInFunctionConfiguration : IEntityTypeConfiguration<ActionInFunction>
{
    public void Configure(EntityTypeBuilder<ActionInFunction> builder)
    {
        builder.ToTable(TableName.ActionInFunctions);

        builder.HasKey(x => new { x.ActionId, x.FunctionId });
    }
}