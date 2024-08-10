using LibraryIndividual4Jun2024;

namespace WinFormsSpeciesFormationProblem1_5Jun2024
{
    internal class Problem1 : Problem
    {
        public Problem1() : base(4)
        {
            this.LowerBounds.Add(-1);
            this.UpperBounds.Add(1);

            this.LowerBounds.Add(0);
            this.UpperBounds.Add(1);

            this.LowerBounds.Add(0);
            this.UpperBounds.Add(1);

            this.LowerBounds.Add(0);
            this.UpperBounds.Add(1);
        }

        public override double GetFitness(params double[] genes)
        {
            double u = genes[0];

            double x = genes[1];
            double y = genes[2];
            double z = genes[3];

            return x * (x - y) + z;
        }

        public override string ToString()
        {
            return "Maximization Problem of x*(x-y) + z";
        }
    }
}
