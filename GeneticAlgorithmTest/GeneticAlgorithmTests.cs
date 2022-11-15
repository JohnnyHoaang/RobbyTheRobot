using Microsoft.VisualStudio.TestTools.UnitTesting;
using GeneticAlgorithm;
using System;

namespace GeneticAlgorithmTest
{
  [TestClass]
  public class GeneticAlgorithmTests
  {
    private double ComputeFitness(IChromosome chromosome, IGeneration generation)
    {
      Random random = new Random();
      return random.Next(100) * random.Next(100);
    }
    [TestMethod]
    public void GenerateGenerationTest()
    {
      IGeneticAlgorithm geneticAlg = GeneticLib.CreateGeneticAlgorithm(200, 70, 70, 0.02, 0.25, 100, ComputeFitness, null);

      IGeneration firstGen = geneticAlg.GenerateGeneration();
      Assert.AreSame(geneticAlg.CurrentGeneration, firstGen);
      Assert.AreEqual(firstGen.NumberOfChromosomes, 200);

      IGeneration secondGen = geneticAlg.GenerateGeneration();
      Assert.AreEqual(secondGen.NumberOfChromosomes, 200);
      Assert.AreSame(geneticAlg.CurrentGeneration, secondGen);

      Assert.AreNotSame(firstGen, secondGen);
      Assert.AreNotSame(firstGen, geneticAlg.CurrentGeneration);

      IGeneration thirdGen = geneticAlg.GenerateGeneration();

      // Comparing the genes and fitness of each chromosomes in the generation
      for (int i = 0; i < 200; i++)
      {
        Assert.AreNotEqual(thirdGen[i].Genes, secondGen[i].Genes);
        Assert.AreNotEqual(secondGen[i].Fitness, firstGen[i].Fitness);
        Assert.AreNotEqual(firstGen[i].Fitness, thirdGen[i].Fitness);
      }

      Assert.AreEqual(thirdGen.NumberOfChromosomes, 200);
      Assert.AreSame(geneticAlg.CurrentGeneration, thirdGen);
      Assert.AreNotSame(thirdGen, secondGen);
    }

    [DataTestMethod]
    [DataRow(0, 0)]
    [DataRow(1.3, 0.2)]
    [DataRow(0.5, 1.0)]
    [DataRow(0.7, 1.4)]
    [DataRow(-0.2, -0.7)]
    [DataRow(-1.2, 0.7)]
    [DataRow(1.0, 1.0)]
    [ExpectedException(typeof(ApplicationException))]
    public void InvalidArgsTests(double mutationRate, double eliteRate)
    {
      IGeneticAlgorithm geneticAlg = GeneticLib.CreateGeneticAlgorithm(200, 70, 70, mutationRate, eliteRate, 100, ComputeFitness, null);
    }
  }
}