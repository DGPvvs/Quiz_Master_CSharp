namespace Quiz_Master_Game_Play.Commands
{
	using Common.Enums;

	public class RejectQuizCommand : RejectOrRemoveQuizCommand
	{
		public RejectQuizCommand(string commandString) : base(commandString)
		{
			this.status = QuizStatus.RejectedQuiz;
		}
	}
}
