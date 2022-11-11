namespace GeneticAlgorithm
{
  using System;
  using System.Collections.Generic;
  internal class GenerationDetails : IGenerationDetails
  {
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



    public GenerationDetails(IGeneticAlgorithm geneticAlgorithm, int? seed = null)
    {
      FitnessEventHandler fitnessEventHandler = geneticAlgorithm.FitnessCalculation;
      if (geneticAlgorithm == null || fitnessEventHandler == null)
      {
        throw new NullReferenceException("null object");
      }
      _geneticAlgorithm = geneticAlgorithm;
      // create a random generation
      _chromosomes = GenerateFirstGeneration();
      _fitnessEventHandler = fitnessEventHandler;
      _seed = seed;
    }


    public GenerationDetails(IChromosome[] chromosome, IGeneticAlgorithm geneticAlgorithm)
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
      _geneticAlgorithm = geneticAlgorithm;
      _fitnessEventHandler = geneticAlgorithm.FitnessCalculation;

    }

    private IChromosome[] GenerateFirstGeneration()
    {
      Random random = new Random();
      
      IChromosome[] chromosomes = new Chromosome[_geneticAlgorithm.PopulationSize];
      // move to chromosomes constructor 
      for (int i = 0; i< chromosomes.Length; i++)
      {
        int [] genes = new int[_geneticAlgorithm.NumberOfGenes];
        chromosomes[i]= new Chromosome(genes, 7);
        for (int j = 0; j< _geneticAlgorithm.NumberOfGenes; j++)
        {
          int actions = random.Next(7);
          chromosomes[i].Genes[j] = actions;
        }
      }
      return chromosomes;
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


    /// <summary>
    /// Randomly selects a parent by comparing its fitness to others in the population
    /// </summary>
    /// <returns></returns>
    IChromosome IGenerationDetails.SelectParent()
    {
      Random random = new Random();
      if(_seed != null){
        random = new Random((int)_seed);
      }
      
      int size = 10;
      int highest = random.Next((int)NumberOfChromosomes);


      for (int i = 0; i < size; i++)
      {
        int index = random.Next((int)NumberOfChromosomes);
        if (_chromosomes[index].Fitness > _chromosomes[highest].Fitness)
        {
          highest = index;
        }
      }

      return _chromosomes[highest];

    }

    /// <summary>
    /// Computes the fitness of all the Chromosomes in the generation. 
    /// Note, a FitnessEventHandler deleagte is invoked for every fitness function that must be calculated and is provided by the user
    /// Note, if NumberOfTrials is greater than 1 in IGeneticAlgorithm, 
    /// the average of the number of trials is used to compute the final fitness of the Chromosome.
    /// </summary>
    void IGenerationDetails.EvaluateFitnessOfPopulation()
    {
      double total = 0;

      if (_geneticAlgorithm.NumberOfTrials > 1)
      {
        foreach (Chromosome chromo in _chromosomes)
        {
          for (int i = 0; i < _geneticAlgorithm.NumberOfTrials; i++)
          {
            total = +_fitnessEventHandler.Invoke(chromo, this);
          }
          double averageFitness = total / _geneticAlgorithm.NumberOfTrials;
          chromo.Fitness = averageFitness;
        }

        List<IChromosome> list = new List<IChromosome>();
        list.AddRange(_chromosomes);
        list.Sort();
        _chromosomes = list.ToArray();
      }
    }


  }
}
