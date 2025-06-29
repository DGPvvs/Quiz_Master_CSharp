namespace Quiz_Master_Game_Play.Commands
{
	using Common.Constants;
	using Common.Enums;
	using Quiz_Master_Game_Play.Game.Contract;
	using Quiz_Master_Game_Play.Users;
	using System.ComponentModel.Design;

	public class ApproveQuizCommand : Command
	{
		public ApproveQuizCommand(string commandString) : base(commandString)
		{
		}

		public override bool Execute(IGame game)
		{
			if ((game.User is Admin) && (game.Cmd.Command == this.CommandString) && (game.Cmd.ParamRange == 2))
			{
				string quizIdString = game.Cmd.Param1!;
				uint quizId = uint.Parse(quizIdString);

				string s = game.User.Quiz.FindAllQuizzes();

				List<string> quizzesVec = s
					.Split(new char[] { '\r', '\n'}, StringSplitOptions.RemoveEmptyEntries)
					.ToList();

				foreach (string quiz in quizzesVec)
				{
					List<string> quizVec = quiz
						.Split(GlobalConstants.QUIZ_ELEMENT_DATA_SEPARATOR, StringSplitOptions.RemoveEmptyEntries)
						.ToList();

					uint id = uint.Parse(quizVec[0]);
					QuizStatus qs = (QuizStatus)Enum.Parse(typeof(QuizStatus), quizVec[4]);

					if (id == quizId && (qs == QuizStatus.NewQuiz || qs == QuizStatus.EditQuiz))
					{
						game.User.Quiz.SaveQuiz(QuizStatus.ApprovedQuiz, id);
						return true;
					}
				}				

				game.Writer.WriteLine("No quiz with the specified id was found pending approval.");

				return true;
			}

			return false;
		}
	}
}
