namespace Quiz_Master_Game_Play.Questions
{
	using Common.Enums;
	using Common.IO.Contract;

	public class ShortAnswerQuestion : TrueOrFalseQuestion
	{
		public ShortAnswerQuestion(IWriter writer, IReader reader, string description, string correctAnswer, uint points, bool isTest)
			: base(writer, reader, description, correctAnswer, points, isTest)
		{
			this.Qt = QuestionType.ShA;
		}
	}
}
