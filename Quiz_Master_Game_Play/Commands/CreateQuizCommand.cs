namespace Quiz_Master_Game_Play.Commands
{
	using Common.Enums;
	using Quiz_Master_Game_Play.Game.Contract;
	using Quiz_Master_Game_Play.Questions;
	using Quiz_Master_Game_Play.QuizClass;
	using Quiz_Master_Game_Play.Users;
	using System.Text;

	using static Common.Constants.GlobalConstants;

	public class CreateQuizCommand : Command
	{
		private const string TF = "T/F";
		private const string SC = "SC";
		private const string MC = "MC";
		private const string ShA = "ShA";
		private const string MP = "MP";

		private IGame game;

		public CreateQuizCommand(string commandString) : base(commandString)
		{
			this.game = null!;
		}

		public override bool Execute(IGame game)
		{
			if ((game.User is Player) && (game.Cmd.Command == this.CommandString) && (game.Cmd.ParamRange == 1))
			{
				this.game = game;

				game.Writer.Write("Enter quiz title: ");
				string quizName = game.Reader.ReadLine();

				game.Writer.Write("Enter number of questions: ");
				string numOfQuestionsString = game.Reader.ReadLine();
				uint numOfQuestions = uint.Parse(numOfQuestionsString);

				uint quizId = game.MaxQuizId + 1;
				game.MaxQuizId = quizId;

				Quiz quiz = new Quiz(game.Writer, game.Reader, game.Provider, quizName, game.User.UserName, quizId, numOfQuestions, 0);

				for (int i = 0; i < numOfQuestions; i++)
				{
					game.Writer.Write($"Enter question {i + 1} type(T/F, SC, MC, ShA, MP): ");
					string questionType = game.Reader.ReadLine();

					game.Writer.Write("Enter description: ");
					string description = game.Reader.ReadLine();

					if (questionType == TF)
					{
						TrueOrFalseQuestion question = this.CreateTF(description);
						quiz.Questions.Add(question);
					}
					else if (questionType == SC)
					{
						SingleChoiceQuestion question = this.CreateSC(description);
						quiz.Questions.Add(question);
					}
					else if (questionType == MC)
					{
						MultipleChoiceQuestion question = this.CreateMC(description);
						quiz.Questions.Add(question);
					}
					else if (questionType == ShA)
					{
						ShortAnswerQuestion question = this.CreateShA(description);
						quiz.Questions.Add(question);
					}
					else if (questionType == MP)
					{
						MatchingPairsQuestion question = this.CreateMP(description);
						quiz.Questions.Add(question);
					}
					else
					{
						game.Writer.WriteLine("Incorrect Question Type");
						i--;
					}
				}

				quiz.SaveQuiz(QuizStatus.NewQuiz, 0);
				game.SaveConfig();

				return true;
			}

			return false;
		}

		private MatchingPairsQuestion CreateMP(string description)
		{
			this.game.Writer.Write("Enter left column values count: ");
			string possibleAnswerCountString = this.game.Reader.ReadLine();
			uint possibleAnswerCount = uint.Parse(possibleAnswerCountString);

			List<string> v1 = new List<string>();
			List<string> v2 = new List<string>();

			for (int j = 0; j < possibleAnswerCount; j++)
			{
				string questNum = $"{(char)(j + 'A')}";

				this.game.Writer.Write($"Enter value {questNum}: ");
				string answ = this.game.Reader.ReadLine();

				v1.Add(answ);
			}

			this.game.Writer.Write("Enter right column values count: ");
			possibleAnswerCountString = this.game.Reader.ReadLine();
			possibleAnswerCount = uint.Parse(possibleAnswerCountString);

			for (int j = 0; j < possibleAnswerCount; j++)
			{
				string questNum = $"{(char)(j + 'a')}";

				this.game.Writer.Write("Enter value " + questNum + ": ");
				string answ = this.game.Reader.ReadLine();

				v2.Add(answ);
			}

			this.game.Writer.Write("Enter correct pairs: ");
			string answer = this.game.Reader.ReadLine();

			this.game.Writer.Write("Enter points: ");
			string pointsString = this.game.Reader.ReadLine();

			uint points = uint.Parse(pointsString);

			MatchingPairsQuestion question = new MatchingPairsQuestion(this.game.Writer, this.game.Reader, description, answer, points, false, (byte)possibleAnswerCount);

			foreach (var item in v1)
			{
				question.Questions.Add(item);
			}

			foreach (var item in v2)
			{
				question.Answers.Add(item);
			}

			return question;
		}

		private ShortAnswerQuestion CreateShA(string description)
		{
			this.game.Writer.Write("Enter correct answer: ");
			string answer = this.game.Reader.ReadLine();

			this.game.Writer.Write("Enter points: ");
			string pointsString = this.game.Reader.ReadLine();

			uint points = uint.Parse(pointsString);

			ShortAnswerQuestion question = new ShortAnswerQuestion(this.game.Writer, this.game.Reader, description, answer, points, false);

			return question;
		}

		private MultipleChoiceQuestion CreateMC(string description)
		{
			this.game.Writer.Write("Enter possible answer count: ");
			string possibleAnswerCountString = this.game.Reader.ReadLine();

			uint possibleAnswerCount = uint.Parse(possibleAnswerCountString);

			List<string> v = new List<string>();

			bool isFirst = true;

			StringBuilder sb = new StringBuilder();

			for (int j = 0; j < possibleAnswerCount; j++)
			{
				string questNum = $"{(char)(j + 'A')}";

				if (isFirst)
				{
					isFirst = false;
				}
				else
				{
					sb.Append(", ");
				}

				sb.Append(questNum);

				this.game.Writer.Write($"Enter answer {questNum}: ");
				string answ = this.game.Reader.ReadLine();

				v.Add(answ);
			}

			this.game.Writer.Write($"Enter correct answer ({sb}): ");
			string answer = this.game.Reader.ReadLine();

			this.game.Writer.Write("Enter points: ");
			string pointsString = this.game.Reader.ReadLine();

			uint points = uint.Parse(pointsString);

			MultipleChoiceQuestion question = new MultipleChoiceQuestion(this.game.Writer, this.game.Reader, description, answer, points, false, (byte)possibleAnswerCount);

			foreach (var item in v)
			{
				question.Questions.Add(item);
			}

			return question;
		}

		private SingleChoiceQuestion CreateSC(string description)
		{
			List<string> v = new List<string>();

			for (int j = 0; j < MAX_LENGTH_SC_QUESTION; j++)
			{
				string questNum = $"{(char)(j + 'A')}";

				this.game.Writer.Write($"Enter answer {questNum}: ");
				string answ = this.game.Reader.ReadLine();

				v.Add(answ);
			}

			this.game.Writer.Write("Enter correct answer (A, B, C, D): ");
			string answer = this.game.Reader.ReadLine();

			this.game.Writer.Write("Enter points: ");
			string pointsString = this.game.Reader.ReadLine();

			uint points = uint.Parse(pointsString);

			SingleChoiceQuestion question = new SingleChoiceQuestion(this.game.Writer, this.game.Reader, description, answer, points, false);

			foreach (var item in v)
			{
				question.Questions.Add(item);
			}

			return question;
		}

		private TrueOrFalseQuestion CreateTF(string description)
		{
			this.game.Writer.Write("Enter correct answer(True/False): ");
			string answer = this.game.Reader.ReadLine();

			this.game.Writer.Write("Enter points: ");
			string pointsString = this.game.Reader.ReadLine();

			uint points = uint.Parse(pointsString);

			TrueOrFalseQuestion question = new TrueOrFalseQuestion(this.game.Writer, this.game.Reader, description, answer, points, false);

			return question;
		}
	}
}
