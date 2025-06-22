namespace Common.Constants
{
	public static class GlobalConstants
	{
		public static string[] listUserCommands =
		{
			"signup <first-name> <last-name> <username> <password1> <password2>"
			, "login <username> <password>"
			, "logout"
			, "help"
			, "exit"
		};

		public static string[] listAdminCommands =
		{
			"pending"
			, "approve-quiz <quiz id>"
			, "reject-quiz <quiz id> <reason>"
			, "view-reports"
			, "remove-quiz <quiz id> <reason>"
			, "ban <username>"
		};

		public static string[] listPlayerCommands =
		{
			  "view-profile"
			, "edit-profile"
			, "view-challeenges"
			, "view-finished-challeenges"
			, "view <nickname>"
			, "messages"
			, "create-quiz"
			, "quizzes"
			, "quizzes <username>"
			, "like-quiz <quiz id>"
			, "unlike-quiz <quiz id>"
			, "add-to-favs <quiz id>"
			, "remove-from-favs <quiz id>"
			, "start-quiz <quiz id> test | normal (shuffle)"
			, "save-quiz <quiz id> <filepath>"
			, "report-quiz <quiz id> <reason>"
		};		

		public static Dictionary<uint, uint> listPointForLevel = new Dictionary<uint, uint>()
		{
			{10, 1000},
			{20, 2000},
			{30, 3000},
			{40, 4000}

		};

		public const byte MAX_LENGTH_SC_QUESTION = 4;
				
		public const string CONFIG_FILE_NAME = "config.txt";
		public const string USERS_FILE_NAME = "users.txt";
		public const string QUIZZES_FILE_NAME = "quizzes.txt";
		public const string MESSAGES_FILE_NAME = "messages.txt";

		public const char FILENAME_TO_DATA_SEPARATOR = '$';
		public const char ROW_DATA_SEPARATOR = '\n';
		public const char ELEMENT_DATA_SEPARATOR = ' ';
		public const char QUIZ_ELEMENT_DATA_SEPARATOR = '|';
		public const char MESSAGE_ELEMENT_DATA_SEPARATOR = '|';
		public const char COMMA_DATA_SEPARATOR = ',';
		public const char QUOTES_DATA_SEPARATOR = '"';
		public const char CREATED_QUIZ_SEPARATOR = '#';
				
		public const string PROMPT_STRING = "> ";
		public const string CREATED_QUIZ_SEPARATOR_STRING = "#";
		public const string QUIZ_ELEMENT_SEPARATOR = "|";
		public const string MESSAGE_ELEMENT_SEPARATOR = "|";
		public const string FILENAME_SEPARATOR = "$";
		public const string ERROR = "error";

		public const bool EXSIST = true;
		public const bool NOT_EXSIST = false;
		public const bool NOT_LOGIN = false;
		public const bool TEST_MODE = true;
		public const bool NORMAL_MODE = false;

		public const string EXIT = "exit";
		public const string LOGIN = "login";
		public const string SIGNUP = "signup";
		public const string LOGOUT = "logout";
		public const string HELP = "help";
	}
}
