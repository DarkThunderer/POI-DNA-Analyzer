using OxyPlot.Series;
using OxyPlot;
using System.Drawing;
using OxyPlot.Wpf;

namespace POI_DNA_Analyzer
{
	class OxyPlotProbabilityGraph : IProbabilityGraph
	{
		private PlotView _plotView;
		private PlotModel _model;

		public OxyPlotProbabilityGraph(PlotView plotView)
		{
			_plotView = plotView;
			_model = new PlotModel { };
		}

		public void Clear()
		{
			_model.Series.Clear();
		}

		public void Show(int[] chunks, double[] probabilities, Color color)
		{
			LineSeries series = new LineSeries
			{
				MarkerType = MarkerType.Circle,
				MarkerSize = 4,
				MarkerStroke = OxyColors.White,
				MarkerFill = OxyColors.Blue,
				Color = OxyColor.FromRgb(color.R, color.G, color.B),
				StrokeThickness = 2,
			};

			for (int i = 0; i < chunks.Length; i++)
			{
				series.Points.Add(new DataPoint(chunks[i], probabilities[i]));
			}

			_model.Series.Add(series);
		}

		public void Show()
		{
			_plotView.Model = _model;
		}
	}
}
