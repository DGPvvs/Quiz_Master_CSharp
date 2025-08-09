namespace Quiz_Master_SQL.Data.Models
{
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;

	public class MultiplyAnswersDB
	{
		public MultiplyAnswersDB()
		{
			this.Id = Guid.NewGuid();
		}

		[Key]
		public Guid Id { get; set; }

		public string Answer { get; set; } = null!;

		[ForeignKey(nameof(this.SingleChoiceQuestion))]
		public Guid SingleChoiceQuestionId { get; set; }

		public virtual SingleChoiceQuestionDB? SingleChoiceQuestion { get; set; }

		[ForeignKey(nameof(this.MultipleChoiceQuestionCorrectAnswer))]
		public Guid MultipleChoiceQuestionCorrectAnswerId { get; set; }

		public virtual MultipleChoiceQuestionDB? MultipleChoiceQuestionCorrectAnswer { get; set; }

		[ForeignKey(nameof(this.MultipleChoiceQuestion))]
		public Guid MultipleChoiceQuestionId { get; set; }

		public virtual MultipleChoiceQuestionDB? MultipleChoiceQuestion { get; set; }

		[ForeignKey(nameof(this.MPQCorrectAnswer))]
		public Guid MPQCorrectAnswerId { get; set; }

		public virtual MatchingPairsQuestionDB? MPQCorrectAnswer { get; set; }

		[ForeignKey(nameof(this.MPQFirstAnswer))]	
		public Guid MPQFirstAnswerId { get; set; }

		public virtual MatchingPairsQuestionDB? MPQFirstAnswer { get; set; }

		[ForeignKey(nameof(this.MPQSecondAnswer))]
		public Guid MPQSecondAnswerId { get; set; }

		public virtual MatchingPairsQuestionDB? MPQSecondAnswer { get; set; }

		
	}
}
