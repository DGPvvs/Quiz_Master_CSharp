namespace Quiz_Master_SQL.Data.Models
{
	using Common.Enums;
	using File_DB.Data.Models;
	using System.ComponentModel.DataAnnotations;

	public class UserDB
	{
		public UserDB()
		{
			this.Id = Guid.NewGuid();
			this.CreatedQuizzes = new HashSet<QuizDB>();
		}

		[Key]
		public Guid? Id { get; set; } = null!;

		[Required]
		public uint UserGameId { get; set; }

		[Required]
		[MaxLength(50)]
		public string? FirstName { get; set; }

		[Required]
		[MaxLength(50)]
		public string? LastName { get; set; }

		[Required]
		[MaxLength(50)]
		public string? UserName { get; set; }

		[Required]
		public uint Password { get; set; }

		[Required]
		public UserOptions UserOptions { get; set; }

		public virtual UserDataDB UserDataDB { get; set; } = null!;

		public virtual ICollection<QuizDB> CreatedQuizzes { get; set; }
	}
}
