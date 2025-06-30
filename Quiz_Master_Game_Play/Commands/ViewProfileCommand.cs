namespace Quiz_Master_Game_Play.Commands
{
	using Common.Classes;
	using Common.Enums;
	using Quiz_Master_Game_Play.Game.Contract;
	using Quiz_Master_Game_Play.Users;
	using System;
	using System.Text;

	using static Common.Constants.GlobalConstants;

	public class ViewProfileCommand : Command
	{
		private enum DatBuild
		{
			VIEW_SELF_PROFILE,
			VIEW_OTHER_PROFILE,
		};

		public ViewProfileCommand(string commandString) : base(commandString)
		{
		}

		public override void Execute(IGame game)
		{
			if (game.Cmd.ParamRange == 1)
			{
				this.ViewSelfProfile((game.User as Player)!, DatBuild.VIEW_SELF_PROFILE);
			}
			else if (game.Cmd.ParamRange == 2)
			{
				this.ViewOtherProfile(game.Cmd.Param1, game, DatBuild.VIEW_OTHER_PROFILE);
			}
		}

		private void ViewOtherProfile(string? userName, IGame game, DatBuild vIEW_OTHER_PROFILE)
		{
			UserStruct us = new UserStruct();

			us.UserName = userName;

			UserOptions uo = (game.User as Player)!.FindUserData(us, NOT_EXSIST);

			if (uo == UserOptions.NotFound)
			{
				game.Writer.WriteLine("User not found!");
			}
			else if (uo.HasFlag(UserOptions.AlreadyExisist))
			{
				Player otherPlayer = new Player(game.Writer, game.Reader, game.Provider, game);

				if (us.Id <= 10)
				{
					otherPlayer.Writer.WriteLine($"Unable to display profile information!{Environment.NewLine} The specified user is an administrator");
				}
				else
				{
					List<string> v = new List<string>();
					otherPlayer.SetUpUserData(us, ref v, UserOptions.Empty);
					this.ViewSelfProfile(otherPlayer, DatBuild.VIEW_OTHER_PROFILE);
				}
			}
		}

		private void ViewSelfProfile(User user, DatBuild option)
		{
			StringBuilder sb = new StringBuilder();

			sb.AppendLine($"{user.Name} {user.UserName}");
			sb.Append($"Level : {(user as Player)?.Level}");

			if (option == DatBuild.VIEW_SELF_PROFILE)
			{
				sb.Append($"{'\t'}{(user as Player)?.Points}/{(user as Player)?.PointsForLevel()} Points to next level");
			}

			sb.AppendLine();

			if (option == DatBuild.VIEW_OTHER_PROFILE)
			{
				sb.AppendLine();
			}

			sb.AppendLine("Created Quizzes:");

			foreach (string? quizString in (user as Player)?.ListCreatedQuizzes!)
			{
				List<string> v = quizString
					.Split(CREATED_QUIZ_SEPARATOR_STRING, StringSplitOptions.RemoveEmptyEntries)
					.ToList();

				sb.AppendLine($"[{v[0]}] {v[1]}");
			}

			if (option == DatBuild.VIEW_SELF_PROFILE)
			{
				sb.AppendLine().Append("Liked Quizzes: ");

				foreach (var item in (user as Player)?.ListLikedQuizzes!)
				{
					sb.Append($"[{item}]");
				}

				sb.AppendLine().Append("Favorite Quizzes: ");

				foreach (var item in (user as Player)?.ListFavoriteQuizzes!)
				{
					sb.Append($"[{item}]");
				}

				sb.AppendLine();
			}

			user.Writer.WriteLine(sb.ToString().TrimEnd());
		}

		public override bool CanExecute(IGame game)
		{
			if (game.User is Player && this.CommandString == game.Cmd.Command)
			{
				return true;
			}

			return false;
		}
	}
}
