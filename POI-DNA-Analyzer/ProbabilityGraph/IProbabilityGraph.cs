using System.Drawing;

namespace POI_DNA_Analyzer
{
	interface IProbabilityGraph
    {
		void Clear();

		void Show(int[] chunks, double[] probability, Color color);
	}
}
