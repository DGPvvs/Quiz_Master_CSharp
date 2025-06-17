namespace Quiz_Master_Game_Play.Game.Contract
{
	public interface IGame
	{
		void Init();

		void Run();

		void Exit();

		uint MaxUserId { get; set; }

		uint MaxQuizId { get; set; }

		void SaveConfig();
	}
}
