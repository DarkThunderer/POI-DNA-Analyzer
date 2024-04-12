using Microsoft.Win32;
using System.IO;

namespace POI_DNA_Analyzer
{
	internal class ResultSaver
	{
		public void Save(string content)
		{
			SaveFileDialog saveFileDialog = new SaveFileDialog();

			if (saveFileDialog.ShowDialog() == true)
			{
				File.WriteAllText(saveFileDialog.FileName, content);
			}
		}
	}
}
