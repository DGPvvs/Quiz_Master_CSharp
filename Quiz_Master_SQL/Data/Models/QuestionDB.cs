namespace Quiz_Master_SQL.Data.Models
{
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;

	public abstract class QuestionDB
	{
		public QuestionDB()
		{
			this.Id = Guid.NewGuid();
		}

		[Key]
		public Guid Id { get; set; }

		public uint Num { get; set; }

		public string Description { get; set; } = null!;

		public uint Point { get; set; }

		[ForeignKey(nameof(this.Quiz))]
		public Guid QuizId { get; set; }

		public virtual QuizDB Quiz { get; set; } = null!;
	}
}
