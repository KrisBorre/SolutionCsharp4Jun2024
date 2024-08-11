using LibraryIndividual4Jun2024;

namespace WinFormsSpeciesFormationProblem4_6Jun2024
{
    public partial class Problem4Form : Form
    {
        private ControlManager controlManager;

        public Problem4Form()
        {
            InitializeComponent();

            int width = 1456;
            int height = 851;
            this.ClientSize = new Size(width, height);

            Problem problem = new Problem4();

            this.Text = problem.ToString();

            this.controlManager = new ControlManager(problem: problem, width: width, height: height);

            foreach (Control control in this.controlManager.Controls)
            {
                this.Controls.Add(control);
            }

            SizeChanged += Form1_SizeChanged;
        }

        private void Form1_SizeChanged(object sender, EventArgs e)
        {
            int width = this.ClientSize.Width;
            int totalPlotViewheight = this.ClientSize.Height - 40;

            if (this.controlManager.PlotViews != null)
            {
                for (int i = 0; i < this.controlManager.PlotViews.Count; i++)
                {
                    if (this.controlManager.PlotViews[i] != null)
                    {
                        this.controlManager.PlotViews[i].Size = new Size(width, (totalPlotViewheight - 40) / this.controlManager.PlotViews.Count);
                        this.controlManager.PlotViews[i].Location = new Point(0, 40 + i * ((totalPlotViewheight - 40) / this.controlManager.PlotViews.Count));
                    }
                }
            }

            this.controlManager.Label2.Location = new Point(12, totalPlotViewheight);
            this.controlManager.TextBox1.Location = new Point(149, totalPlotViewheight);

            this.controlManager.Label3.Location = new Point(251, totalPlotViewheight);
            this.controlManager.TextBox2.Location = new Point(410, totalPlotViewheight);
        }
    }
}
