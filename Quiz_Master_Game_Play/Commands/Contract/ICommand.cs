namespace Quiz_Master_Game_Play.Commands.Contract
{
	using Common.Classes;
	using Quiz_Master_Game_Play.Game.Contract;

	public interface ICommand
	{
        public string CommandString { get; }
        bool Execute(IGame game);
	}
}
