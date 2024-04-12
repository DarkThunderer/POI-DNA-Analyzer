using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace POI_DNA_Analyzer
{
	public partial class MainWindow : Window
	{
		private StreamReader _fileStream;
		private string _filePath = "";
		private ResultText _resultText;

		public MainWindow()
		{
			InitializeComponent();
			_resultText = new ResultText(ResultText);
		}

		private void FindButtonClick(object sender, RoutedEventArgs e)
		{

        }

		private void OpenFileButtonClick(object sender, RoutedEventArgs e)
		{
			FilePicker filePicker = new FilePicker();

			_filePath = filePicker.PickFilePath();

			OpenFile();
		}

		private void SaveFileButtonClick(object sender, RoutedEventArgs e)
		{
			ResultSaver resultSaver = new ResultSaver();
			resultSaver.Save(ResultText.Text);
		}

		private void EnterPromptButtonClick(object sender, RoutedEventArgs e)
		{
			if (_fileStream == null)
				return;

			SequencesFinder sequencesFinder = new SequencesFinder();
			int result = sequencesFinder.GetOccurrencesCount(_fileStream.ReadToEnd(), PromptField.Text);

			_resultText.ShowOccurrencesCount(result.ToString());
			_resultText.ShowOccurrencesIndexes(sequencesFinder.SequenceIndexes);

			OpenFile();
		}

		private void PromptChanged(object sender, TextChangedEventArgs e)
		{

		}

		private void OpenFile()
		{
			if (_fileStream != null)
				_fileStream.Close();

			FileOpener fileOpener = new FileOpener();

			_fileStream = fileOpener.OpenFile(_filePath);
		}
	}
}