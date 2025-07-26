namespace Quiz_Master_File_DB
{
	using Quiz_Master_File_DB.BaseProvider;
	using Quiz_Master_File_DB.IO;
	using Quiz_Master_Game_Play.Commands;
	using Quiz_Master_Game_Play.Commands.Contract;
	using Quiz_Master_Game_Play.Game;
	using Quiz_Master_Game_Play.Game.Contract;

	public class QuizMasterFileDB
	{
		static void Main(string[] args)
		{
			ConsoleWriter writer = new ConsoleWriter();
			ConsoleReader reader = new ConsoleReader();
			FileBaseProvider provider = new FileBaseProvider();

			HashSet<ICommand> commands = new SetCommands().Commands.ToHashSet();

			IGame game = new Game(writer, reader, provider, commands);

			//game.Init();
			//game.Run();

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
