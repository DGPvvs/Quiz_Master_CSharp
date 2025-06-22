namespace Quiz_Master_File_DB.IO
{
	using Common.IO.Contract;

	public class ConsoleReader : IReader
	{
		public string ReadLine() => Console.ReadLine()!;
	}
}
