namespace Quiz_Master_SQL.Data.Configuration.EntityConfiguration
{
	using global::Quiz_Master_SQL.Data.Models;
	using Microsoft.EntityFrameworkCore;
	using Microsoft.EntityFrameworkCore.Metadata.Builders;

	internal class LikedQuizzeDBConfiguration : IEntityTypeConfiguration<LikedQuizzeDB>
	{
		public void Configure(EntityTypeBuilder<LikedQuizzeDB> builder)
		{
			builder
				.HasKey(lq => new { lq.QuizId, lq.UserId });

			builder
				.HasOne(x => x.QuizDBs)
				.WithMany(b => b.LikedQuizzes)
				.OnDelete(DeleteBehavior.NoAction);
		}
	}
}
