﻿namespace LibraryIndividual4Jun2024
{
    public class Individual : IComparable<Individual>
    {
        // https://en.wikipedia.org/wiki/Ploidy
        // monoploid
        private List<double> numbers;

        public double Fitness { get; private set; }

        public List<double> Numbers { get { return numbers; } }

        public Individual()
        {
            this.numbers = new List<double>();
            this.Age = 0;
        }

        public bool IsMale()
        {
            bool isMale = false;
            if (this.numbers.Count > 0 && this.numbers[0] < 0)
            {
                // is X chromosome
                isMale = true;
            }
            return isMale;
        }

        public bool IsFemale()
        {
            bool isFemale = false;
            if (this.numbers.Count > 0 && this.numbers[0] > 0)
            {
                // is Y choromosome
                isFemale = true;
            }
            return isFemale;
        }

        public int Age;

        public void Evaluate(Problem problem)
        {
            List<double> chromosomes = numbers;

            if (this.IsMale() || this.IsFemale())
            {
                this.Fitness = problem.GetFitness(chromosomes.ToArray());
            }
            else
            {
                this.Fitness = -1.0;
            }
        }

        public int CompareTo(Individual other)
        {
            if (other == null) return 1;
            else return -this.Fitness.CompareTo(other.Fitness);
        }
    }
}
