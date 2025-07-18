namespace Quiz_Master_Game_Play.Commands
{
	using Quiz_Master_Game_Play.Game.Contract;
	using Quiz_Master_Game_Play.QuizClass;
	using Quiz_Master_Game_Play.Users;

	public class AddToFavsCommand : Command
	{
		public AddToFavsCommand(string commandString) : base(commandString)
		{
		}

		public override bool CanExecute(IGame game)
		{
			if ((game.User is Player) && (game.Cmd.Command == this.CommandString) && (game.Cmd.ParamRange == 2))
			{
				return uint.TryParse(game.Cmd.Param1, out uint result);
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

			bool isNotFound = true;
			int i = 0;

			while (i < quizzesVec.Count && isNotFound)
			{
				QuizIndexDTO qiDTO = new QuizIndexDTO();

				qiDTO.SetElement(quizzesVec[i]);

				if (qiDTO.Id == uint.Parse(game.Cmd.Param1!))
				{
					game
						.User
						.ListFavoriteQuizzes
						.Add(qiDTO.Id);

					game
						.User
						.NumberFavoriteQuizzes = (uint)game
													.User
													.ListFavoriteQuizzes
													.Count;
					isNotFound = false;
				}

				i++;
			}

			if (isNotFound)
			{
				game.Writer.WriteLine($"No quiz found with ID {game.Cmd.Param1}");
			}
		}
	}
}
