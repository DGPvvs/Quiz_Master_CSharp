namespace Quiz_Master_SQL.Data.Configuration.EntitySeed.SeedData
{
	using Common.Enums;
	using global::Quiz_Master_SQL.Data.Models;

	public class SeedsData
	{
		public IEnumerable<ConfigTableDB> ConfigTableDBSeeder() =>
			new List<ConfigTableDB>()
			{
				new ConfigTableDB
				{
					Id = Guid.NewGuid(),
					MaxUserId = 10,
					MaxQuizId = 0,
				}
			};

		public IEnumerable<UserDB> UserDBSeeder() =>
			new List<UserDB>()
			{
				new UserDB()
				{
					Id = Guid.NewGuid(),
					UserGameId = 1,
					UserName = "admin1",
					Password = 708698343,
					UserOptions = UserOptions.OK,
					FirstName = "Admin",
					LastName = "One"
				}

				, new UserDB()
				{
					Id = Guid.NewGuid(),
					UserGameId = 2,
					UserName = "admin2",
					Password = 708698342,
					UserOptions = UserOptions.OK,
					FirstName = "Admin",
					LastName = "Two"
				}

				, new UserDB()
				{
					Id = Guid.NewGuid(),
					UserGameId = 3,
					UserName = "admin3",
					Password = 708698337,
					UserOptions = UserOptions.OK,
					FirstName = "Admin",
					LastName = "Three"
				}

				, new UserDB()
				{
					Id = Guid.NewGuid(),
					UserGameId = 4,
					UserName = "admin4",
					Password = 708698336,
					UserOptions = UserOptions.OK,
					FirstName = "Admin",
					LastName = "Four"
				}

				, new UserDB()
				{
					Id = Guid.NewGuid(),
					UserGameId = 5,
					UserName = "admin5",
					Password = 708698339,
					UserOptions = UserOptions.OK,
					FirstName = "Admin",
					LastName = "Five"
				}

				, new UserDB()
				{
					Id = Guid.NewGuid(),
					UserGameId = 6,
					UserName = "admin6",
					Password = 708698338,
					UserOptions = UserOptions.OK,
					FirstName = "Admin",
					LastName = "Six"
				}

				, new UserDB()
				{
					Id = Guid.NewGuid(),
					UserGameId = 7,
					UserName = "admin7",
					Password = 708698349,
					UserOptions = UserOptions.OK,
					FirstName = "Admin",
					LastName = "Seven"
				}

				, new UserDB()
				{
					Id = Guid.NewGuid(),
					UserGameId = 8,
					UserName = "admin8",
					Password = 708698348,
					UserOptions = UserOptions.OK,
					FirstName = "Admin",
					LastName = "Eight"
				}

				, new UserDB()
				{
					Id = Guid.NewGuid(),
					UserGameId = 9,
					UserName = "admin9",
					Password = 708698351,
					UserOptions = UserOptions.OK,
					FirstName = "Admin",
					LastName = "Nine"
				}

				, new UserDB()
				{
					Id = Guid.NewGuid(),
					UserGameId = 10,
					UserName = "admin10",
					Password = 2021029294,
					UserOptions = UserOptions.OK,
					FirstName = "Admin",
					LastName = "Ten"
				}
			};
	}
}
