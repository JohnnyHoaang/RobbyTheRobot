namespace GeneticAlgorithm
{
  public static class GeneticLib
  {
    /// <summary>
    /// Provides access to the Genetic Algorithm constructor without exposing the implementation
    /// </summary>
    /// <returns>Genetic Algorithm : IGeneticAlgorithm</returns>
    public static IGeneticAlgorithm CreateGeneticAlgorithm(int populationSize, int numberOfGenes, int lengthOfGene, double mutationRate, double eliteRate, int numberOfTrials, FitnessEventHandler fitnessCalculation, int? seed = null)
    {
      return new GeneticAlgorithm(populationSize, numberOfGenes, lengthOfGene, mutationRate, eliteRate, numberOfTrials, fitnessCalculation, seed);
    }
  }
}