namespace Quiz_Master_Game_Play.Users.Contract
{
	using Common.BaseProvider.Contract;
	using Common.Classes;
	using Common.Enums;
	using Common.IO.Contract;

	public interface IUser
	{
		string? Name { get; }

		string? FirstName { set; }

		string? LastName { set; }

		string? UserName { get; set; }

		uint Id { get; set; }

		string? FileName { get; set; }

		IWriter Writer { get; }

		IReader Reader { get; }

		IBaseProvider Provider { get; }

		bool IsHasLog { get; }

		string BuildUserData();

		uint Hash(string str);

		UserOptions FindUserData(UserStruct us, bool exsist);

		string AllUsers(string users);

		void SetUpUserData(UserStruct us, List<string> list, UserOptions uo);

		void SaveData();
	}
}
