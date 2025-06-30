namespace Quiz_Master_Game_Play.Commands
{
	using Common.Enums;
	using Quiz_Master_Game_Play.Game.Contract;
	using Quiz_Master_Game_Play.QuizClass;
	using Quiz_Master_Game_Play.Users;
	using System.Numerics;
	using System.Text;

	public class QuizzesCommand : Command
	{
		private IGame game;
		private string userName;

		public QuizzesCommand(string commandString) : base(commandString)
		{
			this.game = null!;
			this.userName = string.Empty;
		}

		public override bool CanExecute(IGame game)
		{
			bool result = (game.User is Player) && (game.Cmd.Command == this.CommandString) && ((game.Cmd.ParamRange == 2) || (game.Cmd.ParamRange == 1));

			if (result)
			{
				if (game.Cmd.ParamRange == 2)
				{
					this.userName = game.Cmd.Param1!;
				}

				return true;
			}

			return false;			
		}

		public override void Execute(IGame game)
		{
			string s = game.User.Quiz.FindAllQuizzes();

			List<string> quizzesVec = s
				.Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries)
				.ToList();

			StringBuilder sb = new StringBuilder();

			foreach (string quizz in quizzesVec)
			{
				QuizIndexDTO qiDTO = new QuizIndexDTO();

				qiDTO.SetElement(quizz);
				bool flag = false;

				if (qiDTO.QuizStatus == QuizStatus.ApprovedQuiz)
				{
					if (userName == string.Empty || userName == qiDTO.UserName)
					{
						flag = true;
					}
					else
					{
						flag = false;
					}
				}				

				if (flag)
				{
					sb.AppendLine($"{qiDTO.Id} | {qiDTO.QuizName} | {qiDTO.UserName} | {qiDTO.NumOfQuestions} Questions | {qiDTO.Likes} likes");
					//id|quizName|useName|quizFileName|QuizStatus|numOfQuestions|Likes
				}				
			}

			game.Writer.WriteLine(sb.ToString().TrimEnd());
		}
	}
}
