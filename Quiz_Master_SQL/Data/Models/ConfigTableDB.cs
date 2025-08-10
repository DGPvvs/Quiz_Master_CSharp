namespace Quiz_Master_SQL.Data.Models
{
	using System.ComponentModel.DataAnnotations;

	public class ConfigTableDB
	{
		public ConfigTableDB()
		{
			this.Id = Guid.NewGuid();
		}

		[Key]
		public Guid Id { get; set; }

		public uint MaxUserId { get; set; }

		public uint MaxQuizId { get; set; }
	}
}
