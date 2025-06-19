namespace Quiz_Master_Game_Play.QuizClass
{
	using Common.Enums;
	using static Common.Constants.GlobalConstants;

	public class QuizIndexDTO
	{
        //id|quizName|userName|quizFileName|QuizStatus|numOfQuestions|Likes

        public uint Id { get; set; }

        public string? QuizName { get; set; }

        public string? UserName { get; set; }

        public string? QuizFileName { get; set; }

        public uint QuizStatus { get; set; }

        public uint NumOfQuestions { get; set; }

        public uint Likes { get; set; }

		public void SetElement(string s)
        {
			List<string> quizVec = s.Split(QUIZ_ELEMENT_DATA_SEPARATOR, StringSplitOptions.RemoveEmptyEntries).ToList();

			this.Id = uint.Parse(quizVec[0]);
			this.QuizName = quizVec[1];
			this.UserName = quizVec[2];
			this.QuizFileName = quizVec[3];
			this.QuizStatus = uint.Parse(quizVec[4]);
			this.NumOfQuestions = uint.Parse(quizVec[5]);
			this.Likes = uint.Parse(quizVec[6]);
		}


		public string ToIndexString() => $"{this.Id}|{this.QuizName}|{this.UserName}|{this.QuizFileName}|{this.QuizStatus}|{this.NumOfQuestions}|{this.Likes}";
	};
}
