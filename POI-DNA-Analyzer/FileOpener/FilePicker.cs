using Microsoft.Win32;

namespace POI_DNA_Analyzer
{
	internal class FilePicker
    {
        public string PickFilePath()
        {
			OpenFileDialog openFileDialog = new OpenFileDialog();

			if (openFileDialog.ShowDialog() == true)
				return openFileDialog.FileName;
			else
				return "";
		}
    }
}
