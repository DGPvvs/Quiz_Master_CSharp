namespace Quiz_Master_Game_Play.Commands
{
	using Common.Constants;
	using Common.Enums;
	using Quiz_Master_Game_Play.Game.Contract;
	using Quiz_Master_Game_Play.Users;

	public class ApproveQuizCommand : Command
	{
		public ApproveQuizCommand(string commandString) : base(commandString)
		{
		}

		public override bool CanExecute(IGame game)
		{
			if ((game.User is Admin) && (game.Cmd.Command == this.CommandString) && (game.Cmd.ParamRange == 2))
			{
				return uint.TryParse(game.Cmd.Param1, out uint result);
			}

			return false;
		}

		public override void Execute(IGame game)
		{
			string quizIdString = game.Cmd.Param1!;
			uint quizId = uint.Parse(quizIdString);

			string s = game.User.Quiz.FindAllQuizzes();

			List<string> quizzesVec = s
				.Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries)
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
					return;
				}
			}

			game.Writer.WriteLine("No quiz with the specified id was found pending approval.");
		}
	}
}
