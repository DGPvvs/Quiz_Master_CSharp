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
				  new LogoutUserCommand(GlobalConstants.LOGOUT)
				, new HelpUserCommand(GlobalConstants.HELP)
				, new LoginUserCommand(GlobalConstants.LOGIN)
				, new SignupUserCommand(GlobalConstants.SIGNUP)
				, new ViewProfileCommand(GlobalConstants.VIEW_PROFILE)
				, new CreateQuizCommand(GlobalConstants.CREATE_QUIZ)
				, new BanCommand(GlobalConstants.BAN)
				, new PendingCommand(GlobalConstants.PENDING)
				, new ApproveQuizCommand(GlobalConstants.APPROVE_QUIZ)
				, new ViewReportsCommand(GlobalConstants.VIEW_REPORTS)
				, new QuizzesCommand(GlobalConstants.QUIZZES)
				, new ReportQuizCommand(GlobalConstants.REPORT_QUIZ)
				, new RejectQuizCommand(GlobalConstants.REJECT_QUIZ)
				, new RemoveQuizCommand(GlobalConstants.REMOVE_QUIZ)
				, new EditProfileCommand(GlobalConstants.EDIT_PROFILE)
				, new ViewChalleengesCommand(GlobalConstants.VIEW_CHALLEENGES)
				, new ViewFinishedChalleengesCommand(GlobalConstants.VIEW_FINISHED_CHALLEENGES)
				, new MessageCommand(GlobalConstants.MESSAGES)
				, new LikeQuizCommand(GlobalConstants.LIKE_QUIZ)
				, new UnlikeQuizCommand(GlobalConstants.UNLIKE_QUIZ)
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
