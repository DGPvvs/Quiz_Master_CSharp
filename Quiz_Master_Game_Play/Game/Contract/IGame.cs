namespace Quiz_Master_Game_Play.Game.Contract
{
	using Common.BaseProvider.Contract;
	using Common.Classes;
	using Common.IO.Contract;
	using Quiz_Master_Game_Play.Users.Contract;

	public interface IGame
	{
		IUser User { get; set; }

		IWriter Writer { get; }

		IReader Reader { get; }

		IBaseProvider Provider { get; }

		Invoker Invoker { get; }

		CommandStruct Cmd { get; }

		void Init();

		void Run();

		void Exit();

		uint MaxUserId { get; set; }

		uint MaxQuizId { get; set; }

		void SaveConfig();

		void LoadUser(UserStruct us);
	}
}
