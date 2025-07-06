namespace Quiz_Master_Game_Play.Commands
{
	using Common.Constants;
	using Quiz_Master_Game_Play.Game.Contract;
	using Quiz_Master_Game_Play.Users;
	using System.Text;

	public class ViewChalleengesCommand : Command
	{
		public ViewChalleengesCommand(string commandString) : base(commandString)
		{
		}

		public override bool CanExecute(IGame game)
		{
			if ((game.User is Player) && (game.Cmd.Command == GlobalConstants.VIEW_CHALLEENGES) && (game.Cmd.ParamRange == 1))
			{
				return true;
			}

			return false;
		}

		public override void Execute(IGame game)
		{
			StringBuilder sb = new StringBuilder();

			for (int i = 5; i <= 30; i += 5)
			{
				if (game.User.NumberCreatedQuizzes < i)
				{
					sb.AppendLine($"Create {i} quizzes ({game.User.NumberCreatedQuizzes}/{i})");
				}
			}

			for (int i = 10; i <= 100; i += 10)
			{
				if (game.User.NumberSolvedTestQuizzes < i)
				{
					sb.AppendLine($"Complete {i} quizzes in test mode ({game.User.NumberSolvedTestQuizzes}/{i})");
				}
			}

			for (int i = 10; i <= 100; i += 10)
			{
				if (game.User.NumberSolvedNormalQuizzes < i)
				{
					sb.AppendLine($"Complete {i} quizzes in normal mode ({game.User.NumberSolvedNormalQuizzes}/{i})");
				}
			}

			game.Writer.WriteLine(sb.ToString().TrimEnd());
		}
	}
}
