namespace LibraryIndividual7Jun2024
{
    public class IndividualGenerator
    {
        public int NumberOfProblemVariables;

        public List<double> LowerBounds;
        public List<double> UpperBounds;

        public IndividualGenerator(int numberOfProblemVariables, List<double> lbounds, List<double> ubounds)
        {
            this.NumberOfProblemVariables = numberOfProblemVariables;

            LowerBounds = lbounds;
            UpperBounds = ubounds;
        }

        /// <summary>
        /// The first value: Negative values correspond with males, positive values correspond with females.
        /// </summary>
        public Individual NewIndividual()
        {
            Individual individual = new Individual();

            Random random = new Random();
            for (int i = 0; i < this.NumberOfProblemVariables; i++)
            {
                individual.Numbers.Add(random.NextDouble() * (UpperBounds[i] - LowerBounds[i]) + LowerBounds[i]);
            }

            individual.GeographicCoordinates.Add(random.NextDouble() * (1 + 1) - 1);
            individual.GeographicCoordinates.Add(random.NextDouble() * (1 + 1) - 1);
            return individual;
        }

        public Individual NewIndividual(Individual parent1, Individual parent2)
        {
            double geneticDeviationFromParent = 0.1; //0.5; //0.01; //0.1; // 0.5; // 0.1;

            Random random = new Random();

            Individual child = new Individual();

            int i = 0;
            child.Numbers.Add(random.NextDouble() * (UpperBounds[i] - LowerBounds[i]) + LowerBounds[i]);

            for (i = 1; i < this.NumberOfProblemVariables; i++)
            {
                Individual parent = parent1;
                if (random.NextDouble() < 0.5)
                {
                    parent = parent2;
                }

                while (true)
                {
                    double number = this.SampleNormal(random, parent.Numbers[i], geneticDeviationFromParent);

                    if (number >= this.LowerBounds[i] && number <= this.UpperBounds[i])
                    {
                        child.Numbers.Add(number);
                        break;
                    }
                }
            }

            for (int j = 0; j < 2; j++) // x and y geographic coordinates
            {
                Individual parent = parent1;
                if (random.NextDouble() < 0.5)
                {
                    parent = parent2;
                }

                while (true)
                {
                    double coordinate = this.SampleNormal(rnd: random, mean: parent.GeographicCoordinates[j], std: geneticDeviationFromParent);

                    if (coordinate >= -1 && coordinate <= 1)
                    {
                        child.GeographicCoordinates.Add(coordinate);
                        break;
                    }
                }
            }

            return child;
        }

        private double SampleNormal(Random rnd, double mean, double std)
        {
            // http://en.wikipedia.org/wiki/Box%E2%80%93Muller_transform
            var u1 = 1.0 - rnd.NextDouble();
            var u2 = rnd.NextDouble();
            return Math.Sqrt(-2 * Math.Log(u1)) * Math.Cos(2 * Math.PI * u2) * std + mean;
        }
    }
}
