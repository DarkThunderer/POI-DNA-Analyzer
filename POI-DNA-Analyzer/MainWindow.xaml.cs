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
		private DinucleotidesAnalyzer _dinucleotidesAnalyzer;
		private OxyPlotProbabilityGraph _oxyPlotProbabilityGraph;

		private string _filePath = "";
		private string _currentDinucleotide = "A";
		private int _chunkSize;

		public MainWindow()
		{
			InitializeComponent();

			_resultText = new ResultText(ResultText);
			_listOfIndexes = new ListOfIndexes(List);
			_sequencesFinder = new SequencesFinder();
			_dinucleotidesAnalyzer = new DinucleotidesAnalyzer();
			_oxyPlotProbabilityGraph = new OxyPlotProbabilityGraph(OxyPlot);
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
			SequenceFinderResultSaver resultSaver = new SequenceFinderResultSaver();
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

		private void CheckboxChecked(object sender, RoutedEventArgs e)
		{

		}

		private void SavePathChanged(object sender, TextChangedEventArgs e)
		{

		}

		private void SaveDinucleotidesAnalyzerButtonClick(object sender, RoutedEventArgs e)
		{
			DinucleotidesAnalyzerResultSaver resultSaver = new DinucleotidesAnalyzerResultSaver(_dinucleotidesAnalyzer);

			resultSaver.Save();
		}

		private void StartDinucleotidesAnalyzerButtonClick(object sender, RoutedEventArgs e)
		{
			int defaultChunkSize = 100;

			if (int.TryParse(ChunkSizeTextBox.Text, out _chunkSize))
			{

			}
			else
			{
				_chunkSize = defaultChunkSize;
			}

			ShowGraph();
		}

		private void ShowGraph(object sender, RoutedEventArgs e)
		{
			_currentDinucleotide = ((Button)sender).Tag.ToString();
			ShowGraph();
		}

		private void ShowGraph()
		{
			if (_fileStream == null)
				return;

			_dinucleotidesAnalyzer.Analyze(_fileStream, _chunkSize, SimilaritySlider.Value);
			_oxyPlotProbabilityGraph.Clear();

			if (_currentDinucleotide.Length == 1)
				ShowNN();
			else
				ShowN();

			_oxyPlotProbabilityGraph.Show();
			OpenFile();
		}

		private void ShowNN()
		{
			List<System.Drawing.Color> listOfColors = new List<System.Drawing.Color>()
			{
				System.Drawing.Color.Red,
				System.Drawing.Color.Green,
				System.Drawing.Color.Blue,
				System.Drawing.Color.Orange,
			};

			int i = 0;

			foreach (string key in _dinucleotidesAnalyzer.DinucleotidesProbabilities.Keys.ToList())
			{
				if (key[0].ToString() == _currentDinucleotide[0].ToString())
				{
					_oxyPlotProbabilityGraph.ProvideData(_dinucleotidesAnalyzer.Indexes, _dinucleotidesAnalyzer.DinucleotidesProbabilities[key], listOfColors[i], key);
					i++;
				}
			}
		}

		private void ShowN()
		{
			_oxyPlotProbabilityGraph.ProvideData(_dinucleotidesAnalyzer.Indexes, _dinucleotidesAnalyzer.DinucleotidesProbabilities[_currentDinucleotide], System.Drawing.Color.Red, _currentDinucleotide);
		}
	}
}