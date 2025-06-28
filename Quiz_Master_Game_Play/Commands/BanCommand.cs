namespace Quiz_Master_Game_Play.Commands
{
	using Quiz_Master_Game_Play.Game.Contract;

	public class BanCommand : Command
	{
		public BanCommand(string commandString) : base(commandString)
		{
		}

		public override bool Execute(IGame game)
		{
			throw new NotImplementedException();
		}
	}
}
