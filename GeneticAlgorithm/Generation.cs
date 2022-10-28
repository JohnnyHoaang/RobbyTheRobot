namespace GeneticAlgorithm
{
  using System;
  public class Generation : IGeneration
  {

    public delegate double FitnessEventHandler(IChromosome chromosome, IGeneration generation);

    private IGeneticAlgorithm _geneticAlgorithm;
    private FitnessEventHandler _fitnessEventHandler;
    private int? _seed;
    private IChromosome[] _chromosomes;


    // <summary>
    /// The average fitness across all Chromosomes
    /// </summary>
    public double AverageFitness
    {
      get
      {

        double totalFitness = 0;
        foreach (var chromosome in _chromosomes)
        {
          totalFitness += chromosome.Fitness;
        }

        return totalFitness / _chromosomes.Length;

      }
    }

    /// <summary>
    /// The maximum fitness across all Chromosomes
    /// </summary>
    public double MaxFitness
    {
      get
      {
        int highest = 0;
        for (int i = 1; i < _chromosomes.Length; i++)
        {
          if (_chromosomes[highest].Fitness < _chromosomes[i].Fitness)
          {
            highest = i;
          }
        }
        return _chromosomes[highest].Fitness;
      }
    }

    /// <summary>
    /// Returns the number of Chromosomes in the generation
    /// </summary>
    public long NumberOfChromosomes { get => _chromosomes.Length; }



    public Generation(IGeneticAlgorithm geneticAlgorithm, FitnessEventHandler fitnessEventHandler, int? seed)
    {
      if (geneticAlgorithm == null || fitnessEventHandler == null)
      {
        throw new NullReferenceException("null object");
      }
      _geneticAlgorithm = geneticAlgorithm;
      _fitnessEventHandler = fitnessEventHandler;
      _seed = seed;
    }


    public Generation(IChromosome[] chromosome)
    {
      if (chromosome == null)
      {
        throw new NullReferenceException("null object");
      }

      // initialize array chromosomes
      _chromosomes = new IChromosome[chromosome.Length];
      for (int i = 0; i < chromosome.Length; i++)
      {
        _chromosomes[i] = chromosome[i];

      }

    }

    /// <summary>
    /// Retrieves the IChromosome from the generation
    /// </summary>
    /// <value>The selected IChromosome</value>
    public IChromosome this[int index]
    {
      get
      {
        //TODO: validate index
        return _chromosomes[index];

      }
    }


    // TODO :

    internal class GenerationDetails : IGenerationDetails
    {
      public double AverageFitness => throw new NotImplementedException();

      public double MaxFitness => throw new NotImplementedException();

      public long NumberOfChromosomes => throw new NotImplementedException();

      public IChromosome this[int index] => throw new NotImplementedException();

      /// <summary>
      /// Randomly selects a parent by comparing its fitness to others in the population
      /// </summary>
      /// <returns></returns>
      IChromosome IGenerationDetails.SelectParent()
      {
        throw new NotImplementedException();
      }

      /// <summary>
      /// Computes the fitness of all the Chromosomes in the generation. 
      /// Note, a FitnessEventHandler deleagte is invoked for every fitness function that must be calculated and is provided by the user
      /// Note, if NumberOfTrials is greater than 1 in IGeneticAlgorithm, 
      /// the average of the number of trials is used to compute the final fitness of the Chromosome.
      /// </summary>
      void IGenerationDetails.EvaluateFitnessOfPopulation()
      {
        throw new NotImplementedException();
      }


    }

  }
}
