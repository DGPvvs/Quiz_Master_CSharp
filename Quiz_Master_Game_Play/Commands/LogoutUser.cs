namespace Quiz_Master_Game_Play.Commands
{
	using Common.Constants;
	using Quiz_Master_Game_Play.Game.Contract;
	using Quiz_Master_Game_Play.Users;

	using static Common.Constants.GlobalConstants;

	public class LogoutUserCommand : Command
	{
		public LogoutUserCommand(string commandString) : base(commandString)
		{
		}

		public override bool Execute(IGame game)
		{
			if (this.CommandString == game.Cmd.Command)
			{
				game.User.SaveData();
				game.User = null;

				if (game.Cmd.Param5 != GlobalConstants.EXIT)
				{
					game.User = new User(game.Writer, game.Reader, game.Provider);
				}

				return true;
			}

			return false;
		}
	}
}
