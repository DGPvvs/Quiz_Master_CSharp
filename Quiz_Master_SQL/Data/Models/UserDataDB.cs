namespace File_DB.Data.Models
{
	using Quiz_Master_SQL.Data.Models;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;

	public class UserDataDB
	{
		public UserDataDB()
		{
			this.Id = Guid.NewGuid();
		}

		[Key]
		public Guid Id { get; set; }


		[ForeignKey(nameof(this.UserDB))]
		public Guid UserId { get; set; }

		public virtual UserDB UserDB { get; set; } = null!;

		public uint? Level { get; set; }

		public uint? Points { get; set; }

		public uint? NumberCreatedQuizzes { get; set; }

		public uint? NumberLikedQuizzes { get; set; }

		public uint? NumberFavoriteQuizzes { get; set; }

		public uint? NumberFinishedChallenges { get; set; }

		public uint? NumberSolvedTestQuizzes { get; set; }

		public uint? NumberSolvedNormalQuizzes { get; set; }

		public uint? NumberCreatedQuizzesChallengers { get; set; }

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
