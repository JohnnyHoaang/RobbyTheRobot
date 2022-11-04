using System;
namespace GeneticAlgorithm
{
  internal class GeneticAlgorithm : IGeneticAlgorithm
  {
    public int PopulationSize { get; }

    public int NumberOfGenes { get; }

    public int LengthOfGene { get; }

    public double MutationRate { get; }

    public double EliteRate { get; }

    public int NumberOfTrials { get; }

    public long GenerationCount { get; }
    public IGeneration CurrentGeneration { get; private set; }

    public FitnessEventHandler FitnessCalculation { get; }

    private int? _seed;
    public IGeneration GenerateGeneration()
    {
      if (CurrentGeneration == null)
      {
        CurrentGeneration = new Generation(this, FitnessCalculation, _seed);
      }
      else
      {
        var bestChromosomeCount = (int)(CurrentGeneration.NumberOfChromosomes * EliteRate / 100);
      }
      return CurrentGeneration;
    }

    public GeneticAlgorithm(int populationSize, int numberOfGenes, int lengthOfGene, double mutationRate, double eliteRate, int numberOfTrials, FitnessEventHandler fitnessCalculation, int? seed = null)
    {
      PopulationSize = populationSize;
      NumberOfGenes = numberOfGenes;
      LengthOfGene = lengthOfGene;
      MutationRate = mutationRate;
      EliteRate = eliteRate;
      NumberOfTrials = numberOfTrials;
      FitnessCalculation = fitnessCalculation;
      _seed = seed;
      CurrentGeneration = null;
    }
  }
}
