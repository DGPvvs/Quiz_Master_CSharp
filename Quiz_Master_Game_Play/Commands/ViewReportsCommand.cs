namespace Quiz_Master_Game_Play.Commands
{
	using Common.Constants;
	using Quiz_Master_Game_Play.Game.Contract;
	using Quiz_Master_Game_Play.Users;

	public class ViewReportsCommand : Command
	{
		public ViewReportsCommand(string commandString) : base(commandString)
		{
		}

		public override bool CanExecute(IGame game)
		{
			if ((game.User is Admin) && (game.Cmd.Command == this.CommandString) && (game.Cmd.ParamRange == 1))
			{
				return true;
			}

			return false;
		}

		public override void Execute(IGame game)
		{
			string messagesString = game.User.Message.FindAllMessages();

			List<string> messagesVec = messagesString
				.Split(new char[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries)
				.ToList();

			foreach (var item in messagesVec)
			{
				List<string> messageVec = item
					.Split(GlobalConstants.MESSAGE_ELEMENT_DATA_SEPARATOR, StringSplitOptions.RemoveEmptyEntries)
					.ToList();

				//UserId = 0|date|quizId|SendUserName|ByUserName|reason - Message to Admin

				uint id = uint.Parse(messageVec[0]);

				if (id == 0)
				{
					string msg = $"{messageVec[1]} | sent By {messageVec[3]}	quiz id {messageVec[2]} by {messageVec[4]} | reason: {messageVec[5]}";
					game.Writer.WriteLine(msg);
				}
			}
		}
	}
}
