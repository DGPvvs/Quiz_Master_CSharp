namespace Quiz_Master_Game_Play.Commands
{
	using Common.Classes;
	using Quiz_Master_Game_Play.Commands.Contract;
	using Quiz_Master_Game_Play.Users.Contract;

	public abstract class Command : ICommand
	{
		private readonly string commandString;

        public Command(string commandString)
        {
			this.commandString = commandString;            
        }

        public abstract bool Execute(CommandStruct cmdStr, IUser user);
	}
}
