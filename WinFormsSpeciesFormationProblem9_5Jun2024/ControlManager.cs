using LibraryIndividual4Jun2024;
using OxyPlot;
using OxyPlot.Series;
using OxyPlot.WindowsForms;
using System.Globalization;

namespace WinFormsSpeciesFormationProblem9_5Jun2024
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

        public Label Label2;
        public TextBox TextBox1;

        public Button button1;

        public ControlManager(Problem problem, int width, int height)
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

            this.plotViews = new List<PlotView>();
            int totalPlotViewheight = height - 40;

            for (int i = 0; i < this.numberOfPlots; i++)
            {
                PlotView plotView = new PlotView();
                Controls.Add(plotView);
                plotView.Size = new Size(width, (totalPlotViewheight - 40) / this.numberOfPlots);
                plotView.Location = new Point(0, 40 + i * ((totalPlotViewheight - 40) / this.numberOfPlots));
                this.plotViews.Add(plotView);
            }

            Label2 = new Label();
            Label2.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            Label2.AutoSize = true;
            Label2.Location = new Point(12, totalPlotViewheight);
            Label2.Name = "label2";
            Label2.Size = new Size(131, 15);
            Label2.TabIndex = 2;
            Label2.Text = "number of generations:";
            Controls.Add(Label2);

            TextBox1 = new TextBox();
            TextBox1.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            TextBox1.Location = new Point(149, totalPlotViewheight);
            TextBox1.Name = "textBox1";
            TextBox1.Size = new Size(39, 23);
            TextBox1.TabIndex = 4;
            TextBox1.Text = "2";
            Controls.Add(TextBox1);

            button1 = new Button();
            button1.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            button1.Location = new Point(width - 100, totalPlotViewheight);
            button1.Name = "button1";
            button1.Size = new Size(76, 23);
            button1.TabIndex = 3;
            button1.Text = "Simulate";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            Controls.Add(button1);

            this.Calculate();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Calculate();
        }

        private void Calculate()
        {
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

            int number_of_generations_to_simulate = 2;

            string input1 = this.TextBox1.Text;

            NumberFormatInfo provider = new NumberFormatInfo();
            provider.NumberDecimalSeparator = ".";

            if (int.TryParse(s: input1, style: NumberStyles.AllowDecimalPoint, provider: provider, result: out int number_of_generations_from_textBox))
            {
                number_of_generations_to_simulate = number_of_generations_from_textBox;
            }
            else
            {
                this.TextBox1.Text = number_of_generations_to_simulate.ToString();
            }

            for (int generation = 0; generation < number_of_generations_to_simulate; generation++)
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
