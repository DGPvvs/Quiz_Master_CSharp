namespace Quiz_Master_Game_Play.Game
{
	using File_DB.Contract;
	using Quiz_Master_Game_Play.Game.Contract;
	using System;

	public class Game : IGame
	{
		IUser* user;
		IReader* reader;
		IWriter* writer;
		IBaseProvider* provider;
		CommandStruct* command;

		unsigned int maxUserId;
		unsigned int maxQuizId;
		public void Exit()
		{
			throw new NotImplementedException();
		}

		public uint GetMaxQuizId()
		{
			throw new NotImplementedException();
		}

		public void Init()
		{
			throw new NotImplementedException();
		}

		public void Run()
		{
			throw new NotImplementedException();
		}

		public void SaveConfig()
		{
			throw new NotImplementedException();
		}

		public void SetMaxQuizId(uint quizId)
		{
			throw new NotImplementedException();
		}
	}
}
