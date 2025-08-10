namespace Quiz_Master_SQL.Data.Configuration.EntitySeed
{
	using global::Quiz_Master_SQL.Data.Configuration.EntitySeed.SeedData;
	using global::Quiz_Master_SQL.Data.Models;
	using Microsoft.EntityFrameworkCore;
	using Microsoft.EntityFrameworkCore.Metadata.Builders;

	public class ConfigTableDBSeeder : IEntityTypeConfiguration<ConfigTableDB>
	{
		public void Configure(EntityTypeBuilder<ConfigTableDB> builder)
		{
			builder.HasData(new SeedsData().ConfigTableDBSeeder());
		}
	}
}
