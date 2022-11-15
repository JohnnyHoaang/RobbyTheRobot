using Microsoft.VisualStudio.TestTools.UnitTesting;

using GeneticAlgorithm;
using System.Collections.Generic;
using System;

namespace GeneticAlgorithmTest
{
  [TestClass]
  public class GenerationTest
  {
    IGeneticAlgorithm geneticAlgorithm = GeneticLib.CreateGeneticAlgorithm(200, 243, 7, 0.05, 0.05, 100, CalculateFitness);
    [TestMethod]
    public void NumberOfChromosomesTest()
    {
      IGenerationDetails generation = new GenerationDetails(geneticAlgorithm, geneticAlgorithm.FitnessCalculation);
      Assert.AreEqual(200, generation.NumberOfChromosomes);
    }

    [TestMethod]
    public void AverageChromosomesTest()
    {
      int[] arr = { 9, 11, 5, 7, 20, 19, 12, 3, 4, 7 };
      IChromosome[] list = new Chromosome[10];

      SetChromosomeFitness(arr, list);
      IGeneration generation = new GenerationDetails(list, geneticAlgorithm, 5);
      // Get average fitness of generation
      Assert.AreEqual(9.7, generation.AverageFitness);
    }

    [TestMethod]
    public void MaxFitnessChromosomesTest()
    {

      int[] arr = { 9, 11, 5, 7, 67, 19, 12, 3, 4, 7 };
      IChromosome[] list = new Chromosome[10];
      SetChromosomeFitness(arr, list);
      IGeneration generation = new GenerationDetails(list, geneticAlgorithm, 5);
      // Select chromosome with highest fitness
      Assert.AreEqual(67, generation.MaxFitness);
    }

    [TestMethod]
    public void SelectParentTest()
    {
      int[] arr = { 9, 11, 5, 7, 20, 19, 12, 3, 4, 7 };
      IChromosome[] list = new Chromosome[10];

      for (int i = 0; i < list.Length; i++)
      {
        Chromosome chromosome = new Chromosome(10, 10);
        chromosome.Fitness = arr[i];
        list[i] = chromosome;
      }

      IGenerationDetails generation = new GenerationDetails(list, geneticAlgorithm, 7);
      // Get the chromosome with highest fitness based on subset
      IChromosome highestChromo = generation.SelectParent();
      Assert.AreEqual(20, highestChromo.Fitness);

    }

    [TestMethod]
    public void EvaluatePopulationWithSeedTest()
    {
      IChromosome[] list = new Chromosome[10];
      SetChromosome(list);
      IGenerationDetails generation = new GenerationDetails(list, geneticAlgorithm, 5);
      EvaluationTest(generation);
    }
    [TestMethod]
    public void EvaluatePopulationWithoutSeedTest()
    {
      IChromosome[] list = new Chromosome[10];
      SetChromosome(list);
      IGenerationDetails generation = new GenerationDetails(list, geneticAlgorithm);
      EvaluationTest(generation);
    }
    [TestMethod]
    [ExpectedException(typeof(NullReferenceException))]
    public void nullConstructorErrorTest()
    {
      GenerationDetails generationDetails = new GenerationDetails(null, geneticAlgorithm.FitnessCalculation);
    }

    [TestMethod]
    [ExpectedException(typeof(NullReferenceException))]
    public void nullChromosomeConstructorErrorTest()
    {
      GenerationDetails generationDetails = new GenerationDetails(null, geneticAlgorithm.FitnessCalculation);
    }
    private static double CalculateFitness(IChromosome chromosome, IGeneration generation)
    {
      return 1000;
    }
    private void EvaluationTest(IGenerationDetails generation)
    {
      double averageFitness = CalculateFitness(new Chromosome(10, 10, 5), generation) * geneticAlgorithm.NumberOfTrials / geneticAlgorithm.NumberOfTrials;
      generation.EvaluateFitnessOfPopulation();
      for (int i = 0; i < generation.NumberOfChromosomes; i++)
      {
        // Checks fitness of each chromosome in generation after evaluation
        Assert.AreEqual(averageFitness, generation[i].Fitness);
      }
    }
    private static void SetChromosome(IChromosome[] list)
    {
      for (int i = 0; i < list.Length; i++)
      {
        Chromosome chromo = new Chromosome(10, 10);
        list[i] = chromo;
      }
    }
    private static void SetChromosomeFitness(int[] arr, IChromosome[] list)
    {
      for (int i = 0; i < list.Length; i++)
      {
        Chromosome chromo = new Chromosome(10, 10);
        chromo.Genes = arr;
        list[i] = chromo;
        chromo.Fitness = arr[i];
      }
    }

  }
}