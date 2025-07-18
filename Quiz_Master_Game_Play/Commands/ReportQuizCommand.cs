namespace Quiz_Master_Game_Play.Commands
{
	using Common.Classes;
	using Common.Constants;
	using Common.Enums;
	using Quiz_Master_Game_Play.Game.Contract;
	using Quiz_Master_Game_Play.Users;

	public class ReportQuizCommand : Command
	{
		private string? reason;

		public ReportQuizCommand(string commandString) : base(commandString)
		{
			this.reason = null;
		}

		public override bool CanExecute(IGame game)
		{
			if ((game.User is Player) && (game.Cmd.Command == this.CommandString) && (game.Cmd.ParamRange >= 3))
			{
				if (game.User.GenerateReason(game.Cmd, ref this.reason))
				{
					return uint.TryParse(game.Cmd.Param1, out uint result);
				}
			}
			return false;
		}

		public override void Execute(IGame game)
		{
			//id|quizName|userName|quizFileName|QuizStatus|numOfQuestions|Likes

			//UserId = 0|date|quizId|SendUserName|ByUserName|reason - Message toAdmin
			//UserId|text message - Message to player

			uint quizId = uint.Parse(game.Cmd.Param1!);

			string sendedUserName = game.User.UserName!;
			string ByUserName = string.Empty;

			string s = game.User.Quiz.FindAllQuizzes();

			List<string> quizzesVec = s
				.Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries)
				.ToList();

			int i = 0;

			bool isLoopExit = false;

			while (!isLoopExit)
			{
				List<string> quizVec = quizzesVec[i]
					.Split(GlobalConstants.QUIZ_ELEMENT_DATA_SEPARATOR, StringSplitOptions.RemoveEmptyEntries)
					.ToList();

				uint id = uint.Parse(quizVec[0]);

				if (id == quizId)
				{
					ByUserName = quizVec[2];

					isLoopExit = true;
				}

				i++;

				if (i >= quizzesVec.Count)
				{
					isLoopExit = true;
				}
			}

			List<string> messagesVec = game
				.User
				.Message
				.FindAllMessages()
				.Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries)
				.ToList();

			string separator = GlobalConstants.MESSAGE_ELEMENT_SEPARATOR;

			string newMessageString = $"0{separator}{Date.DateNow}{separator}{quizId}{separator}{sendedUserName}{separator}{ByUserName}{separator}{this.reason}";

			//UserId = 0|date|quizId|SendUserName|ByUserName|reason - Message toAdmin

			messagesVec.Add(newMessageString);

			string allMessagesString = string.Join(Environment.NewLine, messagesVec);

			allMessagesString = $"{GlobalConstants.MESSAGES_FILE_NAME}{GlobalConstants.FILENAME_SEPARATOR}{allMessagesString}";

			game.Provider.Action(ref allMessagesString, ProviderOptions.MessagesSave);
		}
	}
}
