namespace File_DB.BaseProvider
{
	using Common.BaseProvider.Contract;
	using Common.Enums;
	using Quiz_Master_SQL.Quiz_Master_SQL.Data;

	public class SQLBaseProvider : IBaseProvider
	{
		private readonly QuizMasterDbContext context;

		public SQLBaseProvider(QuizMasterDbContext _context)
		{
			this.context = _context ?? throw new ArgumentNullException(nameof(_context), "Context cannot be null");
		}

		public void Action(ref string str, ProviderOptions options)
		{
			//throw new NotImplementedException();
		}
	}
}
