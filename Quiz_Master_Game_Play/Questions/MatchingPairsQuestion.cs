namespace Quiz_Master_Game_Play.Questions
{
	using Common.Enums;
	using Common.IO.Contract;
	using System.Text;

	public class MatchingPairsQuestion : MultipleChoiceQuestion
	{
		private List<string> answers;

		public MatchingPairsQuestion(IWriter writer, IReader reader, string description, string correctAnswer, uint points, bool isTest, byte questionsCount)
			: base(writer, reader, description, correctAnswer, points, isTest, questionsCount)
		{
			this.Qt = QuestionType.MP;
			this.answers = new List<string>();
		}

		public List<string> Answers => this.answers;

		public override void SetUpData(string s)
		{
			throw new NotImplementedException(s);
		}

		public override string BuildQuestionData()
		{
			StringBuilder sb = new StringBuilder();
			sb.AppendLine(base.BuildQuestionData());

			sb.AppendLine(uint.Parse(this.answers.Count.ToString()).ToString());

			for (int i = 0; i < this.answers.Count; i++)
			{
				sb.AppendLine(this.answers[i]);
			}

			return sb.ToString();
		}

		protected override void PrintQuestion()
		{
			this.Writer.WriteLine($"{this.Description}\t({this.Points} points)");

			for (int i = 0; i < this.Questions.Count; ++i)
			{
				char ch = (char)(i + 'A');
				this.Writer.WriteLine($"{ch}) {this.Questions[i]}");
			}

			this.Writer.WriteLine(string.Empty);

			for (int i = 0; i < this.Questions.Count; ++i)
			{
				char ch = (char)(i + 'a');
				this.Writer.WriteLine($"{ch}) {this.Questions[i]}");
			}

			base.PrintQuestion();
		}

		public override string ToStringFile()
		{
			StringBuilder sb = new StringBuilder();
			sb.AppendLine($"Description: {this.Description}");
			sb.AppendLine("First group answers:");

			for (int i = 0; i < this.Questions.Count; i++)
			{
				char ch = (char)(i + 'A');

				sb.AppendLine($"{ch}) {this.Questions[i]}");
			}

			sb.AppendLine("Second group answers:");

			for (int i = 0; i < this.Answers.Count; i++)
			{
				char ch = ((char)(i + 'a'));

				sb.AppendLine($"{ch}) {this.Answers[i]}");
			}

			sb.AppendLine($"Correct answer: {this.CorrectAnswer}");

			return sb.ToString();
		}
	}
}
