namespace Quiz_Master_SQL.IO
{
	using Common.IO.Contract;

	public class ConsoleReader : IReader
	{
		public string ReadLine() => Console.ReadLine()!;
	}
}
