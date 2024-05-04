using System.IO;

namespace POI_DNA_Analyzer
{
	internal class DinucleotidesAnalyzer
    {
		public Action ChunkAnalyzed;

		private ChunkAnalyzer _chunkAnalyzer;
		private ChunkMatrixBuilder _chunkMatrixBuilder;

		public DinucleotidesAnalyzer() 
		{
			_chunkAnalyzer = new ChunkAnalyzer();
			_chunkMatrixBuilder = new ChunkMatrixBuilder(_chunkAnalyzer);

			InitializeDictionary();
		}

		public Dictionary<string, List<double>> DinucleotidesProbabilities { get; private set; } = new Dictionary<string, List<double>>();

		public void Analyze(StreamReader fileStream, int chunkSize)
		{
			ClearDictionary();

			char[] buffer = new char[chunkSize];
			int charsRead;

			while ((charsRead = fileStream.ReadBlock(buffer, 0, buffer.Length)) > 0)
			{
				string chunk = new string(buffer, 0, charsRead);

				_chunkAnalyzer.AnalyzeChunk(chunk);
				_chunkMatrixBuilder.Build();
				GetDataFromMatrix(_chunkMatrixBuilder.DinucleotidesProbabilities);

				fileStream.BaseStream.Seek(chunkSize, SeekOrigin.Current);
			}
		}

		private void GetDataFromMatrix(Dictionary<string, float> matrix)
		{
			foreach (string key in DinucleotidesProbabilities.Keys.ToList())
			{
				if (matrix.ContainsKey(key) == false)
				{
					DinucleotidesProbabilities[key].Add(0);
					return;
				}

				DinucleotidesProbabilities[key].Add(matrix[key] * 100);
			}
		}
		
		private void InitializeDictionary()
		{
			foreach (string key in new DinucleotidesList().Get)
				DinucleotidesProbabilities.Add(key, new List<double>());
		}

		private void ClearDictionary()
		{
			foreach (string key in DinucleotidesProbabilities.Keys.ToList())
				DinucleotidesProbabilities[key] = new List<double>();
		}
    }
}
