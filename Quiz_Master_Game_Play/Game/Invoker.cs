namespace Quiz_Master_Game_Play.Game
{
	using Quiz_Master_Game_Play.Commands.Contract;
	using Quiz_Master_Game_Play.Game.Contract;

	public class Invoker
	{
		private readonly HashSet<ICommand> commands;

		public Invoker(IEnumerable<ICommand> availableCommands)
		{
			this.commands = availableCommands.ToHashSet();
		}

		public bool Handle(IGame game)
		{
			string? cmdStr = game.Cmd.Command;
			ICommand? cmd = this.commands.FirstOrDefault(c => c.CommandString == cmdStr);
			if (cmd == null)
			{
				game.Writer.WriteLine("Unknown command.");
				return false;
			}

			if (!cmd.CanExecute(game))
			{
				game.Writer.WriteLine("You do not have permission to run this command.");
				return false;
			}

			cmd.Execute(game);

			return true; 
		}

	}
}
