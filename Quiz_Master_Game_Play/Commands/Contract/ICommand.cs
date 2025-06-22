namespace Quiz_Master_Game_Play.Commands.Contract
{
	using Quiz_Master_Game_Play.Game.Contract;

	public interface ICommand
	{
        public string CommandString { get; }
        public bool Execute(IGame game);
	}
}
