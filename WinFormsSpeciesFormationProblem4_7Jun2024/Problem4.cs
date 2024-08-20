using LibraryIndividual7Jun2024;

namespace WinFormsSpeciesFormationProblem4_7Jun2024
{
    public class Problem4 : Problem
    {
        public Problem4() : base(3)
        {
            this.LowerBounds.Add(-1);
            this.UpperBounds.Add(1);

            this.LowerBounds.Add(-3);
            this.UpperBounds.Add(12.1);

            this.LowerBounds.Add(4.1);
            this.UpperBounds.Add(5.8);
        }

        public override double GetFitness(double[] genes, double[] coordinates)
        {
            double u = genes[0];

            double x = genes[1];
            double y = genes[2];

            double genetic_fitness = 21.5 + x * Math.Sin(4 * Math.PI * x) + y * Math.Sin(20 * Math.PI * y);

            double geographic_distance = Math.Sqrt(coordinates[0] * coordinates[0] + coordinates[1] * coordinates[1]);
            double geographic_fitness = -100 * Math.Abs((geographic_distance - 0.5) * (geographic_distance - 0.5));

            return genetic_fitness + geographic_fitness;
        }

        public override string ToString()
        {
            return "Maximization Problem of 21.5 + x * sin(4 Pi x) + y * sin(20 Pi y) using a ring species";
        }
    }
}
