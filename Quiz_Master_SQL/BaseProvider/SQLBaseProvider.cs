namespace File_DB.BaseProvider
{
	using Common.BaseProvider.Contract;
	using Common.Constants;
	using Common.Enums;
	using Microsoft.EntityFrameworkCore;
	using Quiz_Master_SQL.Data.DTOModels;
	using Quiz_Master_SQL.Data.Models;
	using Quiz_Master_SQL.Quiz_Master_SQL.Data;
	using System.Text;
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
			else if (options == ProviderOptions.UserFind)
			{
				string s = GlobalConstants.USERS_FILE_NAME;
				str = this.FindUser(s);
			}
			else if (options == ProviderOptions.UserLoad)
			{
				string s = str;
				str = this.UserDataLoad(s);
			}
		}

		private string UserDataLoad(string s)
		{
			Guid id = Guid.Parse(s);
			Task<UserDTO> result = LoadUserData(id);
			var i = result.Result;
			Console.WriteLine($"{i.UserName} {i.FirstName} {i.LastName} {i.UserGameId}");
			Console.WriteLine();
			;
			throw new NotImplementedException();
		}

		private async Task<UserDTO> LoadUserData(Guid id)
		{
			var userId = await this
				.context
				.UsersDB				
				.Where(e => e.Id == id)
				.Select(e => e.UserGameId)
				.FirstOrDefaultAsync();

			UserDTO? user = null;

			if (userId <= 10)
			{
				user = await this
				.context
				.UsersDB
				.Where(e => e.Id == id)
				.Select(e => new UserDTO
				{
					Id = Guid.Parse(e.Id.ToString()!)
					, UserName = e.UserName!
					, Password = e.Password
					, UserOptions = e.UserOptions
					, UserGameId = e.UserGameId
					, FirstName = e.FirstName!
					, LastName = e.LastName!
				})
				.FirstOrDefaultAsync();
			}

				//var user = await this
				//.context
				//.UsersDB
				//.Where(e => e.Id == id)
				//.Select(e => new UserDTO
				//{
				//	Id = Guid.Parse(e.Id.ToString()!)
				//	, UserName = e.UserName!
				//	, Password = e.Password
				//	, UserOptions = e.UserOptions
				//	, UserGameId = e.UserGameId
				//	, FirstName = e.FirstName!
				//	, LastName = e.LastName!
				//	, Level = e.Level
				//	, Points = e.Points
				//	, NumberCreatedQuizzes = e.NumberCreatedQuizzes
				//	, NumberLikedQuizzes = e.NumberLikedQuizzes
				//	, NumberFavoriteQuizzes = e.NumberFavoriteQuizzes
				//	, NumberFinishedChallenges = e.NumberFinishedChallenges
				//	, NumberSolvedTestQuizzes = e.NumberSolvedTestQuizzes
				//	, NumberSolvedNormalQuizzes = e.NumberSolvedNormalQuizzes
				//	, NumberCreatedQuizzesChallengers = e.NumberCreatedQuizzesChallengers
				//})
				//.FirstOrDefaultAsync();

			return user ?? throw new InvalidOperationException("User not found or invalid UserGameId.");
		}

		private string FindUser(string s)
		{
			Task<IEnumerable<UsersHeaderDTO>> result = LoadUsersHeader();

			StringBuilder sb = new StringBuilder();

			foreach (UsersHeaderDTO user in result.Result)
			{
				sb.AppendLine($"{user.UserName} {user.Password} {user.Id} {user.UserGameId} {user.UserOptions}");
			}

			return sb.ToString(); // Return the string representation of the
		}

		private async Task<IEnumerable<UsersHeaderDTO>> LoadUsersHeader()
		{
			IEnumerable<UsersHeaderDTO> users = await this
				.context
				.UsersDB
				.Select(e => new UsersHeaderDTO
				{
					Id = Guid.Parse(e.Id.ToString()!)
					, UserName = e.UserName!
					, Password = e.Password
					, UserOptions = e.UserOptions
					, UserGameId = e.UserGameId
				})
				.ToListAsync();

			return users;
		}

		private string LoadConfig(string s)
		{
			Task<string> i = LoadConfigAsync(s);
			s = i.Result; // Wait for the async task to complete
			return s;
		}

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
