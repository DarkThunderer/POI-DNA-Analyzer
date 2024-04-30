using ScottPlot;
using ScottPlot.WPF;

namespace POI_DNA_Analyzer
{
	internal class ScottPlotProbabilityGraph : IProbabilityGraph
	{
		private WpfPlot _wpfPlot;

		public ScottPlotProbabilityGraph(WpfPlot wpfPlot)
		{
			_wpfPlot = wpfPlot;
		}

		public void Clear()
		{
			_wpfPlot.Plot.Clear();
		}

		public void Show(int[] chunks, double[] probability, System.Drawing.Color color)
		{
			_wpfPlot.Plot.Add.Scatter(chunks, probability, Color.FromColor(color));
			_wpfPlot.Refresh();
		}
	}
}
