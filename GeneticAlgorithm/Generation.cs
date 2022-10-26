namespace GeneticAlgorithm
{
  using System;
  internal class Generation : IGeneration
  {

    public delegate double FitnessEventHandler(IChromosome chromosome, IGeneration generation);

    private IGeneticAlgorithm _geneticAlgorithm;
    private FitnessEventHandler _fitnerssEventHandler;
    private int? _seed;

    // <summary>
    /// The average fitness across all Chromosomes
    /// </summary>
    public double AverageFitness { get; }
    /// <summary>
    /// The maximum fitness across all Chromosomes
    /// </summary>
    public double MaxFitness { get; }

    /// <summary>
    /// Returns the number of Chromosomes in the generation
    /// </summary>
    public long NumberOfChromosomes { get; }

    private IChromosome[] _chromosomes;


    public Generation(IGeneticAlgorithm geneticAlgorithm, FitnessEventHandler fitnerssEventHandler, int? seed)
    {
      if (geneticAlgorithm == null || fitnerssEventHandler == null)
      {
        throw new NullReferenceException("null object");
      }
      _geneticAlgorithm = geneticAlgorithm;
      _fitnerssEventHandler = fitnerssEventHandler;
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
    IChromosome IGeneration.this[int index]
    {
      get
      {

          return _chromosomes[index];

      }
    }

  }
  // TODO :
  internal interface IGenerationDetails : IGeneration
  {
    /// <summary>
    /// Randomly selects a parent by comparing its fitness to others in the population
    /// </summary>
    /// <returns></returns>
    IChromosome SelectParent();

    /// <summary>
    /// Computes the fitness of all the Chromosomes in the generation. 
    /// Note, a FitnessEventHandler deleagte is invoked for every fitness function that must be calculated and is provided by the user
    /// Note, if NumberOfTrials is greater than 1 in IGeneticAlgorithm, 
    /// the average of the number of trials is used to compute the final fitness of the Chromosome.
    /// </summary>
    void EvaluateFitnessOfPopulation();
  }
}
