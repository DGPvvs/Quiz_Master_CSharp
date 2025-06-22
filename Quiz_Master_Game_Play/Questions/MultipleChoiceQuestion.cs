namespace Quiz_Master_Game_Play.Questions
{
	using Common.Enums;
	using Common.IO.Contract;
	using System.Numerics;

	using static Common.Constants.GlobalConstants;

	public  class MultipleChoiceQuestion : SingleChoiceQuestion
	{
		private byte percent;

		public MultipleChoiceQuestion(IWriter writer, IReader reader, string description, string correctAnswer, uint points, bool isTest, uint questionsCount = 4)
			: base(writer, reader, description, correctAnswer, points, isTest, questionsCount)
		{
			this.percent = 0;
			this.Qt = QuestionType.MC;
		}

		private void SeparateAnswers(List<string> v, string s)
		{
			List<string> tmp1 = s.Split(ELEMENT_DATA_SEPARATOR, StringSplitOptions.RemoveEmptyEntries).ToList();

			v.Clear();

			for (int i = 0; i < tmp1.Count; i++)
			{
				List<string> tmp = tmp1[i].Split(COMMA_DATA_SEPARATOR, StringSplitOptions.RemoveEmptyEntries).ToList();

				for (int j = 0; j < tmp.Count; j++)
				{
					v.Add(tmp[j]);
				}
			}
		}

		public override bool AnswerAQuestion()
		{
			bool result = false;

			String* answer = this->Reader()->ReadLine();

			Vector<String> correctAnswersVec, answersVec;

			this->SeparateAnswers(correctAnswersVec, this->GetCorrectAnswer());
			this->SeparateAnswers(answersVec, *answer);

			uint countCorrectAnswers = 0;

			for (size_t i = 0; i < answersVec.getSize(); i++)
			{
				for (size_t j = 0; j < correctAnswersVec.getSize(); j++)
				{
					if (answersVec[i] == correctAnswersVec[j])
					{
						countCorrectAnswers++;
						break;
					}
				}
			}

			this->percent = 100.0 * countCorrectAnswers / this->GetNumOfAnswers();

			if (this->percent > 0)
			{
				result = true;
				this->SetPoints(percent * this->GetPoints() / 100);
			}

			delete answer;
			answer = nullptr;

			return result;
		}
	}
}
