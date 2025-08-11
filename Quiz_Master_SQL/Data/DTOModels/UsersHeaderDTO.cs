namespace Quiz_Master_SQL.Data.DTOModels
{
	using Common.Enums;

	public class UsersHeaderDTO
	{
		public Guid Id { get; set; }

		public string UserName { get; set; } = null!;

		public uint Password { get; set; }

		public UserOptions UserOptions { get; set; }

		public uint UserGameId { get; set; }
	}
}
