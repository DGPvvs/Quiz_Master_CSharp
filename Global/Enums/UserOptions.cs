namespace Common.Enums
{
	[Flags]
	public enum UserOptions
	{
		Empty = 0b00000000,
		OK = 0b00000001,
		NotFound = 0b00000010,
		WrongPassword = 0b00000100,
		AlreadyExisist = 0b00001000,
		Ban = 0b00010000,
		NewUserCreated = 0b00100000,
	}
}
