using Microsoft.VisualStudio.TestTools.UnitTesting;
using GeneticAlgorithm;
using System;

namespace GeneticAlgorithmTest
{
  [TestClass]
  public class GeneticAlgorithmTests
  {
    private double ComputeFitness(IChromosome chromosome, IGeneration generation) { return 2.0; }
    [TestMethod]
    public void GenerateGenerationTest()
    {
      IGeneticAlgorithm geneticAlg = GeneticLib.CreateGeneticAlgorithm(10, 10, 10, 1.4, 50.0, 10, ComputeFitness, null);
      IGeneration firstGen = geneticAlg.GenerateGeneration();
      // Assert.AreEqual(geneticAlg.CurrentGeneration.NumberOfChromosomes, 10);
      Assert.AreEqual(geneticAlg.CurrentGeneration, firstGen);
      IGeneration secondGen = geneticAlg.GenerateGeneration();
      // Assert.IsNull(geneticAlg.CurrentGeneration);
      Assert.AreEqual(geneticAlg.CurrentGeneration, secondGen);

    }
  }
}