namespace Quiz_Master_Game_Play.Commands
{
	using Common.Classes;
	using Common.Constants;
	using Common.Enums;
	using Quiz_Master_Game_Play.Game.Contract;

	public class LoginUserCommand : Command
	{
		public LoginUserCommand(string commandString) : base(commandString)
		{
		}

		public override bool Execute(IGame game)
		{
			if (this.CommandString == game.Cmd.Command && game.Cmd.ParamRange == 3)
			{
				if (game.User.IsHasLog)
				{
					game.Writer.WriteLine("You cannot log in a new user before logging out!");
					return true;
				}

				UserStruct us = new UserStruct();

				us.UserName = game.Cmd.Param1;
				us.Password = game.Cmd.Param2;

				UserOptions uo = game.User.FindUserData(us, GlobalConstants.EXSIST);

				if (uo == UserOptions.NotFound)
				{
					game.Writer.WriteLine("User not found!");
				}
				else if (uo == UserOptions.WrongPassword)
				{
					game.Writer.WriteLine("Wrong password!");
				}
				else if (uo == UserOptions.Ban)
				{
					game.Writer.WriteLine("Sorry, the user has been banned!");
				}
				else if (uo.HasFlag(UserOptions.OK))
				{
					game.LoadUser(us);

					string s = $"Welcome {game.User.Name}!";
					game.Writer.WriteLine(s);					
				}

				return true;
			}

			return false;
		}
	}
}
