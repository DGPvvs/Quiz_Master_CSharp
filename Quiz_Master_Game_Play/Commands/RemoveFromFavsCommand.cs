namespace Quiz_Master_Game_Play.Commands
{
	using Quiz_Master_Game_Play.Game.Contract;
	using Quiz_Master_Game_Play.QuizClass;
	using Quiz_Master_Game_Play.Users;
	using System.Numerics;

	public class RemoveFromFavsCommand : Command
	{
		public RemoveFromFavsCommand(string commandString) : base(commandString)
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
			List<uint> newFavs = new List<uint>();

			bool isNotFound = true;

			foreach (var item in game.User.ListFavoriteQuizzes)
			{
				uint id = item;

				if (id != uint.Parse(game.Cmd.Param1!))
				{
					newFavs.Add(id);
				}
				else
				{
					isNotFound = false;
				}
			}

			if (isNotFound)
			{
				game.Writer.WriteLine($"No quiz found with ID {uint.Parse(game.Cmd.Param1!)}");
			}
			else
			{
				game.User.ListFavoriteQuizzes.Clear();

				foreach (var item in newFavs)
				{
					game.User.ListFavoriteQuizzes.Add(item);
				}
			}
		}
	}
}
