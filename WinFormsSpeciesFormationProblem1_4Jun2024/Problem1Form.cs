using LibraryIndividual4Jun2024;

namespace WinFormsSpeciesFormationProblem1_4Jun2024
{
    public partial class Problem1Form : Form
    {
        private ControlManager controlManager;

        public Problem1Form()
        {
            InitializeComponent();

            int width = 1456;
            int height = 557;
            this.ClientSize = new Size(width, height);

            Problem problem = new Problem1();

            this.Text = problem.ToString();

            this.controlManager = new ControlManager(problem);

            foreach (Control control in controlManager.Controls)
            {
                this.Controls.Add(control);
            }

            SizeChanged += Form1_SizeChanged;
        }

        private void Form1_SizeChanged(object sender, EventArgs e)
        {
            int width = this.ClientSize.Width;
            int height = this.ClientSize.Height;

            if (this.controlManager.PlotViews != null)
            {
                for (int i = 0; i < this.controlManager.PlotViews.Count; i++)
                {
                    if (this.controlManager.PlotViews[i] != null)
                    {
                        this.controlManager.PlotViews[i].Size = new Size(width, (height - 40) / this.controlManager.PlotViews.Count);
                        this.controlManager.PlotViews[i].Location = new Point(0, 40 + i * ((height - 40) / this.controlManager.PlotViews.Count));
                    }
                }
            }
        }
    }
}
