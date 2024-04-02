namespace POI_DNA_Analyzer
{
	internal class SequencesFinder
	{
		public int CountOccurrences(string source, string sequenceToFind)
		{
			int count = 0;
			int index = 0;

			while ((index = source.IndexOf(sequenceToFind, index, StringComparison.OrdinalIgnoreCase)) != -1)
			{
				index += sequenceToFind.Length;
				count++;
			}

			return count;
		}
	}
}
