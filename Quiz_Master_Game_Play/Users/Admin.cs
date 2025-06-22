namespace Quiz_Master_Game_Play.Users
{
	using Common.BaseProvider.Contract;
	using Common.Constants;
	using Common.IO.Contract;

	public class Admin : User
	{
		public Admin(IWriter writer, IReader reader, IBaseProvider provider) : base(writer, reader, provider)
		{
		}

		public override void Help()
		{
			base.Help();

			foreach (var command in GlobalConstants.listAdminCommands)
			{
				this.Writer.WriteLine(command);
			}
		}

		public override string BuildUserData() => base.BuildUserData();

		public override void SaveData() => base.SaveData();
	}
}
