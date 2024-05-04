using OxyPlot.Series;
using OxyPlot;
using System.Drawing;
using OxyPlot.Wpf;
using OxyPlot.Axes;

namespace POI_DNA_Analyzer
{
	class OxyPlotProbabilityGraph
	{
		private PlotView _plotView;
		private PlotModel _model;

		public OxyPlotProbabilityGraph(PlotView plotView)
		{
			_plotView = plotView;
			_model = new PlotModel { };

			_model.Axes.Add(new LinearAxis()
			{
				Position = AxisPosition.Bottom,
				Minimum = 1,
				Maximum = 5,
				FractionUnit = 1,
				MinorStep = 1,
				MajorStep = 1,
				IsZoomEnabled = false,
			});
			_model.Axes.Add(new LinearAxis()
			{
				Position = AxisPosition.Left,
				Minimum = 0,
				Maximum = 100,
				FractionUnit = 10,
				MinorStep = 10,
				MajorStep = 20,
				LabelFormatter = value => $"{value}%",
				IsPanEnabled = false,
				IsZoomEnabled = false,
			});
		}

		public void Clear()
		{
			_model.Series.Clear();
		}

		public void ProvideData(List<double> probabilities, Color color)
		{
			LineSeries series = new LineSeries
			{
				MarkerType = MarkerType.Circle,
				MarkerSize = 4,
				MarkerStroke = OxyColors.White,
				Color = OxyColor.FromRgb(color.R, color.G, color.B),
				StrokeThickness = 2,
			};

			for (int i = 0; i < probabilities.Count; i++)
			{
				series.Points.Add(new DataPoint(i + 1, probabilities[i]));
			}

			_model.Series.Add(series);
		}

		public void Show()
		{
			_plotView.Model = _model;
		}
	}
}
