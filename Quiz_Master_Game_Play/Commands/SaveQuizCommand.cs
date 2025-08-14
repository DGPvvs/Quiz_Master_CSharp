namespace Quiz_Master_Game_Play.Commands
{
	using Common.Constants;
	using Common.Enums;
	using Quiz_Master_Game_Play.Game.Contract;
	using Quiz_Master_Game_Play.Questions;
	using Quiz_Master_Game_Play.Questions.Contract;
	using Quiz_Master_Game_Play.QuizClass;
	using Quiz_Master_Game_Play.Users;
	using System.Collections.Generic;
	using System.Text;

	public class SaveQuizCommand : LoadQuizCommand
	{
		public SaveQuizCommand(string commandString) : base(commandString)
		{
		}

		public override bool CanExecute(IGame game)
		{
			if (base.CanExecute(game) && (game.Cmd.ParamRange == 3))
			{
				return (uint.TryParse(game.Cmd.Param1, out uint result)
					&& game.Cmd.Param2 != string.Empty);
			}

			return false;
		}

		public override void Execute(IGame game)
		{
			string quizId = game.Cmd.Param1!;
			string fileName = game.Cmd.Param2!;
			Quiz quiz = this.LoadQuiz(game, quizId, false);

			if (quiz != null && quiz.UserName == game.User.UserName)
			{
				string fileData = $"{fileName}{GlobalConstants.FILENAME_SEPARATOR}{quiz.QuizName} - {quiz.NumberOfQuestions} Questions{Environment.NewLine}By: {game.User.Name} {game.User.UserName}{Environment.NewLine}";

				StringBuilder sb = new StringBuilder().Append(fileData);


				for (int i = 0; i < quiz.Questions.Count; i++)
				{
					sb.Append($"{i + 1}) {quiz.Questions[i].ToStringFile()}");
					if ((i + 1) < quiz.Questions.Count)
					{
						sb.AppendLine();
					}
				}

				string s = sb.ToString().TrimEnd();

				game.Provider.Action(ref s, game.User.Id, ProviderOptions.QuizSave);
			}
			else
			{
				game.Writer.WriteLine("The quiz was not recorded.");
			}
		}		
	}
}
