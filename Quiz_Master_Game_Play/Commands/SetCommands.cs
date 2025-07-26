namespace Quiz_Master_Game_Play.Commands
{
	using Common.Constants;
	using Quiz_Master_Game_Play.Commands.Contract;

	public class SetCommands
	{
		private ICollection<ICommand> commands;

		public SetCommands()
		{
			this.commands = new List<ICommand>()
			{
				  new LoginUserCommand(GlobalConstants.LOGIN)
				, new LogoutUserCommand(GlobalConstants.LOGOUT)
				, new HelpUserCommand(GlobalConstants.HELP)
				, new PendingCommand(GlobalConstants.PENDING)
				, new ApproveQuizCommand(GlobalConstants.APPROVE_QUIZ)
				, new RejectQuizCommand(GlobalConstants.REJECT_QUIZ)
				, new ViewReportsCommand(GlobalConstants.VIEW_REPORTS)
				, new RemoveQuizCommand(GlobalConstants.REMOVE_QUIZ)
				, new BanCommand(GlobalConstants.BAN)
				, new SignupUserCommand(GlobalConstants.SIGNUP)
				, new ViewProfileCommand(GlobalConstants.VIEW_PROFILE)
				, new EditProfileCommand(GlobalConstants.EDIT_PROFILE)
				, new ViewChalleengesCommand(GlobalConstants.VIEW_CHALLEENGES)
				, new ViewFinishedChalleengesCommand(GlobalConstants.VIEW_FINISHED_CHALLEENGES)
				, new MessageCommand(GlobalConstants.MESSAGES)
				, new CreateQuizCommand(GlobalConstants.CREATE_QUIZ)
				, new QuizzesCommand(GlobalConstants.QUIZZES)
				, new LikeQuizCommand(GlobalConstants.LIKE_QUIZ)
				, new UnlikeQuizCommand(GlobalConstants.UNLIKE_QUIZ)
				, new AddToFavsCommand(GlobalConstants.ADD_TO_FAVS)
				, new RemoveFromFavsCommand(GlobalConstants.REMOVE_FROM_FAVS)
				, new StartQuizCommand(GlobalConstants.START_QUIZ)
				, new SaveQuizCommand(GlobalConstants.SAVE_QUIZ)
				, new ReportQuizCommand(GlobalConstants.REPORT_QUIZ)
			};
		}

		public IEnumerable<ICommand> Commands => this.commands;
	}
}
