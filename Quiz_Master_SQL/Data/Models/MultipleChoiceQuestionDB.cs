namespace Quiz_Master_SQL.Data.Models
{
	using System.ComponentModel.DataAnnotations.Schema;

	public class MultipleChoiceQuestionDB : QuestionDB	
	{
		public MultipleChoiceQuestionDB() : base()
		{
			this.MultiplyCorrectAnswers = new HashSet<MultiplyAnswersDB>();
			this.MultiplyAnswers = new HashSet<MultiplyAnswersDB>();
		}

		[InverseProperty(nameof(MultiplyAnswersDB.MultipleChoiceQuestionCorrectAnswer))]
		public virtual ICollection<MultiplyAnswersDB> MultiplyCorrectAnswers { get; set; }

		[InverseProperty(nameof(MultiplyAnswersDB.MultipleChoiceQuestion))]
		public virtual ICollection<MultiplyAnswersDB> MultiplyAnswers { get; set; }
	}
}
