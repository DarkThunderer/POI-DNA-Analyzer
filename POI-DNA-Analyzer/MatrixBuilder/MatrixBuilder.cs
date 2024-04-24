namespace POI_DNA_Analyzer
{
	internal class MatrixBuilder
	{
		private Dictionary<string, int> _pairs = new Dictionary<string, int>();

		private int _anCount = 0;
		private int _gnCount = 0;
		private int _cnCount = 0;
		private int _tnCount = 0;

		public Dictionary<string, float> PairsProbabilities { get; private set; } = new Dictionary<string, float>();

		public MatrixBuilder()
		{
			PairsProbabilities.Add("AA", 0);
			PairsProbabilities.Add("AG", 0);
			PairsProbabilities.Add("AC", 0);
			PairsProbabilities.Add("AT", 0);

			PairsProbabilities.Add("GA", 0);
			PairsProbabilities.Add("GG", 0);
			PairsProbabilities.Add("GC", 0);
			PairsProbabilities.Add("GT", 0);

			PairsProbabilities.Add("CA", 0);
			PairsProbabilities.Add("CG", 0);
			PairsProbabilities.Add("CC", 0);
			PairsProbabilities.Add("CT", 0);

			PairsProbabilities.Add("TA", 0);
			PairsProbabilities.Add("TG", 0);
			PairsProbabilities.Add("TC", 0);
			PairsProbabilities.Add("TT", 0);
		}

		public void Bulid(Dictionary<string, int> pairs, int chunkSize)
		{
			_pairs = pairs;
			ClearPairs();

			foreach (string key in PairsProbabilities.Keys.ToList())
			{
				HandlePair(key, chunkSize);
			}
		}

		private void HandlePair(string pair, int chunkSize)
		{
			if (PairsProbabilities.ContainsKey(pair))
				PairsProbabilities[pair] = _pairs[pair] / chunkSize;
		}

		private void ClearPairs()
		{
			foreach (string key in PairsProbabilities.Keys.ToList())
				PairsProbabilities[key] = 0;
		}
	}
}
