using LibraryIndividual4Jun2024;
using OxyPlot;
using OxyPlot.Series;
using OxyPlot.WindowsForms;

namespace WinFormsSpeciesFormationProblem8_4Jun2024
{
    internal class ControlManager
    {
        private List<Control> controls;

        public List<Control> Controls
        {
            get { return controls; }
        }

        private int numberOfPlots;
        private List<PlotView> plotViews;

        public List<PlotView> PlotViews
        {
            get { return plotViews; }
        }

        private Label label1;
        private Problem problem;

        public ControlManager(Problem problem)
        {
            this.problem = problem;
            this.numberOfPlots = problem.NumberOfProblemVariables - 1;

            this.controls = new List<Control>();

            label1 = new Label();
            label1.AutoSize = true;
            label1.Location = new Point(10, 7);
            label1.Name = "label1";
            label1.Size = new Size(38, 15);
            label1.TabIndex = 0;
            label1.Text = "label1";
            Controls.Add(label1);

            int width = 1456;
            int height = 557;

            this.plotViews = new List<PlotView>();

            for (int i = 0; i < this.numberOfPlots; i++)
            {
                PlotView plotView = new PlotView();
                Controls.Add(plotView);
                plotView.Size = new Size(width, (height - 40) / this.numberOfPlots);
                plotView.Location = new Point(0, 40 + i * ((height - 40) / this.numberOfPlots));
                this.plotViews.Add(plotView);
            }

            for (int i = 0; i < this.numberOfPlots; i++)
            {
                PlotModel plotModel = new PlotModel();

                LineSeries series = this.GetFitnessSeries(j: i + 1);
                plotModel.Series.Add(series);

                HistogramSeries chs = this.Simulate(j: i + 1);
                plotModel.Series.Add(chs);

                this.plotViews[i].Model = plotModel;
            }
        }

        private LineSeries GetFitnessSeries(int j = 1)
        {
            label1.Text = problem.ToString();

            LineSeries series = new LineSeries();

            const int numberOfPlotPoints = 3000;

            for (int i = 1; i < numberOfPlotPoints; i++)
            {
                double[] y = new double[problem.NumberOfProblemVariables];

                for (int k = 0; k < problem.NumberOfProblemVariables; k++)
                {
                    y[k] = problem.LowerBounds[k];
                }

                y[j] = ((i * (problem.UpperBounds[j] - problem.LowerBounds[j])) / numberOfPlotPoints) + problem.LowerBounds[j];

                series.Points.Add(new DataPoint(y[j], problem.GetFitness(y)));
            }

            return series;
        }

        private HistogramSeries Simulate(int j = 1)
        {
            int populationSize = 1000;
            PopulationGenerator populationGenerator = new PopulationGenerator();
            Population population = populationGenerator.Initialize(this.problem, this.problem, populationSize);

            int maximum_number_generations = 2; //24; // 8;

            for (int generation = 0; generation < maximum_number_generations; generation++)
            {
                population.Evaluate();
                population = populationGenerator.Next(population);
            }

            double lowerBound = problem.LowerBounds[j];
            double upperBound = problem.UpperBounds[j];

            HistogramSeries chs = new HistogramSeries();
            var binningOptions = new BinningOptions(BinningOutlierMode.CountOutliers, BinningIntervalType.InclusiveLowerBound, BinningExtremeValueMode.ExcludeExtremeValues);
            var binBreaks = HistogramHelpers.CreateUniformBins(start: lowerBound, end: upperBound, binCount: 300);
            chs.Items.AddRange(HistogramHelpers.Collect(population.Sample(j), binBreaks, binningOptions));
            chs.StrokeThickness = 1;

            return chs;
        }
    }
}
