namespace Quiz_Master_SQL.Data.Models
{
	public class SingleChoiceQuestionDB : QuestionDB
	{
		public SingleChoiceQuestionDB() : base()
		{			
			this.MultiplyAnswers = new HashSet<MultiplyAnswersDB>();
		}

		public virtual ICollection<MultiplyAnswersDB> MultiplyAnswers { get; set; }
	}
}
