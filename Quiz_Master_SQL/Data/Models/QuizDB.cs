namespace Quiz_Master_SQL.Data.Models
{
	using Common.Enums;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;

	public class QuizDB
	{
		public QuizDB()
		{
			this.Id = Guid.NewGuid();
		}

		[Key]
		public Guid Id { get; set; }

		[ForeignKey(nameof(this.UserDb))]
		public Guid UserId { get; set; }

		public virtual UserDB UserDb { get; set; } = null!;

		public QuizStatus QuizStatus { get; set; }

		public uint NumOfQuestions { get; set; }

		public uint Likes { get; set; }

		public string QuizName { get; set; } = null!;
	}
}
