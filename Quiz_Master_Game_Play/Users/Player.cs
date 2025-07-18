namespace Quiz_Master_Game_Play.Users
{
	using Common.BaseProvider.Contract;
	using Common.Classes;
	using Common.Constants;
	using Common.Enums;
	using Common.IO.Contract;
	using Quiz_Master_Game_Play.Game.Contract;
	using Quiz_Master_Game_Play.Questions;
	using Quiz_Master_Game_Play.Questions.Contract;
	using Quiz_Master_Game_Play.QuizClass;
	using System.Text;
	using static Common.Constants.GlobalConstants;

	public class Player : User
	{
		private uint level;
		private uint points;
		private uint numberCreatedQuizzes;
		private uint numberLikedQuizzes;
		private uint numberFavoriteQuizzes;
		private uint numberFinishedChallenges;
		private uint numberSolvedTestQuizzes;
		private uint numberSolvedNormalQuizzes;
		private uint numberCreatedQuizzesChallengers;

		private IGame game;

		private List<string> listCreatedQuizzes;
		private List<uint> listLikedQuizzes;
		private List<uint> listFavoriteQuizzes;
		private List<string> listFinishedChallenges;

		public Player(IWriter writer, IReader reader, IBaseProvider provider, UserStruct us, UserOptions uo)
			: base(writer, reader, provider)
		{
			this.game = null;
			this.Init();

			List<string> v = new List<string>();

			this.SetUpUserData(us, ref v, uo);
			this.SaveData();
		}

		public Player(IWriter writer, IReader reader, IBaseProvider provider, IGame game)
			: base(writer, reader, provider)
		{
			this.game = game;
			this.Init();
		}

		public uint Level => this.level;

		public uint Points => this.points;

		public List<string> ListCreatedQuizzes => this.listCreatedQuizzes;

		public override List<uint> ListLikedQuizzes => this.listLikedQuizzes;
		
		public override List<uint> ListFavoriteQuizzes => this.listFavoriteQuizzes;

		public override List<string> ListFinishedChallenges => this.listFinishedChallenges;

		public override uint NumberCreatedQuizzes => this.numberCreatedQuizzes;
        
		public override uint NumberSolvedTestQuizzes
		{
			get => this.numberSolvedTestQuizzes;
			set => this.numberSolvedTestQuizzes = value;
		}

		public override uint NumberSolvedNormalQuizzes
		{
			get => this.numberSolvedNormalQuizzes;
			set => this.numberSolvedNormalQuizzes = value;
		}


		public override uint NumberLikedQuizzes
		{
			get => this.numberLikedQuizzes;
			set => this.numberLikedQuizzes = value;
		}

		public override uint NumberFavoriteQuizzes
		{
			get => this.numberFavoriteQuizzes;
			set => this.numberFavoriteQuizzes = value;
		}

		private void Init()
		{
			this.listCreatedQuizzes = new List<string>();
			this.listLikedQuizzes = new List<uint>();
			this.listFavoriteQuizzes = new List<uint>();
			this.listFinishedChallenges = new List<string>();
		}

		public uint PointsForLevel()
		{
			List<uint> levelKeys = GlobalConstants.listPointForLevel
				.Keys
				.Take(GlobalConstants.listPointForLevel.Count - 1)
				.OrderBy(k => k)
				.ToList();

			foreach (var key in levelKeys)
			{
				if (this.level < key)
				{
					return GlobalConstants.listPointForLevel[key];
				}
			}

			return GlobalConstants.listPointForLevel
				.Values
				.Last();
		}

		public override void SaveData()
		{
			string s = this.BuildUserData();
			this.Provider.Action(ref s, ProviderOptions.UserSave);
		}

		public override string BuildUserData()
		{
			StringBuilder sb = new StringBuilder();
			sb.Append(base.BuildUserData());

			this.numberCreatedQuizzes = (uint)this.listCreatedQuizzes.Count;
			this.numberLikedQuizzes = (uint)this.listLikedQuizzes.Count;
			this.numberFavoriteQuizzes = (uint)this.listFavoriteQuizzes.Count;
			this.numberFinishedChallenges = (uint)this.listFinishedChallenges.Count;

			sb.AppendLine(this.level.ToString());
			sb.AppendLine(this.points.ToString());
			sb.AppendLine(this.numberCreatedQuizzes.ToString());
			sb.AppendLine(this.numberLikedQuizzes.ToString());
			sb.AppendLine(this.numberFavoriteQuizzes.ToString());
			sb.AppendLine(this.numberFinishedChallenges.ToString());
			sb.AppendLine(this.numberSolvedTestQuizzes.ToString());
			sb.AppendLine(this.numberSolvedNormalQuizzes.ToString());
			sb.AppendLine(this.numberCreatedQuizzesChallengers.ToString());

			for (int i = 0; i < this.listCreatedQuizzes.Count; i++)
			{
				sb.AppendLine(this.listCreatedQuizzes[i]);
			}

			for (int i = 0; i < this.listLikedQuizzes.Count; i++)
			{
				sb.AppendLine(this.listLikedQuizzes[i].ToString());
			}

			for (int i = 0; i < this.listFavoriteQuizzes.Count; i++)
			{
				sb.AppendLine(this.listFavoriteQuizzes[i].ToString());
			}

			for (int i = 0; i < this.listFinishedChallenges.Count; i++)
			{
				sb.AppendLine(this.listFinishedChallenges[i]);
			}

			return sb.ToString();
		}

		public override void SetUpUserData(UserStruct us, ref List<string> v, UserOptions uo)
		{
			base.SetUpUserData(us, ref v, uo);

			if (uo.HasFlag(UserOptions.NewUserCreated))
			{
				this.level = 0;
				this.points = 0;
				this.numberCreatedQuizzes = 0;
				this.numberLikedQuizzes = 0;
				this.numberFavoriteQuizzes = 0;
				this.numberFinishedChallenges = 0;
				this.numberSolvedTestQuizzes = 0;
				this.numberSolvedNormalQuizzes = 0;
				this.numberCreatedQuizzesChallengers = 0;
				return;
			}

			this.level = uint.Parse(v[2]);
			this.points = uint.Parse(v[3]);
			this.numberCreatedQuizzes = uint.Parse(v[4]);
			this.numberLikedQuizzes = uint.Parse(v[5]);
			this.numberFavoriteQuizzes = uint.Parse(v[6]);
			this.numberFinishedChallenges = uint.Parse(v[7]);
			this.numberSolvedTestQuizzes = uint.Parse(v[8]);
			this.numberSolvedNormalQuizzes = uint.Parse(v[9]);
			this.numberCreatedQuizzesChallengers = uint.Parse(v[10]);

			int i = 11;
			int j = i;

			for (; i < j + this.numberCreatedQuizzes; ++i)
			{
				this.listCreatedQuizzes.Add(v[i]);
			}

			j = i;

			for (; i < j + this.numberLikedQuizzes; ++i)
			{
				this.listLikedQuizzes.Add(uint.Parse(v[i]));
			}

			j = i;

			for (; i < j + this.numberFavoriteQuizzes; ++i)
			{
				this.listFavoriteQuizzes.Add(uint.Parse(v[i]));
			}

			j = i;

			for (; i < j + this.numberFinishedChallenges; ++i)
			{
				this.listFinishedChallenges.Add(v[i]);
			}

			List<string> quizzesVec =  this.Quiz
				.FindAllQuizzes()
				.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries)
				.ToList();

			for (int k = 0; k < quizzesVec.Count; k++)
			{
				QuizIndexDTO qiDTO = new QuizIndexDTO();

				qiDTO.SetElement(quizzesVec[k]);

				bool isAddedNewQuiz = !this.ContainCreatedQuizzes(qiDTO.Id)
					&& (qiDTO.QuizStatus == QuizStatus.ApprovedQuiz)
					&& (qiDTO.UserName == this.UserName);

				if (isAddedNewQuiz)
				{
					string createdQuiz = $"{qiDTO.Id.ToString()}{CREATED_QUIZ_SEPARATOR_STRING}{qiDTO.QuizName}";

					this.listCreatedQuizzes.Add(createdQuiz);
					this.numberCreatedQuizzes = (uint)this.listCreatedQuizzes.Count;

					this.AddQuizChallenge(ChallengerOptions.CreateChallenger);
				}
			}

			/*0 <firstName>
			1 <lastName>
			2 <level>
			3 <points>
			4 <numberCreatedQuizzes>
			5 <numberLikedQuizzes>
			6 <numberFavoriteQuizzes>
			7 <numberFinishedChallenges>
			8 <numberSolvedTestQuizzes>
			9 <numberSolvedNormalQuizzes>
		   10 <numberCreatedQuizzesChallengers>
		   <numberCreatedQuizzes lines of created quizes in format>:
		   <quizId>#<quizName>
		   .
		   .
		   .
		   <numberLikedQuizzes lines of liked quizzes in format>:
		   <quizId>
		   .
		   .
		   .
		   <numberFavoriteQuizzes lines of favorite quizzes in format>:
		   <quizId>
		   .
		   .
		   .
		   <numberFinishedChallenges lines of finished challenges in format>
		   <<data>|<text challenges>>
		   .
		   .
		   .*/

		}

		public override void AddQuizChallenge(ChallengerOptions co)
		{
			uint point = 0;

			if (co == ChallengerOptions.CreateChallenger)
			{
				int createdQuizCount = this.listCreatedQuizzes.Count;

				bool isChalleeng = (createdQuizCount < 31) && (createdQuizCount % 5 == 0);

				if (isChalleeng)
				{
					point = (uint)createdQuizCount * 10 / 2;

					string message = $"{this.Id}{MESSAGE_ELEMENT_SEPARATOR}New challenge complited! You create { createdQuizCount} quizzes! {point} points added.";

					this.Message.SendMessage(message);

					string finishedChaleng = $"{Common.Classes.Date.DateNow}{MESSAGE_ELEMENT_SEPARATOR}Commplete {createdQuizCount} create quizzes";

					this.listFinishedChallenges.Add(finishedChaleng);
					this.numberFinishedChallenges = (uint)this.listFinishedChallenges.Count;
					this.numberCreatedQuizzesChallengers++;
				}
			}
			else if (co == ChallengerOptions.NormalQuizChallenger)
			{
				int normalQuizCount = (int)this.numberSolvedNormalQuizzes;

				bool isChalleeng = (normalQuizCount < 101) && (normalQuizCount % 10 == 0);

				if (isChalleeng)
				{
					point = (uint)normalQuizCount * 10 / 3;

					string message = $"{this.Id}{MESSAGE_ELEMENT_SEPARATOR} New challenge complited! You solved {normalQuizCount} quizzes in normal mode! {point} points added.";

					this.Message.SendMessage(message);

					string finishedChaleng = $"{Common.Classes.Date.DateNow}{MESSAGE_ELEMENT_SEPARATOR}Complete {normalQuizCount} quizzes in normal mode";

					this.listFinishedChallenges.Add(finishedChaleng);
					this.numberFinishedChallenges = (uint)this.listFinishedChallenges.Count;
				}
			}
			else if (co == ChallengerOptions.TestQuizChallenger)
			{
				int testQuizCount = (int)this.numberSolvedTestQuizzes;

				bool isChalleeng = (testQuizCount < 101) && (testQuizCount % 10 == 0);

				if (isChalleeng)
				{
					point = (uint)testQuizCount * 10 / 3;

					string message = $"{this.Id}{MESSAGE_ELEMENT_SEPARATOR}New challenge complited! You solved {testQuizCount} quizzes in test mode! {point} points added.";

					this.Message.SendMessage(message);

					string finishedChaleng = $"{Common.Classes.Date.DateNow}{MESSAGE_ELEMENT_SEPARATOR}Commplete {testQuizCount} quizzes in test mode";

					this.listFinishedChallenges.Add(finishedChaleng);
				}
			}

			this.AddPoints(point);
		}

		public override void AddPoints(uint point)
		{
			this.points += point;

			this.AddLevel();
		}

		public void AddLevel()
		{
			uint pointForLevel = this.PointsForLevel();

			if (pointForLevel - this.points <= 0)
			{
				this.level++;
				this.points -= pointForLevel;

				string message = $"Level {this.level} reached!";

				this.Message.SendMessage(message);
			}
		}


		public Quiz LoadQuiz(string idString, bool isTest)
		{
			uint id = uint.Parse(idString);
			string quizString = ERROR;
			Quiz quiz = null!;			

			List<string> quizzesVec = this
				.Quiz
				.FindAllQuizzes()
				.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries)
				.ToList();

			for (int i = 0; i < quizzesVec.Count; i++)
			{
				QuizIndexDTO qiDTO = new QuizIndexDTO();
				string quizElement = quizzesVec[i];
				qiDTO.SetElement(quizElement);

				if (id == qiDTO.Id && qiDTO.QuizStatus == QuizStatus.ApprovedQuiz)
				{
					quizString = qiDTO.QuizFileName;
					this.Provider.Action(ref quizString, ProviderOptions.QuizLoad);
					break;
				}
			}

			if (quizString != ERROR)
			{
				List<string> quizVec = quizString
					.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries)
					.ToList();

				quiz = this.LoadQuizHeader(quizVec);
				quiz.Id = id;
				uint indexRow = 3;

				for (int i = 0; i < quiz.NumberOfQuestions; i++)
				{
					IQuestion? question = null;

					uint questionType = uint.Parse(quizVec[(int)indexRow]);
					indexRow++;

					if (questionType == (uint)QuestionType.TF)
					{
						question = this.LoadTF(quizVec, indexRow, isTest);
					}
					else if (questionType == (uint)QuestionType.ShA)
					{
						question = this.LoadShA(quizVec, indexRow, isTest);
					}
					else if (questionType == (uint)QuestionType.SC)
					{
						question = this.LoadSC(quizVec, indexRow, isTest);
					}
					else if (questionType == (uint)QuestionType.MP)
					{
						question = this.LoadMP(quizVec, indexRow, isTest);
					}
					else if (questionType == (uint)QuestionType.MC)
					{
						question = this.LoadMC(quizVec, indexRow, isTest);
					}

					quiz.Questions.Add(question!);
				}
			}

			return quiz!;
		}

		public Quiz LoadQuizHeader(List<string> v)
		{
			uint numberOfQuestions = uint.Parse(v[1]);
			string quizName = v[0];
			string userName = v[2];

			Quiz quiz = new Quiz(this.Writer, this.Reader, this.Provider, quizName, userName, 0, numberOfQuestions, 0);

			return quiz;
		}

		public IQuestion LoadTF(List<string> quizVec, uint indexRow, bool isTest)
		{
			string description = quizVec[(int)indexRow];
			indexRow++;

			string answer = quizVec[(int)indexRow];
			indexRow++;

			uint points = uint.Parse(quizVec[(int)indexRow]);
			indexRow++;

			TrueOrFalseQuestion question = new TrueOrFalseQuestion(this.Writer, this.Reader, description, answer, points, isTest);

			return question;
		}

		public IQuestion LoadMP(List<string> quizVec, uint indexRow, bool isTest)
		{
			string description = quizVec[(int)indexRow];
			indexRow++;

			string answer = quizVec[(int)indexRow];
			indexRow++;

			uint points = uint.Parse(quizVec[(int)indexRow]);
			indexRow++;

			byte numOfAnswers = byte.Parse(quizVec[(int)indexRow]);
			indexRow++;

			MatchingPairsQuestion question = new MatchingPairsQuestion(this.Writer, this.Reader, description, answer, points, isTest, numOfAnswers);

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

		public IQuestion LoadShA(List<string> quizVec, uint indexRow, bool isTest)
		{
			string description = quizVec[(int)indexRow];
			indexRow++;

			string answer = quizVec[(int)indexRow];
			indexRow++;

			byte points = byte.Parse(quizVec[(int)indexRow]);
			indexRow++;

			ShortAnswerQuestion question = new ShortAnswerQuestion(this.Writer, this.Reader, description, answer, points, isTest);

			return question;
		}

		public IQuestion LoadMC(List<string> quizVec, uint indexRow, bool isTest)
		{
			string description = quizVec[(int)indexRow];
			indexRow++;

			string answer = quizVec[(int)indexRow];
			indexRow++;

			byte points = byte.Parse(quizVec[(int)indexRow]);
			indexRow++;

			byte numOfAnswers = byte.Parse(quizVec[(int)indexRow]);
			indexRow++;

			MultipleChoiceQuestion question = new MultipleChoiceQuestion(this.Writer, this.Reader, description, answer, points, isTest, numOfAnswers);

			for (int i = 0; i < numOfAnswers; i++)
			{
				question.Questions.Add(quizVec[(int)indexRow]);
				indexRow++;
			}

			return question;
		}

		public IQuestion LoadSC(List<string> quizVec, uint indexRow, bool isTest)
		{
			string description = quizVec[(int)indexRow];
			indexRow++;

			string answer = quizVec[(int)indexRow];
			indexRow++;

			byte points = byte.Parse(quizVec[(int)indexRow]);
			indexRow++;

			byte numOfAnswers = byte.Parse(quizVec[(int)indexRow]);
			indexRow++;

			SingleChoiceQuestion question = new SingleChoiceQuestion(this.Writer, this.Reader, description, answer, points, isTest);

			for (int i = 0; i < numOfAnswers; i++)
			{
				question.Questions.Add(quizVec[(int)indexRow]);
				indexRow++;
			}

			return question;
		}

		public override void Help()
		{
			base.Help();

			foreach (var command in GlobalConstants.listPlayerCommands)
			{
				this.Writer.WriteLine(command);
			}	
		}

		public bool ContainCreatedQuizzes(uint quizId)
		{
			for (int i = 0; i < this.listCreatedQuizzes.Count; i++)
			{
				List<string> v = this
					.listCreatedQuizzes[i]
					.Split(CREATED_QUIZ_SEPARATOR, StringSplitOptions.RemoveEmptyEntries)
					.ToList();

				if (uint.Parse(v[0]) == quizId)
				{
					return true;
				}
			}

			return false;
		}

		public bool ContainLikedQuizzes(uint quizId)
		{
			for (int i = 0; i < this.listCreatedQuizzes.Count; i++)
			{
				List<string> v = this
					.listCreatedQuizzes[i]
					.Split(CREATED_QUIZ_SEPARATOR, StringSplitOptions.RemoveEmptyEntries)
					.ToList();

				if (uint.Parse(v[0]) == quizId)
				{
					return true;
				}
			}

			return false;
		}

		public override uint[] GetOrder(bool isShuffle, uint numberOfQuestions)
		{
			uint[] arr = new uint[numberOfQuestions];
			for (int i = 0; i < numberOfQuestions; i++)
			{
				arr[i] = (uint)i;
			}

			if (!isShuffle)
			{
				return arr;
			}

			Random rand = new Random();
			uint questionSize = numberOfQuestions;

			uint[] arr1 = new uint[numberOfQuestions];

			for (int i = 0; i < numberOfQuestions; i++)
			{
				int randomIndex = rand.Next((int)questionSize);

				arr1[i] = arr[randomIndex];
				arr[randomIndex] = arr[--questionSize];
			}

			return arr1;
		}		
	}
}
