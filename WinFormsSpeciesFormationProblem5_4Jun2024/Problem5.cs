using LibraryIndividual4Jun2024;

namespace WinFormsSpeciesFormationProblem5_4Jun2024
{
    // Genetic Algorithms (1999) page 75
    // The same function as Problem2
    // The Bounds are different from Problem 2
    public class Problem5 : Problem
    {
        public Problem5() : base(2)
        {
            this.LowerBounds.Add(-1);
            this.UpperBounds.Add(1);

            this.LowerBounds.Add(-2);
            this.UpperBounds.Add(1);
        }

        public override double GetFitness(params double[] genes)
        {
            double u = genes[0];

            double x = genes[1];

            return x * Math.Sin(10 * Math.PI * x) + 1.0;
        }

        public override string ToString()
        {
            return "Maximization Problem of x * sin(10 Pi x) + 1.0";
        }
    }
}
