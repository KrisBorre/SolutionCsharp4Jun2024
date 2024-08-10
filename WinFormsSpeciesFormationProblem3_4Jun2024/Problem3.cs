using LibraryIndividual4Jun2024;

namespace WinFormsSpeciesFormationProblem3_4Jun2024
{
    public class Problem3 : Problem
    {
        public Problem3() : base(2)
        {
            this.LowerBounds.Add(-1);
            this.UpperBounds.Add(1);

            this.LowerBounds.Add(-1);
            this.UpperBounds.Add(2);
        }

        public override double GetFitness(params double[] genes)
        {
            double u = genes[0];

            double x = genes[1];

            return -(x * Math.Sin(10 * Math.PI * x) + 1.0);
        }

        public override string ToString()
        {
            return "Minimization Problem of x * sin(10 Pi x) + 1.0 ; is the Maximization of -(x * sin(10 Pi x) + 1.0)";
        }
    }
}
