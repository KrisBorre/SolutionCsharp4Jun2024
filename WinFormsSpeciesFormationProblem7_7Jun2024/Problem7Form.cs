using LibraryIndividual7Jun2024;

namespace WinFormsSpeciesFormationProblem7_7Jun2024
{
    public partial class Problem7Form : Form
    {
        private ControlManager controlManager;

        public Problem7Form()
        {
            InitializeComponent();

            int width = 1456;
            int height = 851;
            this.ClientSize = new Size(width, height);

            Problem problem = new Problem7();

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
        }
    }
}
