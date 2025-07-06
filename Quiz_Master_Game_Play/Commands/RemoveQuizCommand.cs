namespace Quiz_Master_Game_Play.Commands
{
	using Common.Enums;

	public class RemoveQuizCommand : RejectOrRemoveQuizCommand
	{
		public RemoveQuizCommand(string commandString) : base(commandString)
		{
			this.status = QuizStatus.RemovedQuiz;
		}
	}
}
