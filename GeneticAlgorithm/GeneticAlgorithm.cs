using System;
using System.Collections.Generic;
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
      if (CurrentGeneration is null)
      {
        this.CurrentGeneration = new GenerationDetails(this, this.FitnessCalculation, _seed);
      }
      else
      {
        var bestChromosomeCount = (int)(this.CurrentGeneration.NumberOfChromosomes * EliteRate);

        if (bestChromosomeCount % 2 != 0)
        {
          bestChromosomeCount = bestChromosomeCount - 1;
        }

        IChromosome[] bestChromosomes = new IChromosome[bestChromosomeCount];
        List<IChromosome> newGen = new List<IChromosome>();

        for (int i = 0; i < bestChromosomeCount; i++)
        {
          bestChromosomes[i] = CurrentGeneration[i];
        }
        newGen.AddRange(bestChromosomes);
        while (newGen.Count < PopulationSize)
        {
          for (int i = 0; i < bestChromosomes.Length; i = i + 2)
          {
            newGen.AddRange(bestChromosomes[i].Reproduce(bestChromosomes[i + 1], MutationRate));
            if (newGen.Count == PopulationSize)
            {
              break;
            }
          }
        }
        CurrentGeneration = new GenerationDetails(newGen.ToArray(), this);
      }
      return CurrentGeneration;
    }

    public GeneticAlgorithm(int populationSize, int numberOfGenes, int lengthOfGene, double mutationRate, double eliteRate, int numberOfTrials, FitnessEventHandler fitnessCalculation, int? seed = null)
    {
      if (mutationRate <= 0 || mutationRate >= 1)
      {
        throw new ApplicationException("Mutation rate should be a decimal between 0 and 1");
      }
      if (eliteRate <= 0 || eliteRate >= 1)
      {
        throw new ApplicationException("Elite rate should be a decimal between 0 and 1");
      }
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
