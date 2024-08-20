using LibraryIndividual7Jun2024;

namespace WinFormsSpeciesFormationProblem1_7Jun2024
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

        public override double GetFitness(double[] genes, double[] coordinates)
        {
            double u = genes[0];

            double x = genes[1];
            double y = genes[2];
            double z = genes[3];

            double genetic_fitness = x * (x - y) + z;

            double geographic_distance = Math.Sqrt(coordinates[0] * coordinates[0] + coordinates[1] * coordinates[1]);
            double geographic_fitness = -100 * Math.Abs((geographic_distance - 0.5) * (geographic_distance - 0.5));

            return genetic_fitness + geographic_fitness;
        }

        public override string ToString()
        {
            return "Maximization Problem of x*(x-y) + z using a ring species";
        }
    }
}
