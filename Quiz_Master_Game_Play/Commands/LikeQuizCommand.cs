namespace Quiz_Master_Game_Play.Commands
{
	using Common.Enums;
	using Quiz_Master_Game_Play.Game.Contract;
	using Quiz_Master_Game_Play.QuizClass;
	using Quiz_Master_Game_Play.Users;

	public class LikeQuizCommand : Command
	{
		public LikeQuizCommand(string commandString) : base(commandString)
		{
		}

		public override bool CanExecute(IGame game)
		{
			if ((game.User is Player) && (game.Cmd.Command == this.CommandString) && (game.Cmd.ParamRange == 2))
			{
				return true;
			}

			return false;
		}

		public override void Execute(IGame game)
		{
			List<string> quizzesVec = game
				.User
				.Quiz
				.FindAllQuizzes()
				.Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries)
				.ToList();

			List<string> quizVec = new List<string> ();

			foreach (var quiz in quizzesVec)
			{
				QuizIndexDTO qiDTO = new QuizIndexDTO();

				qiDTO.SetElement(quiz);

				bool isLikedQuiz = !ContainLikedQuizzes(qiDTO.Id, game)
					&& (qiDTO.QuizStatus == QuizStatus.ApprovedQuiz)
					&& (qiDTO.Id == uint.Parse(game.Cmd.Param1!));

				if (isLikedQuiz)
				{
					game.User.Quiz.SaveQuiz(QuizStatus.LikeQuiz, qiDTO.Id);
					game.User.ListLikedQuizzes.Add(qiDTO.Id);
					game.User.NumberLikedQuizzes = (uint)game.User.ListLikedQuizzes.Count;
					return;
				}
			}

			game.Writer.WriteLine("No test with the specified ID was found to be liked.");
		}

		private bool ContainLikedQuizzes(uint id, IGame game)
		{
			foreach (var item in game.User.ListLikedQuizzes)
			{
				if (item == id)
				{
					return true;
				}
			}

			return false;
		}
	}
}
