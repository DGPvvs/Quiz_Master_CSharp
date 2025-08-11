namespace Quiz_Master_SQL.Data.DTOModels
{
	using Common.Enums;
	using global::Quiz_Master_SQL.Data.Models;

	public class UserDTO
	{
		public Guid Id { get; set; }

		public string UserName { get; set; } = null!;

		public uint Password { get; set; }

		public UserOptions UserOptions { get; set; }

		public uint UserGameId { get; set; }

		public string FirstName { get; set; } = null!;

		public string LastName { get; set; } = null!;

		public uint Level { get; set; }

		public uint Points { get; set; }

		public uint NumberCreatedQuizzes { get; set; }

		public uint NumberLikedQuizzes { get; set; }

		public uint NumberFavoriteQuizzes { get; set; }

		public uint NumberFinishedChallenges { get; set; }

		public uint NumberSolvedTestQuizzes { get; set; }

		public uint NumberSolvedNormalQuizzes { get; set; }

		public uint NumberCreatedQuizzesChallengers { get; set; }

		public ICollection<FinishedChallengeDB> FinishedChallenges { get; set; }

		public ICollection<QuizDB> CreatedQuizzes { get; set; }

		public ICollection<LikedQuizzeDB> LikedQuizzes { get; set; }

		public ICollection<FavoritedQuizzeDB> FavoritedQuizzes { get; set; }

		public ICollection<MessagesDB> Messages { get; set; }
	}
}
