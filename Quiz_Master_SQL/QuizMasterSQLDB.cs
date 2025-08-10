namespace Quiz_Master_SQL
{
	using File_DB.BaseProvider;
	using global::Quiz_Master_SQL.Data.Configuration.EntitySeed.SeedData;
	using global::Quiz_Master_SQL.IO;
	using global::Quiz_Master_SQL.Quiz_Master_SQL.Data;
	using Quiz_Master_Game_Play.Commands;
	using Quiz_Master_Game_Play.Commands.Contract;
	using Quiz_Master_Game_Play.Game;
	using Quiz_Master_Game_Play.Game.Contract;

	internal class QuizMasterSQLDB
	{
		static void Main(string[] args)
		{
			ConsoleWriter writer = new ConsoleWriter();
			ConsoleReader reader = new ConsoleReader();

			QuizMasterDbContext context = new QuizMasterDbContext();

			//context.AddRange(new SeedsData().UserDBSeeder());
			//context.SaveChanges();

			//QuizMasterDbContext context = new QuizMasterDbContext(true);


			SQLBaseProvider provider = new SQLBaseProvider(context);

			HashSet<ICommand> commands = new SetCommands().Commands.ToHashSet();

			IGame game = new Game(writer, reader, provider, commands);

			game.Init();
			game.Run();

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
