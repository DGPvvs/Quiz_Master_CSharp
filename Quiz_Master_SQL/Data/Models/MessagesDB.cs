namespace Quiz_Master_SQL.Data.Models
{
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;

	public class MessagesDB
	{
		public MessagesDB()
		{
			this.Id = Guid.NewGuid();
		}

		[Key]
		public Guid Id { get; set; }

		public string Message { get; set; } = null!;

		[ForeignKey(nameof(this.UserDB))]
		public Guid UserId { get; set; }

		public virtual UserDB UserDB { get; set; } = null!;
	}
}
