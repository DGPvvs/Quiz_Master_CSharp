namespace Quiz_Master_Game_Play.Questions
{
	using Common.Enums;
	using Common.IO.Contract;
	using Quiz_Master_Game_Play.Questions.Contract;

	public class TrueOrFalseQuestion : Question, IQuestion
	{
		public TrueOrFalseQuestion(IWriter writer, IReader reader, string description, string correctAnswer, uint points, bool isTest)
			: base(writer, reader, description, correctAnswer, points, isTest, 1)
		{
			this.Qt = QuestionType.TF;
		}

		public uint Action()
		{
			uint result = this.Points;

			this.PrintQuestion();

			if (!this.AnswerAQuestion())
			{
				result = 0;
			}

			this.Writer.WriteLine(string.Empty);

			this.PrintTestCondition();

			return result;
		}

		public void SetUpData(string dataString)
		{
		}

		public virtual string BuildQuestionData() => $"{this.Qt}{Environment.NewLine}{this.Description}{Environment.NewLine}{this.CorrectAnswer}{Environment.NewLine}{this.Points}{Environment.NewLine}";


		protected override bool AnswerAQuestion()
		{
			bool result = false;

			string answer = this.Reader.ReadLine();

			if (answer == this.CorrectAnswer)
			{
				result = true;
			}

			return result;
		}

		protected override void PrintQuestion()
		{
			this.Writer.WriteLine($"{this.Description}\t({this.Points} points)");
			base.PrintQuestion();
		}

		public string ToStringFile() => $"Description: {this.Description}{Environment.NewLine}Correct answer: {this.CorrectAnswer}{Environment.NewLine}";
	}
}
