namespace Quiz_Master_SQL.Data.Models
{
	public class ShortAnswerQuestionDB : QuestionDB
	{
		public ShortAnswerQuestionDB() : base()
		{			
		}

		public string Answer { get; set; } = null!;
	}
}
