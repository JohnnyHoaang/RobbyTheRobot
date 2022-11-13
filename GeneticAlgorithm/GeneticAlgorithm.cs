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
        Console.WriteLine(EliteRate);
        var bestChromosomeCount = (int)(this.CurrentGeneration.NumberOfChromosomes * EliteRate / 100);
        IChromosome[] bestChromosomes = new IChromosome[bestChromosomeCount];
        List<IChromosome> newGen = new List<IChromosome>();
        Console.WriteLine(bestChromosomeCount);
        for (int i = 0; i < bestChromosomeCount; i++)
        {
          bestChromosomes[i] = CurrentGeneration[i];
        }

        newGen.AddRange(bestChromosomes);

        while (newGen.Count < PopulationSize)
        {
          for (int i = 0; i < bestChromosomes.Length; i += 2)
          {
            newGen.AddRange(bestChromosomes[i].Reproduce(bestChromosomes[i + 1], MutationRate));
          }
        }

        CurrentGeneration = new GenerationDetails(newGen.ToArray(), this);
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
