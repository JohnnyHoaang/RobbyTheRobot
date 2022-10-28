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
          int[] arr = {1,2,3};
          
          IChromosome chromosome = new Chromosome(arr, 3);
          IChromosome[] chrome = {chromosome};
          IGeneration generation = new Generation(chrome); 
          Assert.AreEqual(1,generation.NumberOfChromosomes);
        }

        [TestMethod]
        public void AverageChromosomesTest()
        {
          int[] arr = {1,2,3};
          
          IChromosome chromosome = new Chromosome(arr, 3);
          IChromosome[] chrome = {chromosome};
          
          IGeneration generation = new Generation(chrome); 
          Assert.AreEqual(0,generation.AverageFitness);
        }

          [TestMethod]
        public void MaxFitnessChromosomesTest()
        {
          int[] arr = {1,2,3};
          
          IChromosome chromosome = new Chromosome(arr, 3);
          IChromosome[] chrome = {chromosome};
          IGeneration generation = new Generation(chrome); 
          Assert.AreEqual(0,generation.MaxFitness);
        }
    }
}