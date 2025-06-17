namespace Quiz_Master_Game_Play.Users
{
	using Common.BaseProvider.Contract;
	using Common.Classes;
	using Common.Enums;
	using Common.IO.Contract;
	using Quiz_Master_Game_Play.Users.Contract;
	using System.Collections.Generic;
	using System.Numerics;

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

		User(IWriter writer, IReader reader, IBaseProvider provider)
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

		public string LastName { set => throw new NotImplementedException(); }
		public string UserName { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
		public uint Id { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
		public string FileName { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

		

		

		

		

		int FindUserIndex(UserStruct& us, Vector<String>& usersVec);
		bool GenerateReason(CommandStruct&);
		Quiz& GetQuiz();
		Message& GetMessage();

		public:
    virtual void setLastName(const String) override;

    virtual String getUserName() const override;
    virtual void setUserName(const String) override;

    virtual void setId(const unsigned int) override;
    virtual unsigned int getId() const override;

    virtual String getFileName() const override;
    virtual void setFileName(const String) override;

    virtual void Login() override;
    virtual void Logout() override;
    virtual void Action(CommandStruct&) override;

    
    virtual unsigned int Hash(const String& str) override;
    virtual int FindUserData(UserStruct&, bool) override;
    virtual void AllUsers(String&) override;
    virtual void SetUpUserData(UserStruct&, Vector<String>&, UserOptions) override;

    virtual void Print() override;
    virtual String BuildUserData() override;
    virtual void Help() override;
    virtual void SaveData() string IUser.BuildUserData()
		{
			throw new NotImplementedException();
		}

		void IUser.Help()
		{
			throw new NotImplementedException();
		}

		public uint Hash(string str)
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

		public void SetUpUserData(UserStruct us, List<string> list, UserOptions uo)
		{
			throw new NotImplementedException();
		}

		void IUser.SaveData()
		{
			throw new NotImplementedException();
		}

		override;

    virtual ~User();
	};
}
