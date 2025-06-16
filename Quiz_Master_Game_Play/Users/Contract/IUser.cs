namespace Quiz_Master_Game_Play.Users.Contract
{
	using Common.BaseProvider.Contract;
	using Common.Classes;
	using Common.IO.Contract;
	using System.Numerics;

	public interface IUser
	{
        public string Name { get; }

        public string FirstName { set; }

        public string LastName { set; }

        public string UserName { get; set; }

        public uint Id { get; set; }

        public string FileName { get; set; }

        public void Login();

        public void Logout();

        public void Action(CommandStruct cmdStr);

        public IWriter Writer { get; }

        public IReader Reader { get; }

        public IBaseProvider Provider { get; }

        
    virtual void Print() = 0;
    virtual bool GetIsHasLog() = 0;
    virtual String BuildUserData() = 0;
    virtual void Help() = 0;
    virtual unsigned int Hash(const String&) = 0;
			virtual void SetIsHasLog(bool) = 0;
    virtual int FindUserData(UserStruct&, bool) = 0;
    virtual void AllUsers(String&) = 0;
    virtual void SetUpUserData(UserStruct&, Vector<String>&, UserOptions) = 0;
    virtual void SaveData() = 0;
};
	}
}
