using OxyPlot.Series;
using OxyPlot;
using System.Drawing;
using OxyPlot.Wpf;
using OxyPlot.Axes;
using OxyPlot.Legends;

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

			Legend legend = new Legend() {
				LegendPlacement = LegendPlacement.Outside,
				LegendPosition = LegendPosition.LeftMiddle,
				LegendBackground = OxyColor.FromAColor(200, OxyColors.White),
				LegendBorder = OxyColors.Black,
				LegendFontSize = 12,
			};

			_model.Legends.Add(legend);

			_model.Axes.Add(new LinearAxis()
			{
				Position = AxisPosition.Bottom,
				Minimum = 1,
				Maximum = 5,
				FractionUnit = 1,
				MinorStep = 1,
				MajorStep = 1,
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

		public void ProvideData(List<int> indexes, List<double> probabilities, Color color, string name)
		{
			LineSeries series = new LineSeries
			{
				MarkerType = MarkerType.Circle,
				MarkerSize = 4,
				MarkerStroke = OxyColors.White,
				Color = OxyColor.FromRgb(color.R, color.G, color.B),
				StrokeThickness = 2,
				Title = name
			};

			for (int i = 0; i < probabilities.Count; i++)
			{
				series.Points.Add(new DataPoint(indexes[i] + 1, probabilities[i]));
			}

			_model.Series.Add(series);
		}

		public void Show()
		{
			_plotView.Model = _model;

			_model.InvalidatePlot(true);
		}
	}
}
