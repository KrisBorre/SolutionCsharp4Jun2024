using LibraryIndividual7Jun2024;
using OxyPlot;
using OxyPlot.Series;
using OxyPlot.WindowsForms;
using System.Globalization;

namespace WinFormsSpeciesFormationProblem9_7Jun2024
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

        private Label label2;
        private TextBox textBox1;

        private Label label3;
        private TextBox textBox2;

        private Label label4;
        private TextBox textBox3;

        private Button button1;

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

            label2 = new Label();
            label2.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            label2.AutoSize = true;
            label2.Location = new Point(12, totalPlotViewheight);
            label2.Name = "label2";
            label2.Size = new Size(131, 15);
            label2.TabIndex = 2;
            label2.Text = "number of generations:";
            Controls.Add(label2);

            textBox1 = new TextBox();
            textBox1.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            textBox1.Location = new Point(149, totalPlotViewheight);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(39, 23);
            textBox1.TabIndex = 4;
            textBox1.Text = "2";
            Controls.Add(textBox1);

            label3 = new Label();
            label3.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            label3.AutoSize = true;
            label3.Location = new Point(251, totalPlotViewheight);
            label3.Name = "label3";
            label3.Size = new Size(153, 15);
            label3.TabIndex = 5;
            label3.Text = "genetic distance parameter:";
            Controls.Add(label3);

            textBox2 = new TextBox();
            textBox2.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            textBox2.Location = new Point(410, totalPlotViewheight);
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(39, 23);
            textBox2.TabIndex = 6;
            textBox2.Text = "1.75";
            Controls.Add(textBox2);

            label4 = new Label();
            label4.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            label4.AutoSize = true;
            label4.Location = new Point(300 + 251 - 30, totalPlotViewheight);
            label4.Name = "Label4";
            label4.Size = new Size(153, 15);
            label4.TabIndex = 5;
            label4.Text = "geographic distance parameter:";
            Controls.Add(label4);

            textBox3 = new TextBox();
            textBox3.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            textBox3.Location = new Point(300 + 410, totalPlotViewheight);
            textBox3.Name = "textBox3";
            textBox3.Size = new Size(39, 23);
            textBox3.TabIndex = 6;
            textBox3.Text = "1.75";
            Controls.Add(textBox3);

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

            const int numberOfPlotPoints = 1000;

            for (int i = 1; i < numberOfPlotPoints; i++)
            {
                double[] genes = new double[problem.NumberOfProblemVariables];

                for (int k = 0; k < problem.NumberOfProblemVariables; k++)
                {
                    genes[k] = problem.LowerBounds[k];
                }

                genes[j] = ((i * (problem.UpperBounds[j] - problem.LowerBounds[j])) / numberOfPlotPoints) + problem.LowerBounds[j];

                double[] coordinates = new double[2];
                coordinates[0] = 0.5;
                coordinates[1] = 0.5;

                series.Points.Add(new DataPoint(genes[j], problem.GetFitness(genes, coordinates)));
            }

            return series;
        }

        private HistogramSeries Simulate(int j = 1)
        {
            int populationSize = 1000;
            PopulationGenerator populationGenerator = new PopulationGenerator();
            Population population = populationGenerator.Initialize(this.problem, this.problem, populationSize);

            int number_of_generations_to_simulate = 2;

            string input1 = this.textBox1.Text;

            NumberFormatInfo provider = new NumberFormatInfo();
            provider.NumberDecimalSeparator = ".";

            if (int.TryParse(s: input1, style: NumberStyles.AllowDecimalPoint, provider: provider, result: out int number_of_generations_from_textBox))
            {
                number_of_generations_to_simulate = number_of_generations_from_textBox;
            }
            else
            {
                this.textBox1.Text = number_of_generations_to_simulate.ToString(provider);
            }

            string input2 = textBox2.Text;

            double geneticDistanceThreshold = 1.75;

            if (double.TryParse(s: input2, style: NumberStyles.AllowDecimalPoint, provider: provider, result: out double double_geneticDistanceThreshold))
            {
                geneticDistanceThreshold = double_geneticDistanceThreshold;
            }
            else
            {
                textBox2.Text = geneticDistanceThreshold.ToString(provider);
            }

            string input3 = textBox3.Text;

            double geographicDistanceThreshold = 1.75;

            if (double.TryParse(s: input3, style: NumberStyles.AllowDecimalPoint, provider: provider, result: out double double_geographicDistanceThreshold))
            {
                geographicDistanceThreshold = double_geographicDistanceThreshold;
            }
            else
            {
                textBox3.Text = geographicDistanceThreshold.ToString(provider);
            }

            for (int generation = 0; generation < number_of_generations_to_simulate; generation++)
            {
                population.Evaluate();
                population = populationGenerator.Next(population, geneticDistanceThreshold, geographicDistanceThreshold);
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
