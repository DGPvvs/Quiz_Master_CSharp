namespace Quiz_Master_Game_Play.Commands
{
	using Common.Enums;
	using Quiz_Master_Game_Play.Game.Contract;
	using Quiz_Master_Game_Play.QuizClass;
	using Quiz_Master_Game_Play.Users;
	using System.Numerics;

	public class UnlikeQuizCommand : Command
	{
		public UnlikeQuizCommand(string commandString) : base(commandString)
		{
		}

		public override bool CanExecute(IGame game)
		{
			if ((game.User is Player) && (game.Cmd.Command == this.CommandString) && (game.Cmd.ParamRange == 2))
			{
				return true;
			}

			return false;
		}

		public override void Execute(IGame game)
		{
			uint id = uint.Parse(game.Cmd.Param1!);

			if (this.ContainLikedQuizzes(id, game))
			{
				List<uint> v = new List<uint>();

				foreach (var item in game.User.ListLikedQuizzes)
				{
					if (item != id)
					{
						v.Add(item);
					}
				}

				game.User.ListLikedQuizzes.Clear();

				foreach (var item in v)
				{
					game.User.ListLikedQuizzes.Add(item);
				}

				game.User.NumberLikedQuizzes = (uint)game.User.ListLikedQuizzes.Count;

				List<string> quizzesVec = game
					.User
					.Quiz
					.FindAllQuizzes()
					.Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries)
					.ToList();

				foreach (var item in quizzesVec)
				{
					QuizIndexDTO qiDTO = new QuizIndexDTO();
					qiDTO.SetElement(item);

					if (qiDTO.Id == id)
					{
						game.User.Quiz.SaveQuiz(QuizStatus.UnlikeQuiz, qiDTO.Id);
						return;
					}
				}
			}
			else
			{
				game.Writer.WriteLine("No test with the specified ID was found to be liked.");
			}
		}

		private bool ContainLikedQuizzes(uint id, IGame game)
		{
			foreach (var item in game.User.ListLikedQuizzes)
			{
				if (item == id)
				{
					return true;
				}
			}

			return false;
		}
	}
}
