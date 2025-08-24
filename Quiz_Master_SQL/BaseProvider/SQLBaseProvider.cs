namespace File_DB.BaseProvider
{
	using Common.BaseProvider.Contract;
	using Common.Constants;
	using Common.Enums;
	using Microsoft.EntityFrameworkCore;
	using Quiz_Master_Game_Play.QuizClass;
	using Quiz_Master_SQL.Data.DTOModels;
	using Quiz_Master_SQL.Data.Models;
	using Quiz_Master_SQL.Quiz_Master_SQL.Data;
	using System.Collections.Generic;
	using System.Text;
	using System.Threading.Tasks;

	public class SQLBaseProvider : IBaseProvider
	{
		private readonly QuizMasterDbContext context;

		public SQLBaseProvider(QuizMasterDbContext _context)
		{
			this.context = _context ?? throw new ArgumentNullException(nameof(_context), "Context cannot be null");
		}

		public void Action(ref string str, uint id, ProviderOptions options)
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

				if (id != 0)
				{
					List<string> usersList = str
						.Split(GlobalConstants.ROW_DATA_SEPARATOR, StringSplitOptions.RemoveEmptyEntries)
						.ToList();

					foreach (string userList in usersList)
					{
						List<string> userData = userList
							.Split(GlobalConstants.ELEMENT_DATA_SEPARATOR, StringSplitOptions.RemoveEmptyEntries)
							.ToList();

						if (uint.Parse(userData[3]) > 10)
						{
							if (this.UpdateUser(userData))
							{
								break;
							}
						}
					}
				}
				else
				{
					str = this.AddNewUser(str);
				}
			}
			else if (options == ProviderOptions.ConfigSave)
			{				
				this.SaveConfig(str);
			}
			else if (options == ProviderOptions.UserSave)
			{
				string s = str;
				//Console.WriteLine(s);
				this.UpdateUser(s, id);
			}
			else if (options == ProviderOptions.QuizFind)
			{
				string s = str;
				str = this.LoadAllQuizzes(s);
			}
			else if (options == ProviderOptions.QuizIndexSave)
			{
				string s = str;
				//quizzes.txt$1 | First Quiz | UserPrime | 1Quiz.txt | NewQuiz | 1 | 0
				str = this.SaveQuizIndex(s, id);
			}
			else if (options == ProviderOptions.QuizSave)
			{
				string s = str;
				str = this.SaveQuiz(s, id);
			}
		}

		private string SaveQuiz(string s, uint id)
		{
			string result = string.Empty;
			string[] quizzesAndFilename = s
				.Split(GlobalConstants.FILENAME_TO_DATA_SEPARATOR, StringSplitOptions.RemoveEmptyEntries)
				.ToArray();
			List<string> quizzes = quizzesAndFilename[1]
				.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries)
				.ToList();

			foreach (string quiz in quizzes)
			{
				QuizIndexDTO quizIndexDTO = new QuizIndexDTO();
				quizIndexDTO.SetElement(quiz);
				if (quizIndexDTO.Id == id)
				{
					Console.WriteLine(id);
					//Task<string> resultAsync = this.UpdateQuizIndexAsync(quizIndexDTO);
					//result = resultAsync.Result;

					break;
				}
			}

			return result;
		}

		private string SaveQuizIndex(string s, uint id)
		{
			string result = string.Empty;
			string[] quizzesAndFilename = s
				.Split(GlobalConstants.FILENAME_TO_DATA_SEPARATOR, StringSplitOptions.RemoveEmptyEntries)
				.ToArray();
			List<string> quizzes = quizzesAndFilename[1]
				.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries)
				.ToList();

			foreach (string quiz in quizzes)
			{				
				QuizIndexDTO quizIndexDTO = new QuizIndexDTO();
				quizIndexDTO.SetElement(quiz);
				if (quizIndexDTO.Id == id)
				{
					Task<string> resultAsync = this.UpdateQuizIndexAsync(quizIndexDTO);
					result = resultAsync.Result;

					break;
				}
			}

			return result;
		}

		private async Task<string> UpdateQuizIndexAsync(QuizIndexDTO quizIndexDTO)
		{
			QuizDB quiz = new QuizDB
			{
				GameId = quizIndexDTO.Id,
				QuizName = quizIndexDTO.QuizName!,
				QuizStatus = quizIndexDTO.QuizStatus,
				NumOfQuestions = quizIndexDTO.NumOfQuestions,
				Likes = quizIndexDTO.Likes
			};

			quiz.UserDb = await this
				.context
				.UsersDB
				.FirstOrDefaultAsync(u => u.UserName == quizIndexDTO.UserName) ?? null!;

			quiz.UserId = (Guid)quiz.UserDb?.Id!;

			await this.context.QuizzesDB.AddAsync(quiz);
			await this.context.SaveChangesAsync();

			return quiz.Id.ToString()!;
		}

		private bool UpdateUser(List<string> userData)
		{		
			Task<bool> userDTO = ChangeUserData(userData);

			bool isChange = userDTO.Result;			

			return isChange;
		}

		private async Task<bool> ChangeUserData(List<string> userData)
		{
			bool result = false;

			UserDB? user = await this
				.context
				.UsersDB
				.FirstOrDefaultAsync(u => u.UserName == userData[0]);

			if (user != null && user.UserOptions != (UserOptions)Enum.Parse(typeof(UserOptions), userData[4]))
			{
				user.UserOptions = (UserOptions)Enum.Parse(typeof(UserOptions), userData[4]);
				result = true;
				this.context.UsersDB.Update(user);
				await this.context.SaveChangesAsync();
			}

			return result;				
		}

		private string LoadAllQuizzes(string s)
		{
			Task<IEnumerable<QuizDB>> quizzes = LoadAllQuizzesFromBase(s);
			IEnumerable<QuizDB> quizList = quizzes.Result;
			StringBuilder sb = new StringBuilder();
			foreach (QuizDB quiz in quizList)
			{
				sb.AppendLine($"{quiz.GameId}|{quiz.QuizName}|{quiz.UserDb.UserGameId}|{quiz.GameId}|{quiz.QuizStatus}|{quiz.NumOfQuestions}|{quiz.Likes}");
			}

			return sb.ToString();
			//1|Quiz1|UserPrime|1Quiz.txt|2|1|0
			//id|quizName|userName|quizFileName|QuizStatus|numOfQuestions|Likes
		}

		private async Task<IEnumerable<QuizDB>> LoadAllQuizzesFromBase(string s)
		{
			IEnumerable<QuizDB> quizzes = await this
				.context
				.QuizzesDB
				.Where(q => q.QuizStatus == QuizStatus.ApprovedQuiz)
				.Select(q => new QuizDB
				{
					Id = q.Id,
					UserId = q.UserId,
					UserDb = q.UserDb,
					QuizStatus = q.QuizStatus,
					NumOfQuestions = q.NumOfQuestions,
					Likes = q.Likes,
					QuizName = q.QuizName,
					GameId = q.GameId
				})
				.ToHashSetAsync();
			
			return quizzes;
		}

		private void UpdateUser(string s, uint id)
		{
			List<string> userData = s
				.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries)
				.ToList();

			List<string> firstName = userData[0]
				.Split(GlobalConstants.FILENAME_SEPARATOR, StringSplitOptions.RemoveEmptyEntries)
				.ToList();
			userData[0] = firstName[1];

			Task<string> result = UpdateUserInBase(userData, id);
		}

		private async Task<string> UpdateUserInBase(List<string> userData, uint id)
		{
			UserDB? user = await this
				.context.UsersDB
				.FirstOrDefaultAsync(u => u.UserGameId == id);

			if (user != null)
			{
				user.FirstName = userData[0];
				user.LastName = userData[1];
				user.Level = uint.Parse(userData[2]);
				user.Points = uint.Parse(userData[3]);
				user.NumberCreatedQuizzes = uint.Parse(userData[4]);
				user.NumberLikedQuizzes = uint.Parse(userData[5]);
				user.NumberFavoriteQuizzes = uint.Parse(userData[6]);
				user.NumberFinishedChallenges = uint.Parse(userData[7]);
				user.NumberSolvedTestQuizzes = uint.Parse(userData[8]);
				user.NumberSolvedNormalQuizzes = uint.Parse(userData[9]);
				user.NumberCreatedQuizzesChallengers = uint.Parse(userData[10]);

				//user.CreatedQuizzes = await context
				//	.QuizzesDB
				//	.Where(q => q.UserId == user.Id)
				//	.ToListAsync();

				int i = 11;
				int j = i;

				for (; i < j + user.NumberCreatedQuizzes; ++i)
				{ 
					QuizDB? quiz = await context
						.QuizzesDB
						.Where(q => q.GameId == uint.Parse(userData[i]))
						.FirstOrDefaultAsync();

					if (quiz != null)
					{
						user.CreatedQuizzes.Add(quiz!);
					}
				}

				j = i;

				for (; i < j + user.NumberLikedQuizzes; ++i)
				{
					//QuizDB? quiz = await context
					//	.QuizzesDB
					//	.Include(lq => lq.LikedQuizzes)
					//	.ThenInclude(q => q.QuizDBs)
					//	.FirstOrDefaultAsync(q => q.GameId == uint.Parse(userData[i]));

					//LikedQuizzeDB? quiz = await context
					//	.LikedQuizzesDB
					//	.Include(lq => lq.QuizDBs)
					//	.Where(q => q.QuizDBs.GameId == uint.Parse(userData[i]))
					//	.FirstOrDefaultAsync();

					Guid quizGameId = await context
						.QuizzesDB
						.Where(q => q.GameId == uint.Parse(userData[i]))
						.Select(q => q.Id)
						.FirstOrDefaultAsync();

					LikedQuizzeDB quiz = new LikedQuizzeDB()
					{
						QuizId = quizGameId,
						UserId = Guid.Parse(user.Id.ToString()!)
					};



					if (quiz != null)
					{
						user.LikedQuizzes.Add(quiz!);
					}
				}

				j = i;

				for (; i < j + user.NumberFavoriteQuizzes; ++i)
				{
					//FavoritedQuizzeDB? quiz = await context
					//	.FavoritedQuizzes
					//	.Include(lq => lq.Quiz)
					//	.Where(q => q.Quiz.GameId == uint.Parse(userData[i]))
					//	.FirstOrDefaultAsync();

					Guid favoriteQuizId = await context
							.QuizzesDB
							.Where(q => q.GameId == uint.Parse(userData[i]))
							.Select(q => q.Id)
							.FirstOrDefaultAsync();

					FavoritedQuizzeDB? quiz = new FavoritedQuizzeDB()
					{
						QuizId = favoriteQuizId,
						UserId = Guid.Parse(user.Id.ToString()!)
					};

					if (quiz != null)
					{
						user.FavoritedQuizzes.Add(quiz!);
					}
				}

				j = i;

				for (; i < j + user.NumberFinishedChallenges; ++i)
				{
					FinishedChallengeDB? challenges = new FinishedChallengeDB()
					{
						ChallengeDescription = userData[i],
						UserId = Guid.Parse(user.Id.ToString()!)
					};

					user.FinishedChallenges.Add(challenges);
				}

				this.context.UsersDB.Update(user);
				await this.context.SaveChangesAsync();
				return $"{user.FirstName}{Environment.NewLine}{user.LastName}";
			}

			return string.Empty; // Return empty string if user not found
			/*0 <firstName>
1 <lastName>
2 <level>
3 <points>
4 <numberCreatedQuizzes>
5 <numberLikedQuizzes>
6 <numberFavoriteQuizzes>
7 <numberFinishedChallenges>
8 <numberSolvedTestQuizzes>
9 <numberSolvedNormalQuizzes>
10 <numberCreatedQuizzesChallengers>
<numberCreatedQuizzes lines of created quizes in format>:
<quizId>#<quizName>
.
.
.
<numberLikedQuizzes lines of liked quizzes in format>:
<quizId>
.
.
.
<numberFavoriteQuizzes lines of favorite quizzes in format>:
<quizId>
.
.
.
<numberFinishedChallenges lines of finished challenges in format>
<<data>|<text challenges>>
.
.
.*/
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

		private string AddNewUser(string str)
		{
			string newUser = str
				.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries)
				.ToList()
				.Last();
			List<string> userData = newUser
				.Split(GlobalConstants.ELEMENT_DATA_SEPARATOR, StringSplitOptions.RemoveEmptyEntries)
				.ToList();

			Task<UserDB> result = AddNewUserToBase(userData);

			string newUserId = result.Result.Id.ToString()!;

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
			UserDTO user = result.Result;
			if (user.UserGameId <=10)
			{
				return $"{result.Result.FirstName}{Environment.NewLine}{result.Result.LastName}";
			}
			else
			{
				StringBuilder sb = new StringBuilder();

				sb.AppendLine(user.FirstName); //0
				sb.AppendLine(user.LastName); //1
				sb.AppendLine(user.Level.ToString()); //2
				sb.AppendLine(user.Points.ToString()); //3
				sb.AppendLine(user.NumberCreatedQuizzes.ToString()); //4
				sb.AppendLine(user.NumberLikedQuizzes.ToString()); //5
				sb.AppendLine(user.NumberFavoriteQuizzes.ToString()); //6
				sb.AppendLine(user.NumberFinishedChallenges.ToString()); //7
				sb.AppendLine(user.NumberSolvedTestQuizzes.ToString()); //8
				sb.AppendLine(user.NumberSolvedNormalQuizzes.ToString()); //9
				sb.AppendLine(user.NumberCreatedQuizzesChallengers.ToString()); //10

				foreach (QuizDB quiz in user.CreatedQuizzes)
				{
					sb.AppendLine($"{quiz.GameId}#{quiz.QuizName}");
				}
				foreach (LikedQuizzeDB likedQuiz in user.LikedQuizzes)
				{
					sb.AppendLine($"{likedQuiz.QuizId}");
				}
				foreach (FavoritedQuizzeDB favoritedQuiz in user.FavoritedQuizzes)
				{
					sb.AppendLine($"{favoritedQuiz.QuizId}");
				}
				foreach (FinishedChallengeDB finishedChallenge in user.FinishedChallenges)
				{
					sb.AppendLine(finishedChallenge.ChallengeDescription);
				}
				return sb.ToString();

				/*0 <firstName>
1 <lastName>
2 <level>
3 <points>
4 <numberCreatedQuizzes>
5 <numberLikedQuizzes>
6 <numberFavoriteQuizzes>
7 <numberFinishedChallenges>
8 <numberSolvedTestQuizzes>
9 <numberSolvedNormalQuizzes>
10 <numberCreatedQuizzesChallengers>
<numberCreatedQuizzes lines of created quizes in format>:
<quizId>#<quizName>
.
.
.
<numberLikedQuizzes lines of liked quizzes in format>:
<quizId>
.
.
.
<numberFavoriteQuizzes lines of favorite quizzes in format>:
<quizId>
.
.
.
<numberFinishedChallenges lines of finished challenges in format>
<<data>|<text challenges>>
.
.
.*/
			}
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
					, Level = (uint)e.Level!
					, Points = (uint)e.Points!
					, NumberCreatedQuizzes = (uint)e.NumberCreatedQuizzes!
					, NumberLikedQuizzes = (uint)e.NumberLikedQuizzes!
					, NumberFavoriteQuizzes = (uint)e.NumberFavoriteQuizzes!
					, NumberFinishedChallenges = (uint)e.NumberFinishedChallenges!
					, NumberSolvedTestQuizzes = (uint)e.NumberSolvedTestQuizzes!
					, NumberSolvedNormalQuizzes = (uint)e.NumberSolvedNormalQuizzes!
					, NumberCreatedQuizzesChallengers = (uint)e.NumberCreatedQuizzesChallengers!
				})
				.FirstOrDefaultAsync();

				if (user != null && user.NumberCreatedQuizzes > 0)
				{
					user.CreatedQuizzes = await this
						.context
						.QuizzesDB
						.Where(q => q.UserId == user.Id)
						.ToHashSetAsync();					
				}

				if (user != null && user.NumberLikedQuizzes > 0)
				{
					user.LikedQuizzes = await this
						.context
						.LikedQuizzesDB
						.Where(lq => lq.UserId == user.Id)
						.ToHashSetAsync();
				}

				//user.LikedQuizzes = await this
				//		.context
				//		.LikedQuizzesDB
				//		.Where(lq => lq.UserId == user.Id)
				//		.Select(lq => new LikedQuizzeDB
				//		{
				//			UserDBs = lq.UserDBs
				//			, QuizId = lq.QuizId
				//			, UserId = lq.UserId
				//			, QuizDBs = lq.QuizDBs
				//		})
				//		.ToListAsync();

				if (user != null && user.NumberFavoriteQuizzes > 0)
				{
					user.FavoritedQuizzes = await this
						.context
						.FavoritedQuizzes
						.Where(lq => lq.UserId == user.Id)
						.ToHashSetAsync();
				}

				//user.FavoritedQuizzes = await this
				//	.context
				//	.FavoritedQuizzes
				//	.Where(fq => fq.UserId == user.Id)
				//	.Select(fq => new FavoritedQuizzeDB
				//	{
				//		User = fq.User
				//		, QuizId = fq.QuizId
				//		, UserId = fq.UserId
				//		, Quiz = fq.Quiz
				//	})
				//	.ToListAsync();

				if (user != null && user.NumberFinishedChallenges > 0)
				{
					user.FinishedChallenges = await this
						.context
						.FinishedChallengesDB
						.Where(fc => fc.UserId == user.Id)
						.Select(fc => new FinishedChallengeDB
						{
							UserDBs = fc.UserDBs
							,
							ChallengeDescription = fc.ChallengeDescription
						})
						.ToHashSetAsync();
				}

				//user.FinishedChallenges = await this
				//	.context
				//	.FinishedChallengesDB
				//	.Where(fc => fc.UserId == user.Id)
				//	.ToListAsync();
			}

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
