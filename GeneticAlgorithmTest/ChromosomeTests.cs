using Microsoft.VisualStudio.TestTools.UnitTesting;
using GeneticAlgorithm;
using System.Collections.Generic;
using System;
using System.Linq;

namespace GeneticAlgorithmTest
{
    [TestClass]
    public class ChromosomeTests
    {
        [TestMethod]
        public void ChromosomeCrossoverTest()
        {
            int seed = 5;
            int[] genes = new int[] { 1, 2, 3, 4, 5, 6, 7 };
            Chromosome chromosome = new Chromosome(genes, 7, seed);
            int[] secondGenes = new int[] { 9, 8, 7, 6, 5, 4, 3 };
            Chromosome secondChromosome = new Chromosome(secondGenes, 7);
            // seed = 5, points : [1,2]
            IChromosome[] chromosomes = chromosome.Reproduce(secondChromosome, 0.01);
            // still the same
            Assert.AreEqual(1, chromosomes[0][0]);
            Assert.AreEqual(9, chromosomes[1][0]);
            // was 2, now 8
            Assert.AreEqual(8, chromosomes[0][1]);
            // was 8, now 2
            Assert.AreEqual(2, chromosomes[1][1]);
            // was 3, now 7
            Assert.AreEqual(7, chromosomes[0][2]);
            // was 7, now 3
            Assert.AreEqual(3, chromosomes[1][2]);
            // still the same
            Assert.AreEqual(4, chromosomes[0][3]);
            Assert.AreEqual(6, chromosomes[1][3]);
            // Expected Results = [[1, 8, 7, 4, 5, 6, 7],[9, 2, 3, 6, 5, 4, 3]]
        }
        [TestMethod]
        public void MutateWithHighRateTest()
        {
            int seed = 10;
            double mutationRate = 1;
            int[] genes = new int[] { 1, 2, 3, 4, 5, 6, 7 };
            Chromosome chromosome = new Chromosome(genes, 7, seed);
            int[] secondGenes = new int[] { 9, 8, 7, 6, 5, 4, 3 };
            Chromosome secondChromosome = new Chromosome(secondGenes, 7);
            // seed = 10, mutation rate = 1
            IChromosome[] chromosomes = chromosome.Reproduce(secondChromosome, mutationRate);
            for (int i = 0; i < genes.Length; i++)
            {
                Assert.AreNotEqual(chromosomes[0].Genes[i], genes[i]);
                Assert.AreNotEqual(chromosomes[1].Genes[i], secondGenes[i]);
            }
        }
        [TestMethod]
        public void MutateWithLowRateTest()
        {
            int seed = 10;
            double mutationRate = 0;
            int[] genes = new int[] { 1, 2, 3, 4, 5, 6, 7 };
            Chromosome chromosome = new Chromosome(genes, 7, seed);
            int[] secondGenes = new int[] { 9, 8, 7, 6, 5, 4, 3 };
            Chromosome secondChromosome = new Chromosome(secondGenes, 7);
            // seed = 10, mutation rate = 1
            IChromosome[] chromosomes = chromosome.Reproduce(secondChromosome, mutationRate);
            for (int i = 0; i < genes.Length; i++)
            {
                // Crossed index : [5,6]
                int[] skipIndex = { 5, 6 };
                if (!skipIndex.Contains(i))
                {
                    Assert.AreEqual(chromosomes[0].Genes[i], genes[i]);
                    Assert.AreEqual(chromosomes[1].Genes[i], secondGenes[i]);
                }
            }
            // Expected Results = [[1, 2, 3, 4, 5, 4, 3],[9, 8, 7, 6, 5, 6, 7]]
        }
        [TestMethod]
        public void MutateWithMediumRateTest()
        {
            int seed = 10;
            double mutationRate = 0.29;
            int[] genes = new int[] { 1, 2, 3, 4, 5, 6, 7 };
            Chromosome chromosome = new Chromosome(genes, 7, seed);
            int[] secondGenes = new int[] { 9, 8, 7, 6, 5, 4, 3 };
            Chromosome secondChromosome = new Chromosome(secondGenes, 7);
            // seed = 10, mutation rate = 0.29
            IChromosome[] chromosomes = chromosome.Reproduce(secondChromosome, mutationRate);
            // Mutated
            Assert.AreNotEqual(9, chromosomes[1][0]);
            // Was 9, now 6
            Assert.AreEqual(6, chromosomes[1][0]);
            // Mutated
            Assert.AreNotEqual(8, chromosomes[1][1]);
            // was 8, now 3
            Assert.AreEqual(3, chromosomes[1][1]);
            // Mutated
            Assert.AreNotEqual(7, chromosomes[1][2]);
            // Was 7, now 0
            Assert.AreEqual(0, chromosomes[1][2]);
            // Check if the rest is same  
            for (int i = 0; i < genes.Length; i++)
            {
                // Mutated index = [0,1,2] , Crossed index = [5,6]
                int[] skipIndex = { 0, 1, 2, 5, 6 };
                if (!skipIndex.Contains(i))
                {
                    Assert.AreEqual(chromosomes[1].Genes[i], secondGenes[i]);
                }
            }
            // Expected Results = [[1, 2, 3, 4, 5, 4, 3],[6, 3, 0, 6, 5, 6, 7]]
        }
        [TestMethod]
        public void SortFitnessTest()
        {
            int[] genes = new int[] { 1, 2, 3 };
            Chromosome chromosome = new Chromosome(genes, 7);
            Chromosome secondChromosome = new Chromosome(genes, 7);
            Chromosome thirdChromosome = new Chromosome(genes, 7);
            chromosome.Fitness = 10;
            secondChromosome.Fitness = 54;
            thirdChromosome.Fitness = 29;
            IChromosome[] chromosomes = new IChromosome[] { chromosome, secondChromosome, thirdChromosome };
            List<IChromosome> sortedChromosomes = new List<IChromosome>(chromosomes);
            // Sort the list
            sortedChromosomes.Sort();
            Assert.AreEqual(54, sortedChromosomes[0].Fitness);
            Assert.AreEqual(29, sortedChromosomes[1].Fitness);
            Assert.AreEqual(10, sortedChromosomes[2].Fitness);
            // Expected Result = [54,29,10]
        }
        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void constructorGenesTest()
        {
            int[] genes = { };
            Chromosome chromosome = new Chromosome(genes, 7);
        }
        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void constructorLengthTest()
        {
            int[] genes = { 1, 2, 3 };
            Chromosome chromosome = new Chromosome(genes, 0);
        }
        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void copyConstructorTest()
        {
            Chromosome chromosome = null;
            Chromosome newChromosome = new Chromosome(chromosome);
        }
    }
}
