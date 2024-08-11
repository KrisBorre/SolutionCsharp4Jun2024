namespace LibraryIndividual4Jun2024
{
    public class PopulationGenerator
    {
        public PopulationGenerator()
        { }

        public Population Initialize(Problem maleProblem, Problem femaleProblem, int populationSize)
        {
            Population population = new Population(maleProblem, femaleProblem);

            IndividualGenerator individualGenerator = new IndividualGenerator(population.NumberOfProblemVariables, population.LowerBounds, population.UpperBounds);

            for (int i = 0; i < populationSize; i++)
            {
                Individual individual = individualGenerator.NewIndividual();

                population.Individuals.Add(individual);
            }

            return population;
        }

        /// <summary>
        /// Precondition: originalPopulation has its fitness evaluated.
        /// </summary>
        /// <param name="originalPopulation"></param>
        /// <returns></returns>
        public Population Next(Population originalPopulation, double geneticDistanceThreshold = 1.75)
        {
            int originalPopulationSize = originalPopulation.Count;

            Population newPopulation = new Population(originalPopulation.MaleProblem, originalPopulation.FemaleProblem);

            IndividualGenerator individualGenerator = new IndividualGenerator(newPopulation.NumberOfProblemVariables, newPopulation.LowerBounds, newPopulation.UpperBounds);

            GeneticDistanceCalculator geneticDistanceCalculator = new GeneticDistanceCalculator(newPopulation.NumberOfProblemVariables);

            Random random = new Random();

            double highestFitness = originalPopulation.GetHighestFitness();
            double lowestFitness = originalPopulation.GetLowestFitness();

            for (int member = 0; member < originalPopulationSize; member++)
            {
                Individual elder = originalPopulation[member];

                double relativeFitness = (elder.Fitness - lowestFitness) / (highestFitness - lowestFitness);

                if (relativeFitness > random.NextDouble())
                {
                    newPopulation.Individuals.Add(elder);
                    elder.Age++;
                }
            }

            double power = Math.Log10(originalPopulationSize);

            while (newPopulation.Count < originalPopulationSize)
            {
                for (int member1 = 0; member1 < originalPopulationSize; member1++)
                {
                    Individual parent1 = originalPopulation[member1];
                    double relativeFitness1 = (parent1.Fitness - lowestFitness) / (highestFitness - lowestFitness);

                    if (Math.Pow(relativeFitness1, power) > random.NextDouble())
                    {
                        for (int member2 = 0; member2 < originalPopulationSize; member2++)
                        {
                            if (member1 != member2)
                            {
                                Individual parent2 = originalPopulation[member2];

                                if ((parent1.IsMale() && parent2.IsFemale()) || (parent1.IsFemale() && parent2.IsMale()))
                                {
                                    double relativeFitness2 = (parent2.Fitness - lowestFitness) / (highestFitness - lowestFitness);

                                    if (Math.Pow(relativeFitness2, power) > random.NextDouble())
                                    {
                                        if (geneticDistanceCalculator.CalculateDistance(parent1, parent2) < geneticDistanceThreshold * random.NextDouble())
                                        {
                                            Individual child = individualGenerator.NewIndividual(parent1, parent2);

                                            newPopulation.Individuals.Add(child);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }

            // Reduce the population size to the initial population size.
            newPopulation.Evaluate();

            newPopulation.Individuals.Sort();

            Population newestPopulation = new Population(originalPopulation.MaleProblem, originalPopulation.FemaleProblem);

            for (int member = 0; member < Math.Min(originalPopulationSize, newPopulation.Individuals.Count); member++)
            {
                Individual individual = newPopulation.Individuals[member];

                newestPopulation.Individuals.Add(individual);
            }

            return newestPopulation;
        }

    }
}
