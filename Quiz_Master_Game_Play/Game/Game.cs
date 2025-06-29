namespace Quiz_Master_Game_Play.Game
{
	using Common.BaseProvider.Contract;
	using Common.Classes;
	using Common.Constants;
	using Common.Enums;
	using Common.IO.Contract;
	using Quiz_Master_Game_Play.Commands.Contract;
	using Quiz_Master_Game_Play.Game.Contract;
	using Quiz_Master_Game_Play.Users;
	using Quiz_Master_Game_Play.Users.Contract;
	using System;
	using System.Collections.Generic;

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

		public Game(IWriter writer, IReader reader, IBaseProvider provider, List<ICommand> commands)
		{
			this.writer = writer;
			this.reader = reader;
			this.provider = provider;
			this.commands = commands;

			this.MaxUserId = 0;
			this.MaxQuizId = 0;
			this.user = null!;
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

		public IUser User
		{
			get => this.user;
			set => this.user = value;
		}

		public IWriter Writer => this.writer;

		public IReader Reader => this.reader;

		public IBaseProvider Provider => this.provider;

		public List<ICommand> Commands => this.commands;

		public CommandStruct Cmd => this.cmd;

		public void Exit()
		{
			if (this.user.IsHasLog)
			{
				//this->LogoutUser();
			}

			this.SaveConfig();
		}

		public void Init()
		{
			this.cmd = new CommandStruct();
			this.user = new User(this.writer, this.reader, this.provider);
			this.LoadConfig();
		}

		public void Run()
		{
			this.GameLoop();
		}

		public void SaveConfig()
		{
			string configString = $"{this.MaxUserId}{Environment.NewLine}{this.MaxQuizId}";
			this.provider.Action(ref configString, ProviderOptions.ConfigSave);
		}

		public void LoadUser(UserStruct us)
		{
			if (us.Id <= 10)
			{
				this.User = new Admin(this.Writer, this.Reader, this.Provider);
			}
			else
			{
				this.User = new Player(this.Writer, this.Reader, this.Provider, this);
			}

			List<string> v = new List<string>();

			this.User.SetUpUserData(us, ref v, UserOptions.Empty);
		}

		private void LoadConfig()
		{
			string configString = string.Empty;
			this.provider.Action(ref configString, ProviderOptions.ConfigLoad);




			List<string> v = configString.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries).ToList();

			this.MaxUserId = uint.Parse(v[0]);
			this.MaxQuizId = uint.Parse(v[1]);
		}

		private void GameLoop()
		{
			bool isLoopExit = false;

			while (!isLoopExit)
			{
				this.SetCommandStruct();

				if (this.Cmd.Command == GlobalConstants.EXIT)
				{
					isLoopExit = true;
					this.Cmd.Command = GlobalConstants.LOGOUT;
				}

				foreach (var command in this.commands)
				{
					if (command.Execute(this))
					{
						break;
					}
				}				
			}
		}

		private void SetCommandStruct()
		{
			this.Cmd.Command = string.Empty;

			this.Writer.Write(GlobalConstants.PROMPT_STRING);

			string s = this.Reader.ReadLine();
			List<string> commandLine = s.Split(GlobalConstants.ELEMENT_DATA_SEPARATOR, StringSplitOptions.RemoveEmptyEntries).ToList();

			this.Cmd.CommandLine = s;
			this.Cmd.ParamRange = (uint)commandLine.Count;

			if (commandLine.Count > 0)
			{
				this.Cmd.Command = commandLine[0];

				for (int i = commandLine.Count; i < 6; i++)
				{
					commandLine.Add(string.Empty);
				}

				this.Cmd.Param1 = commandLine[1];
				this.Cmd.Param2 = commandLine[2];
				this.Cmd.Param3 = commandLine[3];
				this.Cmd.Param4 = commandLine[4];
				this.Cmd.Param5 = commandLine[5];
			}
		}

	}
}
