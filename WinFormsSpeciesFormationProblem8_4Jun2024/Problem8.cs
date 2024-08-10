using LibraryIndividual4Jun2024;

namespace WinFormsSpeciesFormationProblem8_4Jun2024
{
    public class Problem8 : Problem
    {
        public Problem8() : base(2)
        {
            this.LowerBounds.Add(-1);
            this.UpperBounds.Add(1);

            this.LowerBounds.Add(-1);
            this.UpperBounds.Add(1);
        }

        public override double GetFitness(params double[] genes)
        {
            double x = genes[0];
            double y = genes[1];

            // males are defined as individuals with a negative first gene.
            // females are defined as individuals with a positive first gene.

            return -Math.Sign(x) * y;
        }

        public override string ToString()
        {
            return "Maximization Problem of -sign(x)*y";
        }
    }
}
