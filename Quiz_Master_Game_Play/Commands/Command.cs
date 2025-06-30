namespace Quiz_Master_Game_Play.Commands
{
	using Quiz_Master_Game_Play.Commands.Contract;
	using Quiz_Master_Game_Play.Game.Contract;

	public abstract class Command : ICommand
	{
		private readonly string commandString;

		public Command(string commandString)
		{
			this.commandString = commandString;
		}

		public string CommandString => this.commandString;

		public abstract bool CanExecute(IGame game);

		public abstract void Execute(IGame game);
	}
}
