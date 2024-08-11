using LibraryIndividual4Jun2024;

namespace WinFormsSpeciesFormationProblem6_6Jun2024
{
    // The same function as Problem4
    // The Bounds are different from Problem 4
    public class Problem6 : Problem
    {
        public Problem6() : base(3)
        {
            this.LowerBounds.Add(-1);
            this.UpperBounds.Add(1);

            this.LowerBounds.Add(-3);
            this.UpperBounds.Add(12.1);

            this.LowerBounds.Add(-4.1);
            this.UpperBounds.Add(5.8);
        }

        public override double GetFitness(params double[] genes)
        {
            double u = genes[0];

            double x = genes[1];
            double y = genes[2];

            return 21.5 + x * Math.Sin(4 * Math.PI * x) + y * Math.Sin(20 * Math.PI * y);
        }

        public override string ToString()
        {
            return "Maximization Problem of 21.5 + x * sin(4 Pi x) + y * sin(20 Pi y)";
        }
    }
}
