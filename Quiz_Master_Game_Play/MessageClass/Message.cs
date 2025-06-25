namespace Quiz_Master_Game_Play.MessageClass
{
	using Common.BaseProvider.Contract;
	using Common.Enums;

	using static Common.Constants.GlobalConstants;

	public class Message
	{
		private IBaseProvider provider;

		public Message(IBaseProvider provider)
		{
			this.provider = provider;
		}


		private void SaveNewMessage(string s)
		{
			string allMessages = this.FindAllMessages();

			List<string> messagesVec = allMessages.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries).ToList();

			messagesVec.Add(s);

			string result = string.Join(Environment.NewLine, messagesVec);

			this.SaveMessage(result);
		}


		private void SaveMessage(string s)
		{
			string messages = $"{MESSAGES_FILE_NAME}{FILENAME_SEPARATOR}{s}";

			this.provider.Action(ref messages, ProviderOptions.MessagesSave);
		}

		public string FindAllMessages()
		{
			string s = MESSAGES_FILE_NAME;
			this.provider.Action(ref s, ProviderOptions.MessagesLoad);

			if (s == ERROR)
			{
				s = string.Empty;
			}

			return s;
		}

		public void SendMessage(string s)
		{
			this.SaveNewMessage(s);
		}
	}
}
