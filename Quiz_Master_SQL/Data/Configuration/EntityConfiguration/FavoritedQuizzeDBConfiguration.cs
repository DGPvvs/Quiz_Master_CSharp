namespace Quiz_Master_SQL.Data.Configuration.EntityConfiguration
{
	using global::Quiz_Master_SQL.Data.Models;
	using Microsoft.EntityFrameworkCore;
	using Microsoft.EntityFrameworkCore.Metadata.Builders;

	internal class FavoritedQuizzeDBConfiguration : IEntityTypeConfiguration<FavoritedQuizzeDB>
	{
		public void Configure(EntityTypeBuilder<FavoritedQuizzeDB> builder)
		{
			builder
				.HasKey(fq => new { fq.QuizId, fq.UserId });

			builder
				.HasOne(x => x.Quiz)
				.WithMany(b => b.FavoritedQuizzes)
				.OnDelete(DeleteBehavior.NoAction);
		}
	}
}
