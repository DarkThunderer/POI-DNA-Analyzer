﻿using System.IO;
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
	}
}