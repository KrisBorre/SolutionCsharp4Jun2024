namespace LibraryIndividual7Jun2024
{
    public class GeographicDistanceCalculator
    {
        public GeographicDistanceCalculator()
        {
        }

        /// <summary>
        /// Euclidean metric
        /// </summary>
        /// <param name="individual1"></param>
        /// <param name="individual2"></param>
        /// <returns></returns>
        public double CalculateDistance(Individual individual1, Individual individual2)
        {
            double distance = 0;

            for (int i = 1; i < 2; i++)
            {
                distance += Math.Pow(individual1.GeographicCoordinates[i] - individual2.GeographicCoordinates[i], 2);
            }

            return Math.Sqrt(distance);
        }
    }
}
