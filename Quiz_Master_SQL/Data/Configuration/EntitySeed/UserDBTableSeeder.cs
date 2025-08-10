namespace Quiz_Master_SQL.Data.Configuration.EntitySeed
{
	using global::Quiz_Master_SQL.Data.Configuration.EntitySeed.SeedData;
	using global::Quiz_Master_SQL.Data.Models;
	using Microsoft.EntityFrameworkCore;

	public class UserDBTableSeeder : IEntityTypeConfiguration<UserDB>
	{
		public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<UserDB> builder)
		{
			builder.HasData(new SeedsData().UserDBSeeder());
		}
	}
}
