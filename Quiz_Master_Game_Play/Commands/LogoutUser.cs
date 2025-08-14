namespace Quiz_Master_Game_Play.Commands
{
	using Common.Constants;
	using Quiz_Master_Game_Play.Game.Contract;
	using Quiz_Master_Game_Play.Users;

	public class LogoutUserCommand : Command
	{
		public LogoutUserCommand(string commandString) : base(commandString)
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

			game.User.SaveData(game.User.Id);
			game.User = null!;

			game.User = new User(game.Writer, game.Reader, game.Provider);
		}
	}
}
