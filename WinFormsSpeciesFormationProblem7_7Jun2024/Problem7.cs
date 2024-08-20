using LibraryIndividual7Jun2024;

namespace WinFormsSpeciesFormationProblem7_7Jun2024
{
    public class Problem7 : Problem
    {
        public Problem7() : base(4)
        {
            this.LowerBounds.Add(-1);
            this.UpperBounds.Add(1);

            this.LowerBounds.Add(-3);
            this.UpperBounds.Add(12.1);

            this.LowerBounds.Add(-4.1);
            this.UpperBounds.Add(5.8);

            this.LowerBounds.Add(-2);
            this.UpperBounds.Add(2);
        }

        public override double GetFitness(double[] genes, double[] coordinates)
        {
            double u = genes[0];

            double x = genes[1];
            double y = genes[2];
            double z = genes[3];

            double genetic_fitness = 21.5 + x * Math.Sin(4 * Math.PI * x) + y * Math.Sin(20 * Math.PI * y) + z * Math.Sin(0.5 * Math.PI * z);

            double geographic_distance = Math.Sqrt(coordinates[0] * coordinates[0] + coordinates[1] * coordinates[1]);
            double geographic_fitness = -100 * Math.Abs((geographic_distance - 0.5) * (geographic_distance - 0.5));

            return genetic_fitness + geographic_fitness;
        }

        public override string ToString()
        {
            return "Maximization Problem of 21.5 + x * sin(4 Pi x) + y * sin(20 Pi y) + z * sin(0.5 Pi z) using a ring species";
        }
    }
}
