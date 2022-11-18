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
            int numOfGenes = 7;
            int[] genes = new int[] { 1, 2, 3, 4, 5, 6, 7 };
            Chromosome chromosome = new Chromosome(numOfGenes, 7, seed);
            chromosome.Genes = genes;
            int[] secondGenes = new int[] { 9, 8, 7, 6, 5, 4, 3 };
            Chromosome secondChromosome = new Chromosome(numOfGenes, 7);
            secondChromosome.Genes = secondGenes;
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
            int numOfGenes = 7;
            int[] genes = new int[] { 1, 2, 3, 4, 5, 6, 7 };
            Chromosome chromosome = new Chromosome(numOfGenes, 7, seed);
            chromosome.Genes = genes;
            int[] secondGenes = new int[] { 9, 8, 7, 6, 5, 4, 3 };
            Chromosome secondChromosome = new Chromosome(numOfGenes, 7);
            secondChromosome.Genes = secondGenes;
            // seed = 10, mutation rate = 1
            IChromosome[] chromosomes = chromosome.Reproduce(secondChromosome, mutationRate);
            Assert.IsFalse(Array.Equals(chromosomes[0].Genes, genes));
            Assert.IsFalse(Array.Equals(chromosomes[1].Genes, genes));

            // 1 1 5 4 5 5 4 
            // 1 0 0 1 5 6 7
        }
        [TestMethod]
        public void MutateWithLowRateTest()
        {
            int seed = 10;
            double mutationRate = 0;
            int numOfGenes = 7;
            int[] genes = new int[] { 1, 2, 3, 4, 5, 6, 7 };
            Chromosome chromosome = new Chromosome(numOfGenes, 7, seed);
            chromosome.Genes = genes;
            int[] secondGenes = new int[] { 9, 8, 7, 6, 5, 4, 3 };
            Chromosome secondChromosome = new Chromosome(numOfGenes, 7);
            secondChromosome.Genes = secondGenes;
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
            int numOfGenes = 7;
            int[] genes = new int[] { 1, 2, 3, 4, 5, 6, 7 };
            Chromosome chromosome = new Chromosome(numOfGenes, 7, seed);
            chromosome.Genes = genes;
            int[] secondGenes = new int[] { 9, 8, 7, 6, 5, 4, 3 };
            Chromosome secondChromosome = new Chromosome(numOfGenes, 7);
            secondChromosome.Genes = secondGenes;
            // seed = 10, mutation rate = 0.29
            IChromosome[] chromosomes = chromosome.Reproduce(secondChromosome, mutationRate);
            // Mutated
            Assert.AreNotEqual(8, chromosomes[1][1]);
            // was 8, now 0
            Assert.AreEqual(0, chromosomes[1][1]);
            // Mutated
            Assert.AreNotEqual(7, chromosomes[1][2]);
            // Was 7, now 1
            Assert.AreEqual(1, chromosomes[1][2]);
            // Mutated
            Assert.AreNotEqual(5, chromosomes[1][3]);
            // Was 7, now 0
            Assert.AreEqual(6, chromosomes[1][3]);
            // Check if the rest is same  
            for (int i = 0; i < genes.Length; i++)
            {
                // Mutated index = [1,2,3,4,5] , Crossed index = [5,6]
                int[] skipIndex = { 1, 2, 3, 4, 5, 6 };
                if (!skipIndex.Contains(i))
                {
                    Assert.AreEqual(chromosomes[1].Genes[i], secondGenes[i]);
                }
            }
            // Expected Results = [[1, 2, 3, 4, 5, 4, 3],[9, 0, 1, 6, 3, 6, 7]]
        }
        [TestMethod]
        public void SortFitnessTest()
        {
            int numOfGenes = 3;
            Chromosome chromosome = new Chromosome(numOfGenes, 7);
            Chromosome secondChromosome = new Chromosome(numOfGenes, 7);
            Chromosome thirdChromosome = new Chromosome(numOfGenes, 7);
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
        public void constructorGenesErrorTest()
        {
            int genes = 0;
            Chromosome chromosome = new Chromosome(genes, 7);
        }
        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void constructorLengthErrorTest()
        {
            int genes = 3;
            Chromosome chromosome = new Chromosome(genes, 0);
        }
        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void copyConstructorErrorTest()
        {
            Chromosome chromosome = null;
            Chromosome newChromosome = new Chromosome(chromosome);
        }
        [TestMethod]
        public void copyConstructorTest()
        {
            Chromosome chromosome = new Chromosome(10, 7, 2);
            Chromosome copyChromosome = new Chromosome(chromosome);
            for (int i = 0; i < chromosome.Genes.Length; i++)
            {
                Assert.AreEqual(chromosome[i], copyChromosome[i]);
            }
        }
    }
}
