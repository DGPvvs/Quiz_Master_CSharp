namespace File_DB.BaseProvider
{
	using Common.BaseProvider.Contract;
	using Common.Constants;
	using Common.Enums;
	using Microsoft.EntityFrameworkCore;
	using Quiz_Master_SQL.Data.Models;
	using Quiz_Master_SQL.Quiz_Master_SQL.Data;
	using System.Threading.Tasks;

	public class SQLBaseProvider : IBaseProvider
	{
		private readonly QuizMasterDbContext context;

		public SQLBaseProvider(QuizMasterDbContext _context)
		{
			this.context = _context ?? throw new ArgumentNullException(nameof(_context), "Context cannot be null");
		}

		public void Action(ref string str, ProviderOptions options)
		{
			if (options == ProviderOptions.ConfigLoad)
			{
				string s = GlobalConstants.CONFIG_FILE_NAME;
				str = this.LoadConfig(s);
			}
		}

		private string LoadConfig(string s) => LoadConfigAsync(s).ToString();

		private async Task<string> LoadConfigAsync(string s)
		{
			ConfigTableDB? i = await this
				.context
				.ConfigTablesDB
				.Take(1)
				.FirstOrDefaultAsync();

			return $"{i.MaxUserId}{Environment.NewLine}{i.MaxQuizId}";
		}
	}
}
