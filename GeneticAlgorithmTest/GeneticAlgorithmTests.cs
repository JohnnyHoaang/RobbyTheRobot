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
      IGeneticAlgorithm geneticAlg = GeneticLib.CreateGeneticAlgorithm(200, 10, 10, 1.4, 26.5, 10, ComputeFitness, null);

      IGeneration firstGen = geneticAlg.GenerateGeneration();
      Assert.AreEqual(geneticAlg.CurrentGeneration, firstGen);
      Assert.AreEqual(firstGen.NumberOfChromosomes, 200);

      IGeneration secondGen = geneticAlg.GenerateGeneration();
      Assert.AreEqual(secondGen.NumberOfChromosomes, 200);
      Assert.AreEqual(geneticAlg.CurrentGeneration, secondGen);

      Assert.AreNotEqual(firstGen, secondGen);
      Assert.AreNotEqual(firstGen, geneticAlg.CurrentGeneration);

      IGeneration thirdGen = geneticAlg.GenerateGeneration();

      Assert.AreEqual(thirdGen.NumberOfChromosomes, 200);
      Assert.AreEqual(geneticAlg.CurrentGeneration, thirdGen);
      Assert.AreNotEqual(thirdGen, secondGen);
    }
  }
}