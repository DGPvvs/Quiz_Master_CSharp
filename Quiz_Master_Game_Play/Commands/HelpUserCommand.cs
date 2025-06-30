namespace Quiz_Master_Game_Play.Commands
{
	using Quiz_Master_Game_Play.Game.Contract;

	public class HelpUserCommand : Command
	{
		public HelpUserCommand(string commandString) : base(commandString)
		{
		}

		public override bool CanExecute(IGame game)
		{
			if (this.CommandString == game.Cmd.Command)
			{
				return true;
			}

			return false;
		}

		public override void Execute(IGame game)
		{
			game.User.Help();
		}
	}
}
