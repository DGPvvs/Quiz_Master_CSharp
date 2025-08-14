namespace Quiz_Master_File_DB.BaseProvider
{
	using Common.BaseProvider.Contract;
	using Common.Constants;
	using Common.Enums;

	using static Common.Constants.GlobalConstants;

	public class FileBaseProvider : IBaseProvider
	{
		private void FileSave(string str, bool isFileExist)
		{
			List<string> v = str.Split(GlobalConstants.FILENAME_TO_DATA_SEPARATOR, StringSplitOptions.RemoveEmptyEntries).ToList();

			try
			{
				if (v != null)
				{
					File.WriteAllText(v[0], v[1]);
				}
			}
			catch (FieldAccessException fae)
			{

				throw fae;
			}

		}

		private string FileLoad(string str)
		{
			if (File.Exists(str))
			{
				return File.ReadAllText(str);
			}

			string str1 = $"{str}{FILENAME_TO_DATA_SEPARATOR}{10}{Environment.NewLine}{0}";

			this.FileSave(str1, false);

			return this.FileLoad(str);
		}


		private void FileDelete(string str, ProviderOptions options)
		{
		}

		public void Action(ref string str, uint id, ProviderOptions options)
		{
			if (options == ProviderOptions.ConfigLoad)
			{
				string s = GlobalConstants.CONFIG_FILE_NAME;
				str = this.FileLoad(s);
			}
			else if (options == ProviderOptions.ConfigSave)
			{
				string s = $"{GlobalConstants.CONFIG_FILE_NAME}{GlobalConstants.FILENAME_TO_DATA_SEPARATOR}{str}";
				this.FileSave(s, true);
			}
			else if (options == ProviderOptions.UserFind)
			{
				string s = GlobalConstants.USERS_FILE_NAME;
				str = this.FileLoad(s);
			}
			else if (options == ProviderOptions.NewUserSave)
			{
				string s = $"{GlobalConstants.USERS_FILE_NAME}{GlobalConstants.FILENAME_TO_DATA_SEPARATOR}{str}";
				this.FileSave(s, true);
			}
			else if (options == ProviderOptions.EditUser)
			{
				string s = $"{GlobalConstants.USERS_FILE_NAME}{GlobalConstants.FILENAME_TO_DATA_SEPARATOR}{str}";
				this.FileSave(s, true);
			}
			else if ((options == ProviderOptions.UserLoad) || (options == ProviderOptions.QuizFind) || (options == ProviderOptions.MessagesLoad) || (options == ProviderOptions.QuizLoad))
			{
				string s = str;
				str = this.FileLoad(s);
			}
			else if ((options == ProviderOptions.UserSave) || (options == ProviderOptions.QuizSave) || (options == ProviderOptions.QuizIndexSave) || (options == ProviderOptions.MessagesSave))
			{
				string s = str;
				this.FileSave(s, false);
			}
		}
	}
}
