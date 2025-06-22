namespace Common.Classes
{
	public static class Date
	{
        public static string DateNow
		{
			get
			{
				DateTime dt = new DateTime();
				return dt.Date.ToString();
			}
		}
    }
}
