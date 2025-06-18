namespace Quiz_Master_Game_Play.QuizClass
{
	using Common.BaseProvider.Contract;
	using Common.Classes;
	using Common.IO.Contract;
	using Quiz_Master_Game_Play.Users;
	using System.Numerics;
	using Quiz_Master_Game_Play.Questions.Contract;
	using Common.Enums;

	using static Common.Constants.GlobalConstants;

	class Quiz
	{
		private uint id;
		private uint numberOfQuestions;
		private uint numberOfLikes;
		private string quizName;
		private string userName;

		private IWriter writer;
		private IReader reader;
		private IBaseProvider provider;

		private List<IQuestion> questions;

		public Quiz(IWriter writer, IReader reader, IBaseProvider provider, string name, string userName, uint id, uint numberOfQuestions, uint numberOfLikes)
		{
			this.writer = writer;
			this.reader = reader;
			this.provider = provider;
			this.quizName = name;
			this.userName = userName;
			this.id = id;
			this.numberOfQuestions = numberOfQuestions;
			this.numberOfLikes = numberOfLikes;

			this.questions = new List<IQuestion>();
		}

		public string QuizName => this.quizName;

		public string UserName() => this.userName;

		public string GetUserFullName()
		{
			UserStruct us = new UserStruct();

			us.UserName = userName;

			User user = new User(this.writer, this.reader, this.provider);

			user.FindUserData(us, NOT_EXSIST);

			return $"{us.FirstName} {us.LastName}";
		}

		public uint NumberOfQuestions => this.numberOfQuestions;

		public uint Id
		{
			get => this.id;
			set => this.id = value;
		}

		public uint Likes => this.numberOfLikes;

		public void IncrementLikes(int likes)
		{
			if ((this.numberOfLikes > 0 && likes < 0) || likes > 0)
			{
				this.numberOfLikes += (uint)likes;
			}
		}

		public List<IQuestion> Questions => this.questions;

		public void SaveQuiz(QuizStatus qs, uint quizId)
		{
			string quizFileName = string.Empty;
			string s = this.FindAllQuizzes();

			if (qs == QuizStatus.NewQuiz)
			{
				List<string> v = s.Split(ROW_DATA_SEPARATOR, StringSplitOptions.RemoveEmptyEntries).ToList();

				string separator = QUIZ_ELEMENT_DATA_SEPARATOR.ToString();
				quizFileName = $"{this.Id.ToString()}Quiz.txt";

				string newQuizString = $"{this.Id}{separator}{this.QuizName}{separator}{this.UserName}{separator}{quizFileName}";
				newQuizString = $"{newQuizString}{separator}{qs}{separator}{this.NumberOfQuestions}";
				newQuizString = $"{newQuizString}{separator}{this.Likes}";

				//id|quizName|useName|quizFileName|QuizStatus|numOfQuestions|Likes

				v.Add(newQuizString);

				string allQuizzesString = string.Join(ROW_DATA_SEPARATOR, v);

				allQuizzesString = $"{QUIZZES_FILE_NAME}{FILENAME_TO_DATA_SEPARATOR}{allQuizzesString}";

				this.provider.Action(ref allQuizzesString, ProviderOptions.QuizIndexSave);
			}
			else if (qs == QuizStatus.ApprovedQuiz)
			{
				List<String> resultVec = new List<string>();

				List<string> quizzesVec = s
					.Split(ROW_DATA_SEPARATOR, StringSplitOptions.RemoveEmptyEntries)
					.ToList();

				for (int i = 0; i < quizzesVec.Count; i++)
				{
					string quizString = quizzesVec[i];

					List<string> quizVec = quizString
						.Split(QUIZ_ELEMENT_DATA_SEPARATOR, StringSplitOptions.RemoveEmptyEntries)
						.ToList();

					uint id = uint.Parse(quizVec[0]);

					if (id == quizId)
					{
						string approveString = $"{quizVec[0]}{QUIZ_ELEMENT_SEPARATOR}{quizVec[1]}{QUIZ_ELEMENT_SEPARATOR}{quizVec[2]}{QUIZ_ELEMENT_SEPARATOR}{quizVec[3]}";
						approveString = $"{approveString}{QUIZ_ELEMENT_SEPARATOR}{QuizStatus.ApprovedQuiz}{QUIZ_ELEMENT_SEPARATOR}{quizVec[5]}{QUIZ_ELEMENT_SEPARATOR}{quizVec[6]}";

						resultVec.Add(approveString);
					}
					else
					{
						resultVec.Add(quizString);
					}
				}

				string allQuizzesString = string.Join(ROW_DATA_SEPARATOR, resultVec);

				allQuizzesString = $"{QUIZZES_FILE_NAME}{FILENAME_TO_DATA_SEPARATOR}{allQuizzesString}";

				this.provider.Action(ref allQuizzesString, ProviderOptions.QuizIndexSave);

				//id|quizName|userName|quizFileName|QuizStatus|numOfQuestions|Likes
			}
			else if (qs == QuizStatus.LikeQuiz)
			{
				Vector<String> quizzesVec, resultVec;

				String::Split(ROW_DATA_SEPARATOR, quizzesVec, s);

				for (size_t i = 0; i < quizzesVec.getSize(); i++)
				{
					String quizString = quizzesVec[i];

					QuizIndexDTO qiDTO;

					qiDTO.SetElement(quizString);

					if (qiDTO.id == quizId)
					{
						qiDTO.likes++;
						String incLikeString = qiDTO.ToIndexString();
						resultVec.push_back(incLikeString);
					}
					else
					{
						resultVec.push_back(quizString);
					}
				}

				String allQuizzesString;
				String::Join(ROW_DATA_SEPARATOR, resultVec, allQuizzesString);

				allQuizzesString = QUIZZES_FILE_NAME + FILENAME_SEPARATOR + allQuizzesString;

				this->provider->Action(allQuizzesString, ProviderOptions::QuizzeIndexSave);
			}
			else if (qs == QuizStatus::UnlikeQuiz)
			{
				Vector<String> quizzesVec, resultVec;

				String::Split(ROW_DATA_SEPARATOR, quizzesVec, s);

				for (size_t i = 0; i < quizzesVec.getSize(); i++)
				{
					String quizString = quizzesVec[i];

					QuizIndexDTO qiDTO;

					qiDTO.SetElement(quizString);

					if (qiDTO.id == quizId)
					{
						qiDTO.likes--;
						String incLikeString = qiDTO.ToIndexString();
						resultVec.push_back(incLikeString);
					}
					else
					{
						resultVec.push_back(quizString);
					}
				}

				String allQuizzesString;
				String::Join(ROW_DATA_SEPARATOR, resultVec, allQuizzesString);

				allQuizzesString = QUIZZES_FILE_NAME + FILENAME_SEPARATOR + allQuizzesString;

				this->provider->Action(allQuizzesString, ProviderOptions::QuizzeIndexSave);
			}

			if (qs == QuizStatus::NewQuiz)
			{
				char* arr1 = new char[2] { '\0' };
				arr1[0] = FILENAME_TO_DATA_SEPARATOR;

				String allQuizData = quizFileName + String(arr1) + this->GetQuizName() + NEW_LINE + String::UIntToString(this->GetNumberOfQuestions());
				allQuizData += NEW_LINE + this->GetUserName() + NEW_LINE;

				for (size_t i = 0; i < this->GetNumberOfQuestions(); i++)
				{
					allQuizData += this->GetQuestions()[i]->BuildQuestionData();
				}

				this->provider->Action(allQuizData, ProviderOptions::QuizzeSave);

				arr1[0] = QUOTES_DATA_SEPARATOR;
				this->writer->WriteLine("Quiz " + String(arr1) + this->GetQuizName() + String(arr1) + " with ID " + String::UIntToString(this->GetId()) + " sent for admin approval!");

			}
		}

		public string FindAllQuizzes()
		{
			String s = QUIZZES_FILE_NAME;

			this->provider->Action(s, ProviderOptions::QuizzeFind);

			if (s == ERROR)
			{
				s = EMPTY_STRING;
			}

			return s;
		}
	}
}
