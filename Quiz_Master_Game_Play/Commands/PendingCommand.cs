namespace Quiz_Master_Game_Play.Commands
{
	using Common.Constants;
	using Common.Enums;
	using Quiz_Master_Game_Play.Game.Contract;
	using Quiz_Master_Game_Play.Users;
	using System.Numerics;

	public class PendingCommand : Command
	{
		public PendingCommand(string commandString) : base(commandString)
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
			string s = game.User.Quiz.FindAllQuizzes();

			List<string> quizzesVec = s
				.Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries)
				.ToList();

			foreach (string quizz in quizzesVec)
			{
				List<string> quizVec = quizz
					.Split(GlobalConstants.QUIZ_ELEMENT_DATA_SEPARATOR, StringSplitOptions.RemoveEmptyEntries)
					.ToList();

				QuizStatus qs = (QuizStatus)Enum.Parse(typeof(QuizStatus), quizVec[4]);

				if (qs == QuizStatus.NewQuiz || qs == QuizStatus.EditQuiz)
				{
					string output = $"[id {quizVec[0]}] {quizVec[1]} by {quizVec[2]}";
					game.Writer.WriteLine(output);
				}
			}
		}
	}
}
