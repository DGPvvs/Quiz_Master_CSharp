namespace Quiz_Master_Game_Play.Game.Contract
{
	public interface IGame
	{
		public void Init();

		public void Run();

		public void Exit();

		public uint GetMaxQuizId();

		public void SetMaxQuizId(uint quizId);

		public void SaveConfig();
	}
}
