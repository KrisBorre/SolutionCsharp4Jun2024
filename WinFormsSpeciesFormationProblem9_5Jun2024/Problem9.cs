using LibraryIndividual4Jun2024;

namespace WinFormsSpeciesFormationProblem9_5Jun2024
{
    public class Problem9 : Problem
    {
        public Problem9() : base(6)
        {
            this.LowerBounds.Add(-1);
            this.UpperBounds.Add(1);

            this.LowerBounds.Add(-1);
            this.UpperBounds.Add(1);

            this.LowerBounds.Add(0);
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
            double x = genes[0];

            // males are defined as individuals with a negative first gene.
            // females are defined as individuals with a positive first gene.

            double y1 = genes[1];
            double y2 = genes[2];
            double y3 = genes[3];
            double y4 = genes[4];
            double y5 = genes[5];

            return -Math.Sign(x) * y1 * y2 * y3 * y4 * y5;
        }

        public override string ToString()
        {
            return "Optimization problem with 6 floating point chromosomes";
        }
    }
}
