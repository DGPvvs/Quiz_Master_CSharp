namespace Quiz_Master_Game_Play.Users
{
	using Common.BaseProvider.Contract;
	using Common.Classes;
	using Common.Constants;
	using Common.Enums;
	using Common.IO.Contract;
	using Quiz_Master_Game_Play.MessageClass;
	using Quiz_Master_Game_Play.QuizClass;
	using Quiz_Master_Game_Play.Users.Contract;
	using System.Collections.Generic;

	public class User : IUser
	{
		private uint id;
		private string firstName;
		private string lastName;
		private string userName;
		private string fileName;
		private uint password;
		private bool isHasLogin;
		private IWriter writer;
		private IReader reader;
		private IBaseProvider provider;
		private Quiz quiz;
		private Message message;

		public User(IWriter writer, IReader reader, IBaseProvider provider)
		{
			this.writer = writer;
			this.reader = reader;
			this.provider = provider;
			this.isHasLogin = false;
			this.id = 0;
			this.password = 0;

			this.quiz = new Quiz(this.writer, this.reader, this.provider, string.Empty, string.Empty, 0, 0, 0);
			this.message = new Message(this.provider);
		}

		public bool IsHasLog
		{
			get => this.isHasLogin;
			protected set => this.isHasLogin = value;
		}

		protected uint Password
		{
			get => this.password;
			set => this.password = value;
		}

		public IWriter Writer => this.writer;

		public IReader Reader => this.reader;

		public IBaseProvider Provider => this.provider;

		public string Name => $"{this.firstName} {this.lastName}";

		public string? FirstName
		{
			set => this.firstName = value!;
		}

		public string? LastName
		{
			set => this.lastName = value!;
		}

		public string? UserName
		{
			get => this.userName;
			set => this.userName = value!;
		}

		public uint Id
		{
			get => this.id;
			set => this.id = value;
		}

		public string? FileName
		{
			get => this.fileName;
			set => this.fileName = value!;
		}

		public int FindUserIndex(UserStruct us, List<string> usersVec)
		{
			int result = -1;
			int i = 0;

			bool isLoopExit = false;
			bool isFound = false;

			bool notEmptyVector = usersVec.Count > 0;

			while (notEmptyVector && !(isLoopExit || isFound))
			{
				string user = usersVec[i];

				List<string> v = user.Split(GlobalConstants.ELEMENT_DATA_SEPARATOR, StringSplitOptions.RemoveEmptyEntries).ToList();

				if (us.UserName == v[0])
				{
					isFound = true;
					result = i;
				}

				i++;

				if (i >= usersVec.Count)
				{
					isLoopExit = true;
				}
			}

			return result;
		}

		public bool GenerateReason(CommandStruct cmdStr, ref string? reason)
		{
			List<string> v = cmdStr.CommandLine!.Split(GlobalConstants.ELEMENT_DATA_SEPARATOR, StringSplitOptions.RemoveEmptyEntries).ToList();

			List<string> v1 = new List<string>();

			if (v.Count > 1)
			{
				for (int i = 2; i < v.Count; i++)
				{
					v1.Add(v[i]);
				}

				reason = string.Join(GlobalConstants.ELEMENT_DATA_SEPARATOR, v1);

				return true;
			}

			return false;
		}

		public Message Message => this.message;

		public Quiz Quiz => this.quiz;

		public uint Hash(string str)
		{
			uint hash = 1315423911;

			foreach (char ch in str)
			{
				hash ^= ((hash << 5) + ch + (hash >> 2));
			}

			return (hash & 0x7FFFFFFF);
		}

		public UserOptions FindUserData(UserStruct us, bool exsist)
		{
			string users = string.Empty;

			users = this.AllUsers(users);

			List<string> usersVec = users.Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries).ToList();

			int userIndex = this.FindUserIndex(us, usersVec);

			if (userIndex > -1)
			{
				List<string> v = usersVec[userIndex].Split(GlobalConstants.ELEMENT_DATA_SEPARATOR, StringSplitOptions.RemoveEmptyEntries).ToList();

				if (exsist && this.Hash(us.Password) != uint.Parse(v[1]))
				{
					return UserOptions.WrongPassword;
				}
				else if (exsist && ((UserOptions)Enum.Parse(typeof(UserOptions), v[4]) & UserOptions.Ban) == UserOptions.Ban)
				{
					return UserOptions.Ban;
				}

				us.FileName = v[2];
				us.Id = uint.Parse(v[3]);
				us.FirstName = v[0];
				us.Password = v[1];

				return (UserOptions.Empty | UserOptions.OK | UserOptions.AlreadyExisist);
			}

			return UserOptions.NotFound;

		}

		public string AllUsers(string users)
		{
			this.provider.Action(ref users, ProviderOptions.UserFind);
			return users;
		}

		public virtual void SetUpUserData(UserStruct us, ref List<string> v, UserOptions uo)
		{
			if ((uo & UserOptions.NewUserCreated) == UserOptions.NewUserCreated)
			{
				this.FirstName = us.FirstName;
				this.LastName = us.LastName;
			}
			else
			{
				string s = us.FileName!;
				this.provider.Action(ref s, ProviderOptions.UserLoad);

				v = s.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries).ToList();

				this.FirstName = v[0];
				this.LastName = v[1];
				this.IsHasLog = true;
			}

			this.FileName = us.FileName;
			this.Id = us.Id;
			this.UserName = us.UserName!;
			if (us.Password != string.Empty)
			{
				this.Password = uint.Parse(us.Password!);
			}
		}

		public virtual string BuildUserData() => $"{this.FileName}{GlobalConstants.FILENAME_TO_DATA_SEPARATOR}{this.firstName}{Environment.NewLine}{this.lastName}{Environment.NewLine}";

		public virtual void SaveData()
		{
			return;
		}

		public virtual void Help()
		{
			foreach (var command in GlobalConstants.listUserCommands)
			{
				this.Writer.WriteLine(command);
			}
		}
	}
}
