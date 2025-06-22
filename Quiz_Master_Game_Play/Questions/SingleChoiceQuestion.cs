namespace Quiz_Master_Game_Play.Questions
{
	using Common.Enums;
	using Common.IO.Contract;
	using Quiz_Master_Game_Play.Questions.Contract;
	using System.Numerics;

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
		}

		public List<string> Questions => this.questions;

		virtual unsigned int Action() override;
    virtual void SetUpData(String&) override;
    virtual String BuildQuestionData() override;
    virtual void PrintQuestion() override;
    virtual bool AnswerAQuestion() override;
    virtual String ToStringFile() uint IQuestion.Action()
		{
			throw new NotImplementedException();
		}

		void IQuestion.SetUpData(string dataString)
		{
			throw new NotImplementedException();
		}

		string IQuestion.BuildQuestionData()
		{
			throw new NotImplementedException();
		}

		string IQuestion.ToStringFile()
		{
			throw new NotImplementedException();
		}

		override;

    SingleChoiceQuestion(IWriter*, IReader*, String&, String&, unsigned int, bool);
		SingleChoiceQuestion(IWriter*, IReader*, String&, String&, unsigned int, bool, unsigned int);

		virtual ~SingleChoiceQuestion() { };
	}
}
