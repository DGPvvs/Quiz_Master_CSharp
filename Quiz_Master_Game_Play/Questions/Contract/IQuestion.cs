namespace Quiz_Master_Game_Play.Questions.Contract
{
	public interface IQuestion
	{
		uint Action();
		void SetUpData(String dataString);
		string BuildQuestionData();
		string ToStringFile();
	}
}
