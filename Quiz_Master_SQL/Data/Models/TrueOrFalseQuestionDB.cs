namespace Quiz_Master_SQL.Data.Models
{
	public class TrueOrFalseQuestionDB : QuestionDB
	{
		public TrueOrFalseQuestionDB() : base()
		{
			
		}

		public bool Answer { get; set; }

		override public string ToString() => this.Answer.ToString();
	}
}
