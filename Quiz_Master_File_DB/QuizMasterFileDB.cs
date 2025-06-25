namespace Quiz_Master_File_DB
{
	using Common.Constants;
	using Quiz_Master_Game_Play.Game.Contract;
	using Quiz_Master_Game_Play.Game;
	using Quiz_Master_File_DB.IO;
	using Quiz_Master_File_DB.BaseProvider;
	using Quiz_Master_Game_Play.Commands.Contract;
	using Quiz_Master_Game_Play.Commands;

	public class QuizMasterFileDB
	{
		static void Main(string[] args)
		{
			ConsoleWriter writer = new ConsoleWriter();
			ConsoleReader reader = new ConsoleReader();
			FileBaseProvider provider = new FileBaseProvider();

			List<ICommand> commands = new List<ICommand>()
			{
				new LogoutUserCommand(GlobalConstants.LOGOUT)
				, new HelpUserCommand(GlobalConstants.HELP)
				, new LoginUserCommand(GlobalConstants.LOGIN)
				, new SignupUserCommand(GlobalConstants.SIGNUP)
			};

			IGame game = new Game(writer, reader, provider, commands);

			try
			{
				game.Init();
				game.Run();
			}
			catch (Exception ex)
			{
				writer.WriteLine(ex.Message);
				throw ex;
			}
		}
	}
}
