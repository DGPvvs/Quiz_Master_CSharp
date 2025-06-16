namespace Common.BaseProvider.Contract
{
	using Common.Enums;

	public interface IBaseProvider
	{
		public void Action(string str, ProviderOptions options);
	}
}
