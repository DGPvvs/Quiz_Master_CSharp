namespace Quiz_Master_Game_Play.Commands
{
	using Common.Constants;
	using Common.Enums;
	using Quiz_Master_Game_Play.Game.Contract;
	using Quiz_Master_Game_Play.QuizClass;

	public class StartQuizCommand : LoadQuizCommand
	{
		public StartQuizCommand(string commandString) : base(commandString)
		{
		}

		public override bool CanExecute(IGame game)
		{
			if (base.CanExecute(game) && (game.Cmd.ParamRange >= 2))
			{
				return (uint.TryParse(game.Cmd.Param1, out uint result))
					&& (game.Cmd.Param2 == GlobalConstants.TEST
						|| game.Cmd.Param2 == GlobalConstants.NORMAL);
			}

			return false;
		}

		public override void Execute(IGame game)
		{
			string quizId = game.Cmd.Param1!;
			string mode = game.Cmd.Param2!;
			string shuffle = game.Cmd.Param3!;

			bool isTest = (mode == GlobalConstants.TEST)
				? GlobalConstants.TEST_MODE
				: GlobalConstants.NORMAL_MODE;

			bool isShuffle = (shuffle == GlobalConstants.SHUFFLE)
				? true
				: false;

			Quiz quiz = this.LoadQuiz(game, quizId, isTest);

			if (quiz != null)
			{
				uint[] order = game.User.GetOrder(isShuffle, quiz.NumberOfQuestions);

				for (int i = 0; i < quiz.NumberOfQuestions; ++i)
				{
					uint idx = order[i];
					game.User.AddPoints(quiz.Questions[(int)idx].Action());
				}

				if (isTest)
				{
					game.User.NumberSolvedTestQuizzes += 1;
					game.User.AddQuizChallenge(ChallengerOptions.TestQuizChallenger);
				}
				else
				{
					game.User.NumberSolvedNormalQuizzes += 1;
					game.User.AddQuizChallenge(ChallengerOptions.NormalQuizChallenger);
				}
			}
			else
			{
				game.Writer.WriteLine("Quiz not found");
			}
		}
	}
}
