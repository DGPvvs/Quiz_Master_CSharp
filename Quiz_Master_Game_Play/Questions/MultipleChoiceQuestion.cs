namespace Quiz_Master_Game_Play.Questions
{
	using Common.Constants;
	using Common.Enums;
	using Common.IO.Contract;
	using System.Numerics;

	using static Common.Constants.GlobalConstants;

	public class MultipleChoiceQuestion : SingleChoiceQuestion
	{
		private byte percent;

		public MultipleChoiceQuestion(IWriter writer, IReader reader, string description, string correctAnswer, uint points, bool isTest, byte questionsCount = 4)
			: base(writer, reader, description, correctAnswer, points, isTest, questionsCount)
		{
			this.percent = 0;
			this.Qt = QuestionType.MC;
		}

		private void SeparateAnswers(List<string> v, string s)
		{
			List<string> tmp1 = s.Split(GlobalConstants.ELEMENT_DATA_SEPARATOR, StringSplitOptions.RemoveEmptyEntries).ToList();

			v.Clear();

			for (int i = 0; i < tmp1.Count; i++)
			{
				List<string> tmp = tmp1[i].Split(GlobalConstants.COMMA_DATA_SEPARATOR, StringSplitOptions.RemoveEmptyEntries).ToList();

				for (int j = 0; j < tmp.Count; j++)
				{
					v.Add(tmp[j]);
				}
			}
		}

		protected override bool AnswerAQuestion()
		{
			bool result = false;

			string answer = this.Reader.ReadLine();

			List<string> correctAnswersVec = new List<string>();
			List<string> answersVec = new List<string>();

			this.SeparateAnswers(correctAnswersVec, this.CorrectAnswer);
			this.SeparateAnswers(answersVec, answer);

			uint countCorrectAnswers = 0;

			for (int i = 0; i < answersVec.Count; i++)
			{
				for (int j = 0; j < correctAnswersVec.Count; j++)
				{
					if (answersVec[i] == correctAnswersVec[j])
					{
						countCorrectAnswers++;
						break;
					}
				}
			}

			this.percent = (byte)(100.0 * countCorrectAnswers / this.NumOfAnswers);

			if (this.percent > 0)
			{
				result = true;
				this.Points = (percent * this.Points / 100);
			}

			return result;
		}
	}
}
