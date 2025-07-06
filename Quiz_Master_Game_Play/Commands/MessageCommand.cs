namespace Quiz_Master_Game_Play.Commands
{
	using Common.Constants;
	using Quiz_Master_Game_Play.Game.Contract;
	using Quiz_Master_Game_Play.Users;
	using System.Numerics;

	public class MessageCommand : Command
	{
		public MessageCommand(string commandString) : base(commandString)
		{
		}

		public override bool CanExecute(IGame game)
		{
			if ((game.User is Player) && (game.Cmd.Command == this.CommandString) && (game.Cmd.ParamRange == 1))
			{
				return true;
			}

			return false;
		}

		public override void Execute(IGame game)
		{
			List<string> v = game
				.User
				.Message
				.FindAllMessages()
				.Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries)
				.ToList();

			foreach (var item in v)
			{
				List<string> vv = item
					.Split(GlobalConstants.MESSAGE_ELEMENT_DATA_SEPARATOR, StringSplitOptions.RemoveEmptyEntries)
					.ToList();

				if (uint.Parse(vv[0]) == game.User.Id)
				{
					game.Writer.WriteLine(vv[1]);
				}
			}
		}
	}
}
