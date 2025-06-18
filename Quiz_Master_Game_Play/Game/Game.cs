namespace Quiz_Master_Game_Play.Game
{
	using Common.BaseProvider.Contract;
	using Common.Classes;
	using Common.Enums;
	using Common.IO.Contract;
	using Quiz_Master_Game_Play.Commands.Contract;
	using Quiz_Master_Game_Play.Game.Contract;
	using Quiz_Master_Game_Play.Users;
	using Quiz_Master_Game_Play.Users.Contract;
	using System;
	using System.Collections.Generic;

	using static Common.Constants.GlobalConstants;

	public class Game : IGame
	{
		private IUser user;
		private IReader reader;
		private IWriter writer;
		private IBaseProvider provider;
		private List<ICommand> commands;
		private CommandStruct cmd;

		private uint maxUserId;
		private uint maxQuizId;

		Game(IWriter writer, IReader reader, IBaseProvider provider, List<ICommand> commands)
		{
			this.writer = writer;
			this.reader = reader;
			this.provider = provider;
			this.commands = commands;

			this.MaxUserId = 0;
			this.MaxQuizId = 0;
		}

		public uint MaxUserId
		{
			get => this.maxUserId;
			set => this.maxUserId = value;
		}

		public uint MaxQuizId
		{
			get => this.maxQuizId;
			set => this.maxQuizId = value;
		}

		public void Exit()
		{
			throw new NotImplementedException();
		}

		public void Init()
		{
			this.cmd = new CommandStruct();
			this.user = new User(this.writer, this.reader, this.provider);
			this.LoadConfig();
		}

		public void Run()
		{
			throw new NotImplementedException();
		}

		public void SaveConfig()
		{
			throw new NotImplementedException();
		}

		public void LoadConfig()
		{
			string configString = string.Empty;

			this.provider.Action(ref configString, ProviderOptions.ConfigLoad);

			List<string> v = configString.Split(ROW_DATA_SEPARATOR, StringSplitOptions.RemoveEmptyEntries).ToList();

			this.maxUserId = uint.Parse(v[0]);
			this.maxQuizId = uint.Parse(v[1]);
		}
	}
}
