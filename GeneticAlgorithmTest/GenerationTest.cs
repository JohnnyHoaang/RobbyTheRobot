using Microsoft.VisualStudio.TestTools.UnitTesting;

using GeneticAlgorithm;
using System.Collections.Generic;
using System;

namespace GeneticAlgorithmTest
{
  [TestClass]
  public class GenerationTest
  {
    [TestMethod]
    public void NumberOfChromosomesChromosomesTest()
    {
      int[] arr = { 9, 11, 5, 7, 20, 19, 12, 3, 4, 7 };
      IChromosome[] list = new Chromosome[10];

      for (int i = 0; i < list.Length; i++)
      {
        Chromosome chromo = new Chromosome(null, 10, null);
        chromo.Fitness = arr[i];
        list[i] = chromo;
      }

      IGeneration generation = new GenerationDetails(list);
      Assert.AreEqual(10, generation.NumberOfChromosomes);
    }

    [TestMethod]
    public void AverageChromosomesTest()
    {
      int[] arr = { 9, 11, 5, 7, 20, 19, 12, 3, 4, 7 };
      IChromosome[] list = new Chromosome[10];

      for (int i = 0; i < list.Length; i++)
      {
        Chromosome chromo = new Chromosome(null, 10, null);
        chromo.Fitness = arr[i];
        list[i] = chromo;
      }

      IGeneration generation = new GenerationDetails(list);
      Assert.AreEqual(9.7, generation.AverageFitness);
    }

    [TestMethod]
    public void MaxFitnessChromosomesTest()
    {

      int[] arr = { 9, 11, 5, 7, 20, 19, 12, 3, 4, 7 };
      IChromosome[] list = new Chromosome[10];

      for (int i = 0; i < list.Length; i++)
      {
        Chromosome chromo = new Chromosome(null, 10, null);
        chromo.Fitness = arr[i];
        list[i] = chromo;
      }

      IGeneration generation = new GenerationDetails(list);
      Assert.AreEqual(20, generation.MaxFitness);
    }

    [TestMethod]
    public void SelectParentTest()
    {
      int[] arr = { 9, 11, 5, 7, 20, 19, 12, 3, 4, 7 };
      IChromosome[] list = new Chromosome[10];

      for (int i = 0; i < list.Length; i++)
      {
        Chromosome chromo = new Chromosome(null, 10, null);
        chromo.Fitness = arr[i];
        list[i] = chromo;
      }

      IGenerationDetails generation = new GenerationDetails(list);
      // get the chromosome with highest fitness
      IChromosome highestChromo = generation.SelectParent();
      // use fitness value of retrun chromosome to check position in array
      Assert.AreEqual(20, highestChromo.Fitness);

    }

    [TestMethod]
    public void EvaluatePopulationTest()
    {
      int[] arr = { 9, 11, 5, 7, 20, 19, 12, 3, 4, 7 };
      IChromosome[] list = new Chromosome[10];

      for (int i = 0; i < list.Length; i++)
      {
        Chromosome chromo = new Chromosome(null, 10, null);
        chromo.Fitness = arr[i];
        list[i] = chromo;
      }
    }
  }
}