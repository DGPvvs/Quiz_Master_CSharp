namespace Quiz_Master_Game_Play.Users
{
	using Common.BaseProvider.Contract;
	using Common.Classes;
	using Common.Enums;
	using Common.IO.Contract;
	using Quiz_Master_Game_Play.MessageClass;
	using Quiz_Master_Game_Play.QuizClass;
	using Quiz_Master_Game_Play.Users.Contract;
	using System.Collections.Generic;
	using System.Numerics;

	using static Common.Constants.GlobalConstants;

	class User : IUser
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

		public string FirstName
		{
			set => this.firstName = value;
		}

		public string LastName
		{
			set => this.lastName = value;
		}

		public string UserName
		{
			get => this.userName;
			set => this.userName = value;
		}

		public uint Id
		{
			get => this.id;
			set => this.id = value;
		}

		public string FileName
		{
			get => this.fileName;
			set => this.fileName = value;
		}

		protected int FindUserIndex(UserStruct us, List<string> usersVec)
		{
			int result = -1;
			int i = 0;

			bool isLoopExit = false;
			bool isFound = false;

			bool notEmptyVector = usersVec.Count > 0;

			while (notEmptyVector && !(isLoopExit || isFound))
			{
				string user = usersVec[i];

				List<string> v = user.Split(ELEMENT_DATA_SEPARATOR, StringSplitOptions.RemoveEmptyEntries).ToList();

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

		protected bool GenerateReason(CommandStruct cmdStr)
		{
			List<string> v = cmdStr.CommandLine.Split(ELEMENT_DATA_SEPARATOR, StringSplitOptions.RemoveEmptyEntries).ToList();

			List<string> v1 = new List<string>();

			if (v.Count > 1)
			{
				for (int i = 2; i < v.Count; i++)
				{
					v1.Add(v[i]);
				}

				cmdStr.Param2 = string.Join(ELEMENT_DATA_SEPARATOR, v1);

				return true;
			}

			return false;
		}

		protected Quiz GetQuiz => this.quiz;

		protected Message GetMessage => this.message;

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

			List<string> usersVec = users.Split(ROW_DATA_SEPARATOR, StringSplitOptions.RemoveEmptyEntries).ToList();

			int userIndex = this.FindUserIndex(us, usersVec);

			if (userIndex > -1)
			{
				List<string> v = usersVec[userIndex].Split(ELEMENT_DATA_SEPARATOR, StringSplitOptions.RemoveEmptyEntries).ToList();

				if (exsist && this.Hash(us.Password) != uint.Parse(v[1]))
				{
					return UserOptions.WrongPassword;
				}
				else if (exsist && (v[4].StringToInt() & UserOptions::Ban) == UserOptions::Ban)
				{
					return UserOptions::Ban;
				}

				us.fileName = v[2];
				us.id = v[3].StringToInt();
				us.firstName = v[0];
				us.password = v[1];

				return (UserOptions::Empty | UserOptions::OK | UserOptions::AlreadyExisist);
			}

			return UserOptions::NotFound;

		}

		public string AllUsers(string users)
		{
			this.provider.Action(ref users, ProviderOptions.UserFind);
		}

		public void SetUpUserData(UserStruct us, List<string> v, UserOptions uo)
		{
			if ((uo & UserOptions::NewUserCreated) == UserOptions::NewUserCreated)
			{
				this->firstName = us.firstName;
				this->lastName = us.lastName;
			}
			else
			{
				String s = us.fileName;
				this->provider->Action(s, ProviderOptions::UserLoad);

				String::Split(ROW_DATA_SEPARATOR, v, s);

				this->firstName = v[0];
				this->lastName = v[1];
				this->SetIsHasLog(true);
			}

			this->fileName = us.fileName;
			this->id = us.id;
			this->userName = us.userName;
			this->password = us.password.StringToInt();
		}

		public string BuildUserData()
		{
			String result = EMPTY_STRING;

			char* arr = new char[2] { '\0' };
			arr[0] = FILENAME_TO_DATA_SEPARATOR;

			result += this->fileName + String(arr);

			arr[0] = ROW_DATA_SEPARATOR;
			String newLine = String(arr);

			result += this->firstName + newLine;
			result += this->lastName + newLine;


			return result;
		}

		public void SaveData()
		{
			return;
		}


		string IUser.BuildUserData()
		{
			throw new NotImplementedException();
		}

		int IUser.FindUserData(UserStruct us, bool exsist)
		{
			throw new NotImplementedException();
		}

		public string AllUsers()
		{
			throw new NotImplementedException();
		}

		void IUser.SaveData()
		{
			throw new NotImplementedException();
		}
	}
}
