namespace Quiz_Master_Game_Play.Commands
{
	using Common.Classes;
	using Common.Constants;
	using Common.Enums;
	using Quiz_Master_Game_Play.Game.Contract;
	using Quiz_Master_Game_Play.Users;

	public class SignupUserCommand : Command
	{
		public SignupUserCommand(string commandString) : base(commandString)
		{
		}

		public override bool Execute(IGame game)
		{
			if (this.CommandString == game.Cmd.Command && game.Cmd.ParamRange == 6)
			{
				if (game.User.IsHasLog)
				{
					game.Writer.WriteLine("You cannot log in a new user before logging out!");
					return false;
				}

				UserStruct us = new UserStruct();

				us.FirstName = game.Cmd.Param1;
				us.LastName = game.Cmd.Param2;
				us.UserName = game.Cmd.Param3;
				us.Password = game.Cmd.Param4;

				UserOptions uo = game.User.FindUserData(us, GlobalConstants.NOT_EXSIST);

				if (uo.HasFlag(UserOptions.AlreadyExisist))
				{
					game.Writer.WriteLine("Such a user already exists!");
					return false;
				}
				else if (us.Password != game.Cmd.Param5)
				{
					game.Writer.WriteLine("Passwords do not match!");
					return false;
				}

				string users = string.Empty;
				users = game.User.AllUsers(users);
				List<string> usersVec = users.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries).ToList();

				string newUser = $"{us.UserName} {game.User.Hash(us.Password)} ";
				string fileName = $"{us.UserName}{++game.MaxUserId}.txt";

				newUser = $"{newUser}{fileName} {game.MaxUserId} {UserOptions.OK}";
				us.FileName = fileName;
				us.Id = game.MaxUserId;
				us.Password = string.Empty;
				usersVec.Add(newUser);

				string usersString = string.Join(Environment.NewLine, usersVec);

				game.Provider.Action(ref usersString, ProviderOptions.NewUserSave);

				Player newPlayer = new Player(game.Writer, game.Reader, game.Provider, us, UserOptions.NewUserCreated);

				game.Writer.WriteLine($"Signup {us.UserName} successful!");

				game.SaveConfig();
				return true;
			}

			return false;
		}
	}
}
