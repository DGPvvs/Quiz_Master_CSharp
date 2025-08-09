namespace Quiz_Master_SQL.Data.Models
{
	using System.ComponentModel.DataAnnotations.Schema;

	public class MatchingPairsQuestionDB : QuestionDB
	{
		public MatchingPairsQuestionDB() : base()		
		{
			this.MatchingPairsCorrectAnswers = new HashSet<MultiplyAnswersDB>();
			this.MatchingPairsFirstAnswers = new HashSet<MultiplyAnswersDB>();
			this.MatchingPairsSecondAnswers = new HashSet<MultiplyAnswersDB>();
		}

		[InverseProperty(nameof(MultiplyAnswersDB.MPQCorrectAnswer))]
		public virtual ICollection<MultiplyAnswersDB> MatchingPairsCorrectAnswers { get; set; }

		[InverseProperty(nameof(MultiplyAnswersDB.MPQFirstAnswer))]
		public virtual ICollection<MultiplyAnswersDB> MatchingPairsFirstAnswers { get; set; }

		[InverseProperty(nameof(MultiplyAnswersDB.MPQSecondAnswer))]
		public virtual ICollection<MultiplyAnswersDB> MatchingPairsSecondAnswers { get; set; }
	}
}
