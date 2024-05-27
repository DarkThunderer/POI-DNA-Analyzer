namespace POI_DNA_Analyzer
{
	internal class EuclideanDistanceMatrixComparator : IMatrixComparator
	{
		private var _firstMatrixProbabilities;
		private var _secondMatrixProbabilities;

		public bool IsSimilar(Dictionary<string, float> firstMatrix, Dictionary<string, float> secondMatrix, double similarityCoefficient)
		{
			_firstMatrixProbabilities = GetDataFromMatrix(firstMatrix);
			_secondMatrixProbabilities = GetDataFromMatrix(secondMatrix);

			double similarity = CalculateCosineSimilarity(_firstMatrixProbabilities, _secondMatrixProbabilities);

			if (similarity >= similarityCoefficient)
				return true;
			else
				return false;
		}

		private Dictionary<string, Dictionary<string, float>> GetDataFromMatrix(Dictionary<string, float> inputMatrix)
		{
			var result = new Dictionary<string, Dictionary<string, float>>() 
			{
				['AN'] = new Dictionary<string, float>(),
				['TN'] = new Dictionary<string, float>(),
				['GN'] = new Dictionary<string, float>(),
				['CN'] = new Dictionary<string, float>()
			};
			
			foreach (var item in inputMatrix)
			{
				switch (item.Key)
                {
					case 'AA' || 'AT' || 'AG' || 'AC':
						result['AN'].Add(item.Key, Math.Round(item.Value * 100, 2));
					case 'TA' || 'TT' || 'TG' || 'TC':
						result['TN'].Add(item.Key, Math.Round(item.Value * 100, 2));
					case 'GA' || 'GT' || 'GG' || 'GC':
						result['GN'].Add(item.Key, Math.Round(item.Value * 100, 2));
					case 'CA' || 'CT' || 'CG' || 'CC':
						result['CN'].Add(item.Key, Math.Round(item.Value * 100, 2));
				}
			}

			return result;
		}

		private double CalculateCosineSimilarity(Dictionary<string, Dictionary<string, float>> firstMatrix, Dictionary<string, Dictionary<string, float>> secondMatrix)
		{
			float similarity = 0;
			foreach (var vector in firstMatrix) 
			{
				float innerProduct = 0;
				float firstNorm = 0;
				float secondNorm = 0;
				foreach (var axis in vector.Value.Keys)
                {
					innerProduct += vector.Value[axis] * secondMatrix[vector.Key][axis];
					firstNorm += Math.Pow(vector.Value[axis], 2);
					secondNorm += Math.Pow(secondMatrix[vector.Key][axis], 2);
				}
				firstNorm = Math.Sqrt(firstNorm);
				secondNorm = Math.Sqrt(secondNorm);
				similarity += innerProduct / (firstNorm * secondNorm);
			}
			return similarity / 4;
		}
	}
}
