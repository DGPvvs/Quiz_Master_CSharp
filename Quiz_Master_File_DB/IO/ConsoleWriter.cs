namespace Quiz_Master_File_DB.IO
{
	using Common.IO.Contract;

	public class ConsoleWriter : IWriter
	{
		public void Write(string str)
		{
			Console.Write(str);
		}

		public void WriteLine(string str)
		{
			Console.WriteLine(str);
		}
	}
}
