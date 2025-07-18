namespace Quiz_Master_File_DB
{
	using Common.Constants;
	using Quiz_Master_Game_Play.Game.Contract;
	using Quiz_Master_Game_Play.Game;
	using Quiz_Master_File_DB.IO;
	using Quiz_Master_File_DB.BaseProvider;
	using Quiz_Master_Game_Play.Commands.Contract;
	using Quiz_Master_Game_Play.Commands;

	public class QuizMasterFileDB
	{
		static void Main(string[] args)
		{
			ConsoleWriter writer = new ConsoleWriter();
			ConsoleReader reader = new ConsoleReader();
			FileBaseProvider provider = new FileBaseProvider();

			List<ICommand> commands = new List<ICommand>()
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

			IGame game = new Game(writer, reader, provider, commands);

			//game.Init();
			//game.Run();

			try
			{
				game.Init();
				game.Run();
			}
			catch (Exception ex)
			{
				writer.WriteLine(ex.Message);
				throw ex;
			}
		}
	}
}
