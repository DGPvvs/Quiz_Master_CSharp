namespace Quiz_Master_SQL.Data.Models
{
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;

	public class FinishedChallengeDB
	{
		public FinishedChallengeDB()
		{
			this.Id = Guid.NewGuid();
		}

		[Key]
		public Guid Id { get; set; }

		[Required]
		public string ChallengeDescription { get; set; } = null!;

		[ForeignKey(nameof(this.UserDBs))]
		public Guid UserId { get; set; }

		public virtual UserDB UserDBs { get; set; } = null!;
	}
}
