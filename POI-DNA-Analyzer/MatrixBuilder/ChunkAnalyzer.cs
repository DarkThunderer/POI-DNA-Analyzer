namespace POI_DNA_Analyzer
{
	internal class ChunkAnalyzer
	{
		public Dictionary<string, int> Pairs { get; private set; } = new Dictionary<string, int>();

		public ChunkAnalyzer()
		{
			Pairs.Add("AA", 0);
			Pairs.Add("AG", 0);
			Pairs.Add("AC", 0);
			Pairs.Add("AT", 0);

			Pairs.Add("GA", 0);
			Pairs.Add("GG", 0);
			Pairs.Add("GC", 0);
			Pairs.Add("GT", 0);

			Pairs.Add("CA", 0);
			Pairs.Add("CG", 0);
			Pairs.Add("CC", 0);
			Pairs.Add("CT", 0);

			Pairs.Add("TA", 0);
			Pairs.Add("TG", 0);
			Pairs.Add("TC", 0);
			Pairs.Add("TT", 0);
		}

		public void AnalyzeChunk(string chunk)
		{
			ClearPairs();

			for (int i = 0; i < chunk.Length - 1; i++)
			{
				string pair = chunk[i].ToString() + chunk[i + 1].ToString();

				HandlePair(pair);
			}
		}

		private void HandlePair(string pair)
		{
			if (Pairs.ContainsKey(pair))
				Pairs[pair]++;
		}

		private void ClearPairs()
		{
			foreach (string key in Pairs.Keys.ToList())
				Pairs[key] = 0;
		}
	}
}
