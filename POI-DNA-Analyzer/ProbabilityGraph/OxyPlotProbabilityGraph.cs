using OxyPlot.Series;
using OxyPlot;
using System.Drawing;
using OxyPlot.Wpf;
using OxyPlot.Axes;
using OxyPlot.Legends;
using OxyPlot.Annotations;

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
				DataPoint dataPoint = new DataPoint(i, probabilities[i]);
				series.Points.Add(dataPoint);

			}

			EditXAxis(indexes);

			_model.Series.Add(series);
		}

		public void Show()
		{
			_plotView.Model = _model;

			_model.InvalidatePlot(true);
		}

		private void EditXAxis(List<int> indexes)
		{
			if (_model == null)
				return;

			LinearAxis xAxis = _model.Axes.OfType<LinearAxis>()
				.FirstOrDefault(axis => axis.Position == AxisPosition.Bottom);

			if (xAxis != null)
			{
				xAxis.MajorStep = 1;
				xAxis.MinorStep = 1;
				xAxis.LabelFormatter = value =>
				{
					return indexes[Convert.ToInt32(value)].ToString();
				};

				_plotView.InvalidatePlot();
			}
		}
	}
}
