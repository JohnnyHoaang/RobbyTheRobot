using System;
using System.Collections.Generic;

namespace GeneticAlgorithm
{
  internal class Chromosome : IChromosome
  {
    private int? _seed;
    private int _numOfGenes;

    /// <summary>
    /// Constructor that creates a chromosome based on the number of genes, the length of genes and a possible seed
    /// </summary>
    /// <returns>Chromosome : IChromosome</returns>
    public Chromosome(int numOfGenes, long length, int? seed = null)
    {
      if (length == 0 || numOfGenes == 0)
        throw new ApplicationException("Num of genes or Length cannot be 0");
      _numOfGenes = numOfGenes;
      Length = length;
      Genes = GenerateGenes();
      _seed = seed;
    }

    /// <summary>
    /// Constructor that creates a deep copy of a given array of chromosomes
    /// </summary>
    /// <returns>Chromosome : IChromosome</returns>
    public Chromosome(Chromosome chromosome)
    {
      if (chromosome == null)
        throw new NullReferenceException("Chromosome cannot be null");
      Genes = Copy(chromosome.Genes);
      Length = chromosome.Length;
      Fitness = chromosome.Fitness;
    }
    public double Fitness { get; set; }

    public int[] Genes { get; set; }

    public long Length { get; }

    public int this[int index] => Genes[index];
    // Crosses 2 parent chromosomes and returns 2 child chromosomes
    public IChromosome[] Reproduce(IChromosome spouse, double mutationProb)
    {
      // Generates 2 points and cross chromosomes
      List<int> points = GeneratePoints(Convert.ToInt32(Genes.Length));
      IChromosome[] chromosomes = Crossover(spouse, points);
      chromosomes = Mutate(chromosomes, mutationProb);
      return chromosomes;
    }
    public int CompareTo(IChromosome other)
    {
      return other.Fitness.CompareTo(Fitness);
    }

    /// <summary>
    /// Generate the genes for the chromosome
    /// </summary>
    /// <returns>Genes : int[]</returns>
    private int[] GenerateGenes()
    {
      Random random = GetRandom();
      int[] genes = new int[_numOfGenes];
      for (int i = 0; i < genes.Length; i++)
      {
        genes[i] = random.Next((int)Length);
      }
      return genes;
    }

    /// <summary>
    /// Use by reproduce. This facilitates the reproduction of chromosomes and handles the crossing of genes
    /// </summary>
    /// <returns>Chromosome : IChromosome[]</returns>
    private IChromosome[] Crossover(IChromosome spouse, List<int> points)
    {
      Chromosome firstChild = new Chromosome(_numOfGenes, Length);
      Chromosome secondChild = new Chromosome(_numOfGenes, Length);

      int[] firstGenes = Copy(spouse.Genes);
      int[] secondGenes = Copy(Genes);

      for (int i = 0; i < Genes.Length; i++)
      {
        if (i < points[0] || i > points[1])
        {
          // Swap chromosome
          firstGenes[i] = this[i];
          secondGenes[i] = spouse[i];
        }
      }

      firstChild.Genes = firstGenes;
      secondChild.Genes = secondGenes;

      IChromosome[] children = { firstChild, secondChild };
      return children;
    }

    /// <summary>
    /// Creates a deep copy of the given array
    /// </summary>
    /// <returns>Genes : int[]</returns>
    private int[] Copy(int[] genes)
    {
      int[] newGenes = new int[genes.Length];
      for (int i = 0; i < genes.Length; i++)
      {
        newGenes[i] = genes[i];
      }
      return newGenes;
    }
    private List<int> GeneratePoints(int size)
    {
      Random random = GetRandom();
      List<int> points = new List<int>();

      int first = 0;
      int second = 0;
      // Generates different points
      while (first == second)
      {
        first = random.Next(size);
        second = random.Next(size);
      }
      points.Add(first);
      points.Add(second);
      // Sort the points by ascending order
      points.Sort();
      return points;
    }

    /// <summary>
    /// Handles the mutation of a gene
    /// </summary>
    /// <returns>Chromosomes : IChromosome[]</returns>
    private IChromosome[] Mutate(IChromosome[] chromosomes, double mutationProb)
    {
      Random random = GetRandom();
      int actions = (int)Length;
      foreach (IChromosome chromosome in chromosomes)
      {
        // Iterates through each gene to check mutation possibilities
        foreach (int gene in chromosome.Genes)
        {
          // Mutates according to probability
          if (random.NextDouble() < mutationProb)
          {
            int randomMutation = random.Next(actions);
            chromosome.Genes[random.Next(Genes.Length)] = randomMutation;
          }
        }
      }
      return chromosomes;
    }
    private Random GetRandom()
    {
      Random random = new Random();
      if (_seed != null)
      {
        random = new Random((int)_seed);
      }

      return random;
    }
  }
}