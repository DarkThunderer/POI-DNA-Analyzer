using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace POI_DNA_Analyzer
{
	public partial class MainWindow : Window
	{
		private StreamReader _fileStream;
		private ResultText _resultText;
		private ListOfIndexes _listOfIndexes;
		private SequencesFinder _sequencesFinder;

		private string _filePath = "";

		public MainWindow()
		{
			InitializeComponent();
			_resultText = new ResultText(ResultText);
			_listOfIndexes = new ListOfIndexes(List);
			_sequencesFinder = new SequencesFinder();

			ScottPlotProbabilityGraph dinucleotidesGraph = new ScottPlotProbabilityGraph(WpfPlot1);
			dinucleotidesGraph.Show(new int[]{ 1, 2, 3, 4, 5 }, new double[]{ 0.52, 0.73, 0.28, 0.89, 0.41 }, System.Drawing.Color.Red);
			dinucleotidesGraph.Show(new int[] { 1, 2, 3, 4, 5 }, new double[] { 0.14, 0.67, 0.92, 0.36, 0.75 }, System.Drawing.Color.Green);
			dinucleotidesGraph.Show(new int[] { 1, 2, 3, 4, 5 }, new double[] { 0.81, 0.25, 0.63, 0.49, 0.97 }, System.Drawing.Color.Blue);
			dinucleotidesGraph.Show(new int[] { 1, 2, 3, 4, 5 }, new double[] { 0.33, 0.68, 0.17, 0.94, 0.55 }, System.Drawing.Color.Orange);

			InitializeComponent();
			OxyPlotProbabilityGraph oxyPlotProbabilityGraph = new OxyPlotProbabilityGraph(OxyPlot);
			oxyPlotProbabilityGraph.Show(new int[] { 1, 2, 3, 4, 5 }, new double[] { 0.52, 0.73, 0.28, 0.89, 0.41 }, System.Drawing.Color.Red);
			oxyPlotProbabilityGraph.Show(new int[] { 1, 2, 3, 4, 5 }, new double[] { 0.14, 0.67, 0.92, 0.36, 0.75 }, System.Drawing.Color.Green);
			oxyPlotProbabilityGraph.Show(new int[] { 1, 2, 3, 4, 5 }, new double[] { 0.81, 0.25, 0.63, 0.49, 0.97 }, System.Drawing.Color.Blue);
			oxyPlotProbabilityGraph.Show(new int[] { 1, 2, 3, 4, 5 }, new double[] { 0.33, 0.68, 0.17, 0.94, 0.55 }, System.Drawing.Color.Orange);
			oxyPlotProbabilityGraph.Show();
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
			resultSaver.Save(ResultText.Text, _sequencesFinder.SequenceIndexes);
		}

		private void EnterPromptButtonClick(object sender, RoutedEventArgs e)
		{
			if (_fileStream == null)
				return;

			int result = _sequencesFinder.GetOccurrencesCount(_fileStream.ReadToEnd(), PromptField.Text);

			_resultText.ShowOccurrencesCount(result.ToString());
			_listOfIndexes.ShowOccurrencesIndexes(_sequencesFinder.SequenceIndexes);

			OpenFile();
		}

		private void ClearResultButtonClick(object sender, RoutedEventArgs e)
		{
			_resultText.Clear();
			_listOfIndexes.Clear();
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

		private void f(object sender, RoutedEventArgs e)
		{

        }

		private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
		{

		}
	}
}