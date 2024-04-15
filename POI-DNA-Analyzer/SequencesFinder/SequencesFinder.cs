namespace POI_DNA_Analyzer
{
	internal class SequencesFinder
	{
		public LinkedList<int> SequenceIndexes = new LinkedList<int>();
		public string Indexes = "";

		public int GetOccurrencesCount(string source, string sequenceToFind)
		{
			SequenceIndexes.Clear();

			int count = 0;
			int index = 0;

			while ((index = source.IndexOf(sequenceToFind, index, StringComparison.OrdinalIgnoreCase)) != -1)
			{
				SequenceIndexes.AddLast(index);

				index += sequenceToFind.Length;
				count++;
			}

			return count;
		}
	}
}
