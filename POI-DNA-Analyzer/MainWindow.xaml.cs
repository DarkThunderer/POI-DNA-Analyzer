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
		private DinucleotidesAnalyzer _dinucleotidesAnalzyer;
		private OxyPlotProbabilityGraph _oxyPlotProbabilityGraph;

		private string _filePath = "";
		private string _currentNucleotide = "A";
		private int _chunkSize;

		public MainWindow()
		{
			InitializeComponent();

			_resultText = new ResultText(ResultText);
			_listOfIndexes = new ListOfIndexes(List);
			_sequencesFinder = new SequencesFinder();
			_dinucleotidesAnalzyer = new DinucleotidesAnalyzer();
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

		private void CheckboxChecked(object sender, RoutedEventArgs e)
		{

		}

		private void SavePathChanged(object sender, TextChangedEventArgs e)
		{

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

			ShowNN();
		}

		private void ShowNN()
		{
			if (_fileStream == null)
				return;

			_dinucleotidesAnalzyer.Analyze(_fileStream, _chunkSize, SimilaritySlider.Value);

			_oxyPlotProbabilityGraph.Clear();

			List<System.Drawing.Color> listOfColors = new List<System.Drawing.Color>()
			{
				System.Drawing.Color.Red,
				System.Drawing.Color.Green,
				System.Drawing.Color.Blue,
				System.Drawing.Color.Orange,
			};

			int i = 0;

			foreach (string key in _dinucleotidesAnalzyer.DinucleotidesProbabilities.Keys.ToList())
			{
				if (key[0].ToString() == _currentNucleotide)
				{
					_oxyPlotProbabilityGraph.ProvideData(_dinucleotidesAnalzyer.Indexes, _dinucleotidesAnalzyer.DinucleotidesProbabilities[key], listOfColors[i], key);
					i++;
				}
			}

			_oxyPlotProbabilityGraph.Show();
			OpenFile();
		}

		private void ShowAN(object sender, RoutedEventArgs e)
		{
			_currentNucleotide = "A";
			ShowNN();
		}

		private void ShowCN(object sender, RoutedEventArgs e)
		{
			_currentNucleotide = "C";
			ShowNN();
		}

		private void ShowTN(object sender, RoutedEventArgs e)
		{
			_currentNucleotide = "T";
			ShowNN();
		}

		private void ShowGN(object sender, RoutedEventArgs e)
		{
			_currentNucleotide = "G";
			ShowNN();
		}
	}
}