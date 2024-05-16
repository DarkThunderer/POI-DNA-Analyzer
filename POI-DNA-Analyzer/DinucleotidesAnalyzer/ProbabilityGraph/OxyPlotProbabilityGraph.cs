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

			CreateLegend();
			AddXAxis();
			AddYAxis();
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
				DataPoint dataPoint = new DataPoint(i, probabilities[i]);
				series.Points.Add(dataPoint);
			}

			UpdateXAxis(indexes);

			_model.Series.Add(series);
		}

		public void Show()
		{
			_plotView.Model = _model;

			_model.InvalidatePlot(true);
		}

		public void Clear()
		{
			_model.Series.Clear();
		}

		private void UpdateXAxis(List<int> indexes)
		{
			if (_model == null)
				return;

			LinearAxis? xAxis = new LinearAxis();

			if (TryGetXAxis(out xAxis))
			{
				xAxis.MajorStep = 1;
				xAxis.MinorStep = 1;
				xAxis.LabelFormatter = value => FormatValue(indexes, value);

				_plotView.InvalidatePlot();
			}
		}

		private string FormatValue(List<int> indexes, double value)
		{
			if (value >= indexes.Count || value < 0)
				return "";

			string result = indexes[Convert.ToInt32(value)].ToString();

			return result;
		}

		private void CreateLegend()
		{
			Legend legend = new Legend()
			{
				LegendPlacement = LegendPlacement.Outside,
				LegendPosition = LegendPosition.LeftMiddle,
				LegendBackground = OxyColor.FromAColor(200, OxyColors.White),
				LegendBorder = OxyColors.Black,
				LegendFontSize = 12,
			};

			_model.Legends.Add(legend);
		}

		private void AddXAxis()
		{
			_model.Axes.Add(new LinearAxis()
			{
				Position = AxisPosition.Bottom,
				Minimum = 1,
				Maximum = 5,
				FractionUnit = 1,
				MinorStep = 1,
				MajorStep = 1,
			});
		}

		private void AddYAxis()
		{
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

		private bool TryGetXAxis(out LinearAxis? linearAxis)
		{
			linearAxis = _model.Axes.OfType<LinearAxis>()
				.FirstOrDefault(axis => axis.Position == AxisPosition.Bottom);

			return linearAxis != null;
		}
	}
}
