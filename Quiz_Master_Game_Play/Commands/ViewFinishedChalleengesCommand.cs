namespace Quiz_Master_Game_Play.Commands
{
	using Quiz_Master_Game_Play.Game.Contract;
	using Quiz_Master_Game_Play.Users;

	public class ViewFinishedChalleengesCommand : Command
	{
		public ViewFinishedChalleengesCommand(string commandString) : base(commandString)
		{
		}

		public override bool CanExecute(IGame game)
		{
			if (game.User is Player && this.CommandString == game.Cmd.Command && game.Cmd.ParamRange == 1)
			{
				return true;
			}

			return false;
		}

		public override void Execute(IGame game)
		{
			foreach (var item in game.User.ListFinishedChallenges)
			{
				game.Writer.WriteLine(item);
			}
		}
	}
}
