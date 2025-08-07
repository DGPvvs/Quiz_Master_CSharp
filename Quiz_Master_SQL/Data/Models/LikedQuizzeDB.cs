namespace Quiz_Master_SQL.Data.Models
{
	using System.ComponentModel.DataAnnotations.Schema;

	public class LikedQuizzeDB
	{
		[ForeignKey(nameof(this.QuizDBs))]
		public Guid QuizId { get; set; }

		[ForeignKey(nameof(this.UserDBs))]
		public Guid UserId { get; set; }
		public virtual QuizDB QuizDBs { get; set; } = null!;
		public virtual UserDB UserDBs { get; set; } = null!;
	}
}
