using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace POI_DNA_Analyzer
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		private StreamReader _fileStream;
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
			FileOpener fileOpener = new FileOpener();

			_fileStream = fileOpener.OpenFile(filePicker.PickFilePath());
		}

		private void EnterPromptButtonClick(object sender, RoutedEventArgs e)
		{
			if (_fileStream == null)
				return;

			SequencesFinder sequencesFinder = new SequencesFinder();
			int result = sequencesFinder.CountOccurrences(_fileStream.ReadToEnd(), PromptField.Text);

			_resultText.ShowOccurrencesCount(result.ToString());
		}

		private void PromptChanged(object sender, TextChangedEventArgs e)
		{

		}
	}
}