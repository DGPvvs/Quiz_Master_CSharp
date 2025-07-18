namespace Quiz_Master_Game_Play.Questions
{
	using Common.Constants;
	using Common.Enums;
	using Common.IO.Contract;
	using Quiz_Master_Game_Play.Questions.Contract;
	using System.Text;

	public class SingleChoiceQuestion : Question, IQuestion
	{
		private List<string> questions;

		public SingleChoiceQuestion(IWriter writer, IReader reader, string description, string correctAnswer, uint points, bool isTest)
			: base(writer, reader, description, correctAnswer, points, isTest, 4)
		{
			this.Qt = QuestionType.SC;
		}

		public SingleChoiceQuestion(IWriter writer, IReader reader, string description, string correctAnswer, uint points, bool isTest, byte questionsCount)
			: base(writer, reader, description, correctAnswer, points, isTest, questionsCount)
		{
			this.Qt = QuestionType.SC;
			this.questions = new List<string>();
		}

		public List<string> Questions => this.questions;

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

		public virtual void SetUpData(string dataString)
		{
			List<string> quest = dataString.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries).ToList();

			for (int i = 0; i < quest.Count; ++i)
			{
				this.questions.Add(quest[i]);
			}
		}

		public virtual string BuildQuestionData()
		{
			StringBuilder sb = new StringBuilder();

			sb.AppendLine(this.Qt.ToString());
			sb.AppendLine(this.Description);
			sb.AppendLine(this.CorrectAnswer);
			sb.AppendLine(this.Points.ToString());
			sb.AppendLine(this.questions.Count.ToString());

			for (int i = 0; i < this.questions.Count; ++i)
			{
				sb.AppendLine(this.questions[i]);
			}

			return sb.ToString();
		}

		protected override void PrintQuestion()
		{
			this.Writer.WriteLine($"{this.Description}\t({this.Points} points)");

			for (int i = 0; i < this.questions.Count; ++i)
			{
				char ch = (char)((int)'A' + i);
				this.Writer.WriteLine($"{ch}) {this.questions[i]}");
			}

			base.PrintQuestion();
		}

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

		public virtual string ToStringFile()
		{
			StringBuilder sb = new StringBuilder();
			sb.AppendLine($"Description: {this.Description}");

			sb.AppendLine("Posible answers:");

			for (int i = 0; i < this.Questions.Count; i++)
			{
				char ch = (char)((int)'a' + i);

				sb.AppendLine($"{ch}) {this.Questions[i]}");
			}

			sb.AppendLine($"Correct answer: {this.CorrectAnswer}");

			return sb.ToString();
		}
	}
}
