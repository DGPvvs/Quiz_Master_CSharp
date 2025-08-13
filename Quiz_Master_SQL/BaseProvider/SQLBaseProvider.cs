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
			else if (options == ProviderOptions.NewUserSave)
			{
				str = this.AddNewUser(str, true);
			}
			else if (options == ProviderOptions.ConfigSave)
			{				
				this.SaveConfig(str);
			}
			else if (options == ProviderOptions.UserSave)
			{
				string s = str;
				Console.WriteLine(s);
				this.UpdateUser(s, false);
			}
		}

		private void UpdateUser(string s, bool v)
		{
			throw new NotImplementedException();
		}

		private void SaveConfig(string str)
		{
			List<uint> confData = str.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries)
				.Select(uint.Parse)
				.ToList();
			Task result = SaveConfigAsync(confData);
		}

		private async Task SaveConfigAsync(List<uint> confData)
		{
			ConfigTableDB? config = await this.context.ConfigTablesDB.FirstOrDefaultAsync();
			config.MaxUserId = confData[0];
			config.MaxQuizId = confData[1];
			this.context.ConfigTablesDB.Update(config);
			await this.context.SaveChangesAsync();
		}

		private string AddNewUser(string str, bool v)
		{
			string newUser = str
				.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries)
				.ToList()
				.Last();
			List<string> userData = newUser
				.Split(GlobalConstants.ELEMENT_DATA_SEPARATOR, StringSplitOptions.RemoveEmptyEntries)
				.ToList();

			Task<UserDB> result = AddNewUserToBase(userData);

			string newUserId = result.Result.Id.ToString();

			return newUserId; // Return the new user ID
		}

		private async Task<UserDB> AddNewUserToBase(List<string> userData)
		{
			UserDB newUser = new UserDB()
			{
				UserName = userData[0]
				, Password = uint.Parse(userData[1])
				, UserGameId = uint.Parse(userData[3])
				, UserOptions = (UserOptions)Enum.Parse(typeof(UserOptions), userData[4])
				, FirstName = string.Empty
				, LastName = string.Empty
			};

			this.context.UsersDB.Add(newUser);
			await this.context.SaveChangesAsync();

			return newUser;
		}

		private string UserDataLoad(string s)
		{
			Guid id = Guid.Parse(s);
			Task<UserDTO> result = LoadUserData(id);
			var i = result.Result;
			return $"{result.Result.FirstName}{Environment.NewLine}{result.Result.LastName}";
		}

		private async Task<UserDTO> LoadUserData(Guid id)
		{
			var userId = await this
				.context
				.UsersDB				
				.Where(e => e.Id == id)
				.AsNoTracking()
				.Select(e => e.UserGameId)
				.FirstOrDefaultAsync();

			UserDTO? user = null;

			if (userId <= 10)
			{
				user = await this
				.context
				.UsersDB
				.Where(e => e.Id == id)
				.AsNoTracking()
				.Select(e => new UserDTO
				{
					FirstName = e.FirstName!
					, LastName = e.LastName!
				})
				.FirstOrDefaultAsync();
			}
			else
			{
				throw new NotImplementedException();
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
