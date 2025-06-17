namespace Quiz_Master_Game_Play.Commands.Contract
{
	using Common.Classes;
	using Quiz_Master_Game_Play.Users.Contract;

	public interface ICommand
	{
		bool Execute(CommandStruct cmdStr, IUser user);
	}
}
