namespace Common.Classes
{
	public static class Date
	{
        public static string DateNow
		{
			get
			{
				DateTime dt = DateTime.Now;
				return dt.Date.ToString("dd/MM/yyyy");
			}
		}
    }
}
