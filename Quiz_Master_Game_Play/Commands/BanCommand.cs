namespace Quiz_Master_Game_Play.Commands
{
	using Common.Classes;
	using Common.Constants;
	using Common.Enums;
	using Quiz_Master_Game_Play.Game.Contract;
	using Quiz_Master_Game_Play.Users;

	public class BanCommand : Command
	{
		public BanCommand(string commandString) : base(commandString)
		{
		}

		public override bool Execute(IGame game)
		{
			if ((game.User is Admin) && (game.Cmd.Command == this.CommandString) && (game.Cmd.ParamRange == 2))
			{
				UserStruct us = new UserStruct();

				us.FirstName = string.Empty;
				us.LastName = string.Empty;
				us.UserName = game.Cmd.Param1;
				us.Password = string.Empty;
				
				string users = game.User.AllUsers(string.Empty);
				List<string> usersVec = users.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries).ToList();

				int userIndex = game.User.FindUserIndex(us, usersVec);

				if (userIndex < 0)
				{
					game.Writer.WriteLine("No user with that name found!");
					return true;
				}

				List<string> v = usersVec[userIndex]
					.Split(GlobalConstants.ELEMENT_DATA_SEPARATOR, StringSplitOptions.RemoveEmptyEntries)
					.ToList();

				if (uint.Parse(v[3]) <= 10)
				{
					game.Writer.WriteLine("Banning an administrator is not allowed!");
				}
				else
				{
					v[4] = (UserOptions.Empty | UserOptions.Ban).ToString();

					string s = string.Join(GlobalConstants.ELEMENT_DATA_SEPARATOR, v);
					usersVec[userIndex] = s;

					string s1 = string.Join(GlobalConstants.ROW_DATA_SEPARATOR, usersVec);
					game.Provider.Action(ref s1, ProviderOptions.NewUserSave);
				}

				return true;
			}

			return false;
		}
	}
}
