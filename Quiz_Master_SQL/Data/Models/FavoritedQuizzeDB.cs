namespace Quiz_Master_SQL.Data.Models
{
	using System.ComponentModel.DataAnnotations.Schema;

	public class FavoritedQuizzeDB
	{
		[ForeignKey(nameof(this.User))]
		public Guid UserId { get; set; }

		public virtual UserDB User { get; set; } = null!;

		[ForeignKey(nameof(this.Quiz))]
		public Guid QuizId { get; set; }

		public virtual QuizDB Quiz { get; set; } = null!;
	}
}
