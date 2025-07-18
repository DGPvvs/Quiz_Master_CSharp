namespace Quiz_Master_Game_Play.Commands
{
	using Common.Constants;
	using Common.Enums;
	using Quiz_Master_Game_Play.Game.Contract;
	using Quiz_Master_Game_Play.Questions.Contract;
	using Quiz_Master_Game_Play.Questions;
	using Quiz_Master_Game_Play.QuizClass;
	using Quiz_Master_Game_Play.Users;

	public class LoadQuizCommand : Command
	{
		public LoadQuizCommand(string commandString) : base(commandString)
		{
		}

		public override bool CanExecute(IGame game)
		{
			if ((game.User is Player) && (game.Cmd.Command == this.CommandString))
			{
				return true;
			}

			return false;
		}

		public override void Execute(IGame game)
		{
			return;
		}

		protected Quiz LoadQuiz(IGame game, string idString, bool isTest)
		{
			uint id = uint.Parse(idString);
			string quizString = GlobalConstants.ERROR;

			Quiz quiz = null!;

			List<string> quizzesVec = game
				.User
				.Quiz
				.FindAllQuizzes()
				.Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries)
				.ToList();

			foreach (string s in quizzesVec)
			{
				QuizIndexDTO qiDTO = new QuizIndexDTO();

				qiDTO.SetElement(s);

				if (id == qiDTO.Id && qiDTO.QuizStatus == QuizStatus.ApprovedQuiz)
				{
					quizString = qiDTO.QuizFileName!;

					game.Provider.Action(ref quizString, ProviderOptions.QuizLoad);
					break;
				}
			}

			if (quizString != GlobalConstants.ERROR)
			{
				List<string> quizVec = quizString
					.Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries)
					.ToList();

				quiz = this.LoadQuizHeader(game, quizVec);

				quiz.Id = id;
				uint indexRow = 3;

				for (int i = 0; i < quiz.NumberOfQuestions; i++)
				{
					IQuestion question = null!;

					QuestionType questionType = (QuestionType)Enum.Parse(typeof(QuestionType), quizVec[(int)indexRow]);
					indexRow++;

					if (questionType == QuestionType.TF)
					{
						question = this.LoadTF(game, quizVec, indexRow, isTest);
					}
					else if (questionType == QuestionType.ShA)
					{
						question = this.LoadShA(game, quizVec, indexRow, isTest);
					}
					else if (questionType == QuestionType.SC)
					{
						question = this.LoadSC(game, quizVec, indexRow, isTest);
					}
					else if (questionType == QuestionType.MP)
					{
						question = this.LoadMP(game, quizVec, indexRow, isTest);
					}
					else if (questionType == QuestionType.MC)
					{
						question = this.LoadMC(game, quizVec, indexRow, isTest);
					}

					quiz.Questions.Add(question);
				}
			}

			return quiz;

		}

		private IQuestion LoadMC(IGame game, List<string> quizVec, uint indexRow, bool isTest)
		{
			string description = quizVec[(int)indexRow];
			indexRow++;

			string answer = quizVec[(int)indexRow];
			indexRow++;

			uint points = uint.Parse(quizVec[(int)indexRow]);
			indexRow++;

			byte numOfAnswers = byte.Parse(quizVec[(int)indexRow]);
			indexRow++;

			MultipleChoiceQuestion question = new MultipleChoiceQuestion(game.Writer, game.Reader, description, answer, points, isTest, numOfAnswers);

			for (int i = 0; i < numOfAnswers; i++)
			{
				question.Questions.Add(quizVec[(int)indexRow]);
				indexRow++;
			}

			return question;
		}

		private IQuestion LoadMP(IGame game, List<string> quizVec, uint indexRow, bool isTest)
		{
			string description = quizVec[(int)indexRow];
			indexRow++;

			string answer = quizVec[(int)indexRow];
			indexRow++;

			uint points = uint.Parse(quizVec[(int)indexRow]);
			indexRow++;

			byte numOfAnswers = byte.Parse(quizVec[(int)indexRow]);
			indexRow++;

			MatchingPairsQuestion question = new MatchingPairsQuestion(game.Writer, game.Reader, description, answer, points, isTest, numOfAnswers);

			for (int i = 0; i < numOfAnswers; i++)
			{
				question.Questions.Add(quizVec[(int)indexRow]);
				indexRow++;
			}

			byte numOfAnswers1 = byte.Parse(quizVec[(int)indexRow]);
			indexRow++;

			for (int j = 0; j < numOfAnswers1; j++)
			{
				question.Questions.Add(quizVec[(int)indexRow]);
				indexRow++;
			}

			return question;
		}

		private IQuestion LoadSC(IGame game, List<string> quizVec, uint indexRow, bool isTest)
		{
			string description = quizVec[(int)indexRow];
			indexRow++;

			string answer = quizVec[(int)indexRow];
			indexRow++;

			uint points = uint.Parse(quizVec[(int)indexRow]);
			indexRow++;

			byte numOfAnswers = byte.Parse(quizVec[(int)indexRow]);
			indexRow++;

			SingleChoiceQuestion question = new SingleChoiceQuestion(game.Writer, game.Reader, description, answer, points, isTest);

			for (int i = 0; i < numOfAnswers; i++)
			{
				question.Questions.Add(quizVec[(int)indexRow]);
				indexRow++;
			}

			return question;
		}

		private IQuestion LoadShA(IGame game, List<string> quizVec, uint indexRow, bool isTest)
		{
			string description = quizVec[(int)indexRow];
			indexRow++;

			string answer = quizVec[(int)indexRow];
			indexRow++;

			uint points = uint.Parse(quizVec[(int)indexRow]);
			indexRow++;

			ShortAnswerQuestion question = new ShortAnswerQuestion(game.Writer, game.Reader, description, answer, points, isTest);

			return question;
		}

		private IQuestion LoadTF(IGame game, List<string> quizVec, uint indexRow, bool isTest)
		{
			string description = quizVec[(int)indexRow];
			indexRow++;

			string answer = quizVec[(int)indexRow];
			indexRow++;

			uint points = uint.Parse(quizVec[(int)indexRow]);
			indexRow++;

			TrueOrFalseQuestion question = new TrueOrFalseQuestion(game.Writer, game.Reader, description, answer, points, isTest);

			return question;
		}

		private Quiz LoadQuizHeader(IGame game, List<string> quizVec)
		{
			uint numberOfQuestions = uint.Parse(quizVec[1]);
			string quizName = quizVec[0];
			string userName = quizVec[2];

			Quiz quiz = new Quiz(game.Writer, game.Reader, game.Provider, quizName, userName, 0, numberOfQuestions, 0);

			return quiz;
		}
	}
}
