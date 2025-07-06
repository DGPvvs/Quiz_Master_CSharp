namespace Quiz_Master_Game_Play.Users.Contract
{
	using Common.BaseProvider.Contract;
	using Common.Classes;
	using Common.Enums;
	using Common.IO.Contract;
	using Quiz_Master_Game_Play.MessageClass;
	using Quiz_Master_Game_Play.QuizClass;

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

		void SetUpUserData(UserStruct us, ref List<string> list, UserOptions uo);

		void SaveData();

		void Help();

		int FindUserIndex(UserStruct us, List<string> usersVec);

		Quiz Quiz { get; }

		Message Message { get; }

		bool GenerateReason(CommandStruct cmdStr, ref string? reason);		

		public uint Password { get; set; }

		public uint NumberCreatedQuizzes { get; }

		public uint NumberSolvedTestQuizzes { get; }
		
		public uint NumberSolvedNormalQuizzes { get; }
		
		public uint NumberLikedQuizzes { get; set; }

		public List<string> ListFinishedChallenges { get; }

		public List<uint> ListLikedQuizzes { get; }
	}
}
