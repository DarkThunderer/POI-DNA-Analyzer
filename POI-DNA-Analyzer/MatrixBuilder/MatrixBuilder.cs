namespace POI_DNA_Analyzer
{
	internal class MatrixBuilder
	{
		private ChunkAnalyzer _chunkAnalyzer;

		public MatrixBuilder(ChunkAnalyzer chunkAnalyzer)
		{
			_chunkAnalyzer = chunkAnalyzer;
		}

		public Dictionary<string, float> DinucleotidesProbabilities { get; private set; } = new Dictionary<string, float>();

		public void Build()
		{
			ClearDinucleotidesProbabilities();

			foreach (string key in _chunkAnalyzer.DinucleotidesCount.Keys.ToList())
			{
				SetProbability(key);
			}
		}

		private void SetProbability(string dinucleotide)
		{
			DinucleotidesProbabilities.Add(dinucleotide, CalculateProbability(dinucleotide));
		}

		private float CalculateProbability(string dinucleotide)
		{
			char nucleotide = dinucleotide[0];

			float result = _chunkAnalyzer.DinucleotidesCount[dinucleotide] / _chunkAnalyzer.NucleotidesCount[nucleotide];

			return result;
		}

		private void ClearDinucleotidesProbabilities()
		{
			DinucleotidesProbabilities.Clear();
		}
	}
}
