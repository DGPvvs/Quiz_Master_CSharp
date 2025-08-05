namespace Quiz_Master_SQL
{
	using global::Quiz_Master_SQL.IO;
	using global::Quiz_Master_SQL.Quiz_Master_SQL.Data;

	internal class QuizMasterSQLDB
	{
		static void Main(string[] args)
		{
			ConsoleWriter writer = new ConsoleWriter();
			ConsoleReader reader = new ConsoleReader();

			QuizMasterDbContext context = new QuizMasterDbContext();


			//SQLBaseProvider provider = new SQLBaseProvider(context);

			//HashSet<ICommand> commands = new SetCommands().Commands.ToHashSet();

			//IGame game = new Game(writer, reader, provider, commands);

			//game.Init();
			//game.Run();

			//try
			//{
			//	//game.Init();
			//	//game.Run();
			//}
			//catch (Exception ex)
			//{
			//	writer.WriteLine(ex.Message);
			//	throw ex;
			//}
		}
	}
}
