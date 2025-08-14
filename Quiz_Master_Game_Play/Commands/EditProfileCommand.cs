namespace Quiz_Master_Game_Play.Commands
{
	using Common.Classes;
	using Common.Constants;
	using Common.Enums;
	using Quiz_Master_Game_Play.Game.Contract;
	using Quiz_Master_Game_Play.Users;

	public class EditProfileCommand : Command
	{
		public EditProfileCommand(string commandString) : base(commandString)
		{
		}

		public override bool CanExecute(IGame game)
		{
			if ((game.User is Player) && (game.Cmd.Command == this.CommandString) && (game.Cmd.ParamRange == 1))
			{
				return true;
			}

			return false;
		}

		public override void Execute(IGame game)
		{
			game.Writer.Write("Enter password: ");
			string password = game.Reader.ReadLine();

			if (game.User.Hash(password) != game.User.Password)
			{
				game.Writer.WriteLine("Wrong password");
				return;
			}

			game.Writer.Write("Enter new password: ");
			string newPassword = game.Reader.ReadLine();

			game.Writer.Write("Reenter new password: ");
			string repeatNewPassword = game.Reader.ReadLine();

			if (newPassword != repeatNewPassword)
			{
				game.Writer.WriteLine("Passwords do not match");

				return;
			}

			UserStruct us = new UserStruct();

			us.FirstName = string.Empty;
			us.LastName = string.Empty;
			us.UserName = game.User.UserName;
			us.Password = string.Empty;
						
			string users = string.Empty;			

			List<string> usersVec = game
				.User
				.AllUsers(users)
				.Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries)
				.ToList();

			int userIndex = game.User.FindUserIndex(us, usersVec);

			if (userIndex < 0)
			{
				game.Writer.WriteLine("No user with that name found!");
				return;
			}

			List<string> v = usersVec[userIndex]
				.Split(GlobalConstants.ELEMENT_DATA_SEPARATOR, StringSplitOptions.RemoveEmptyEntries)
				.ToList();

			v[1] = $"{game.User.Hash(newPassword)}";
			game.User.Password = game.User.Hash(newPassword);

			usersVec[userIndex] = string.Join(GlobalConstants.ELEMENT_DATA_SEPARATOR, v);

			string s1 = string.Join(Environment.NewLine, usersVec);

			game.Provider.Action(ref s1, game.User.Id, ProviderOptions.EditUser);
		}
	}
}
