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

    public long GenerationCount { get; private set; }
    public IGeneration CurrentGeneration { get; private set; }

    public FitnessEventHandler FitnessCalculation { get; }

    private int? _seed;
    public IGeneration GenerateGeneration()
    {
      GenerationCount++;
      if (CurrentGeneration is null)
      {
        this.CurrentGeneration = new GenerationDetails(this, this.FitnessCalculation, _seed);
      }
      else
      {
        var elites = (int)(this.CurrentGeneration.NumberOfChromosomes * EliteRate);

        IChromosome[] bestChromosomes = new IChromosome[elites];
        List<IChromosome> newGen = new List<IChromosome>();

        for (int i = 0; i < elites; i++)
        {
          bestChromosomes[i] = CurrentGeneration[i];
        }
        newGen.AddRange(bestChromosomes);

        while (newGen.Count < this.PopulationSize)
        {
          IChromosome firstParent = ((GenerationDetails)CurrentGeneration).SelectParent();
          IChromosome secondParent = ((GenerationDetails)CurrentGeneration).SelectParent();
          newGen.AddRange(firstParent.Reproduce(secondParent, MutationRate));
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
