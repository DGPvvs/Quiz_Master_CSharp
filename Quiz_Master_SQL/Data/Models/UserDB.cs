namespace Quiz_Master_SQL.Data.Models
{
	using Common.Enums;
	using System.ComponentModel.DataAnnotations;

	public class UserDB
	{
		public UserDB()
		{
			this.Id = Guid.NewGuid();
			this.CreatedQuizzes = new HashSet<QuizDB>();
			this.FinishedChallenges = new HashSet<FinishedChallengeDB>();
			this.LikedQuizzes = new HashSet<LikedQuizzeDB>();
			this.FavoritedQuizzes = new HashSet<FavoritedQuizzeDB>();
			this.Messages = new HashSet<MessagesDB>();
		}

		[Key]
		public Guid? Id { get; set; } = null!;		

		[Required]
		[MaxLength(50)]
		public string? UserName { get; set; }

		[Required]
		public uint Password { get; set; }

		[Required]
		public UserOptions UserOptions { get; set; }

		[Required]
		public uint UserGameId { get; set; }

		[Required]
		[MaxLength(50)]
		public string? FirstName { get; set; }

		[Required]
		[MaxLength(50)]
		public string? LastName { get; set; }

		public uint? Level { get; set; }

		public uint? Points { get; set; }

		public uint? NumberCreatedQuizzes { get; set; }

		public uint? NumberLikedQuizzes { get; set; }

		public uint? NumberFavoriteQuizzes { get; set; }

		public uint? NumberFinishedChallenges { get; set; }

		public uint? NumberSolvedTestQuizzes { get; set; }

		public uint? NumberSolvedNormalQuizzes { get; set; }

		public uint? NumberCreatedQuizzesChallengers { get; set; }

		public virtual ICollection<FinishedChallengeDB> FinishedChallenges { get; set; }

		public virtual ICollection<QuizDB> CreatedQuizzes { get; set; }		

		public virtual ICollection<LikedQuizzeDB> LikedQuizzes { get; set; }

		public virtual ICollection<FavoritedQuizzeDB> FavoritedQuizzes { get; set; }
		
		public virtual ICollection<MessagesDB> Messages { get; set; }

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
