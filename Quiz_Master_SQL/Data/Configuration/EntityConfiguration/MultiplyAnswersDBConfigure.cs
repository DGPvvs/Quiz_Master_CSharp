namespace Quiz_Master_SQL.Data.Configuration.EntityConfiguration
{
	using global::Quiz_Master_SQL.Data.Models;
	using Microsoft.EntityFrameworkCore;
	using Microsoft.EntityFrameworkCore.Metadata.Builders;

	internal class MultiplyAnswersDBConfigure : IEntityTypeConfiguration<MultiplyAnswersDB>
	{
		public void Configure(EntityTypeBuilder<MultiplyAnswersDB> builder)
		{
			builder
				.HasOne(x => x.MultipleChoiceQuestion)
				.WithMany(b => b.MultiplyAnswers)
				.OnDelete(DeleteBehavior.NoAction);

			builder
				.HasOne(x => x.MultipleChoiceQuestionCorrectAnswer)
				.WithMany(b => b.MultiplyCorrectAnswers)
				.OnDelete(DeleteBehavior.NoAction);

			builder
				.HasOne(x => x.MPQCorrectAnswer)
				.WithMany(b => b.MatchingPairsCorrectAnswers)
				.OnDelete(DeleteBehavior.NoAction);

			builder
				.HasOne(x => x.MPQFirstAnswer)
				.WithMany(b => b.MatchingPairsFirstAnswers)
				.OnDelete(DeleteBehavior.NoAction);

			builder
				.HasOne(x => x.MPQSecondAnswer)
				.WithMany(b => b.MatchingPairsSecondAnswers)
				.OnDelete(DeleteBehavior.NoAction);
		}
	}
}
