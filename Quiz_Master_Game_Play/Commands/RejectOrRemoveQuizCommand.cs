namespace Quiz_Master_Game_Play.Commands
{
	using Common.Classes;
	using Common.Constants;
	using Common.Enums;
	using Quiz_Master_Game_Play.Game.Contract;
	using Quiz_Master_Game_Play.Users;

	public class RejectOrRemoveQuizCommand : Command
	{
		private string? reason;
		protected QuizStatus status;

		public RejectOrRemoveQuizCommand(string commandString) : base(commandString)
		{
			this.reason = null;
		}

		public override bool CanExecute(IGame game)
		{
			if ((game.User is Admin) && (game.Cmd.Command == this.CommandString) && (game.Cmd.ParamRange >= 3))
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

			//UserId = 0|date|quizId|SendUserName|ByUserName|reason - Message to Admin
			//UserId|text message - Message to player

			bool isFound = false;

			uint quizId = uint.Parse(game.Cmd.Param1!);

			string ByUserName = string.Empty;

			string s = game.User.Quiz.FindAllQuizzes();

			List<string> quizzesVec = s
				.Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries)
				.ToList();

			List<string> newQuizzesVec = new List<string>();

			foreach (var quizString in quizzesVec)
			{
				List<string> quizVec = quizString
					.Split(GlobalConstants.QUIZ_ELEMENT_DATA_SEPARATOR, StringSplitOptions.RemoveEmptyEntries)
					.ToList();

				uint id = uint.Parse(quizVec[0]);

				if (id == quizId)
				{
					ByUserName = quizVec[2];

					isFound = true;

					string separator = GlobalConstants.QUIZ_ELEMENT_SEPARATOR;
					string quizStr = $"{quizVec[0]}{separator}{quizVec[1]}{separator}{quizVec[2]}{separator}{quizVec[3]}{separator}{this.status.ToString()}{separator}{quizVec[5]}{separator}{quizVec[6]}";

					newQuizzesVec.Add(quizStr);
				}
				else
				{
					newQuizzesVec.Add(quizString);
				}
			}

			if (isFound)
			{
				string allQuizzesString = $"{GlobalConstants.QUIZZES_FILE_NAME}{GlobalConstants.FILENAME_SEPARATOR}{string.Join(Environment.NewLine, newQuizzesVec)}";

				game.Provider.Action(ref allQuizzesString, ProviderOptions.QuizIndexSave);

				UserStruct us = new UserStruct();
				us.UserName = ByUserName;

				game.User.FindUserData(us, GlobalConstants.NOT_LOGIN);

				List<string> messagesVec = game
					.User
					.Message
					.FindAllMessages()
					.Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries)
					.ToList();

				string separator = GlobalConstants.MESSAGE_ELEMENT_SEPARATOR;
				string newMessageString = $"{us.Id}{separator}{this.reason}";

				//UserId|text message - Message to player

				messagesVec.Add(newMessageString);

				string allMessagesString = $"{GlobalConstants.MESSAGES_FILE_NAME}{GlobalConstants.FILENAME_SEPARATOR}{string.Join(Environment.NewLine, messagesVec)}";

				game.Provider.Action(ref allMessagesString, ProviderOptions.MessagesSave);
			}
			else
			{
				game.Writer.WriteLine("No quiz found with that id!");
			}
		}
	}
}
