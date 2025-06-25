namespace Quiz_Master_Game_Play.Commands
{
	using Quiz_Master_Game_Play.Game.Contract;

	public class HelpUserCommand : Command
	{
		public HelpUserCommand(string commandString) : base(commandString)
		{
		}

		public override bool Execute(IGame game)
		{
			if (this.CommandString == game.Cmd.Command)
			{
				game.User.Help();

				return true;
			}

			return false;
		}
	}
}
