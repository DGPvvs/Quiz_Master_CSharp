namespace Common.BaseProvider.Contract
{
	using Common.Enums;

	public interface IBaseProvider
	{
		public void Action(ref string str, uint id, ProviderOptions options);
	}
}
