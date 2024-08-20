namespace LibraryIndividual7Jun2024
{
    public abstract class Problem
    {
        public int NumberOfProblemVariables { get; private set; }

        public List<double> UpperBounds { get; set; }
        public List<double> LowerBounds { get; set; }

        public Problem(int numberOfProblemVariables)
        {
            NumberOfProblemVariables = numberOfProblemVariables;
            UpperBounds = new List<double>();
            LowerBounds = new List<double>();
        }

        public abstract double GetFitness(double[] genes, double[] coordinates);
    }
}
