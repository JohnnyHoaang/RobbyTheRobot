using Microsoft.VisualStudio.TestTools.UnitTesting;
using GeneticAlgorithm;
using System.Collections.Generic;
using System;

namespace GeneticAlgorithmTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void ChromosonePointsTest()
        {
            int [] genes = new int[]{1,2,3,4,5,6,7};
            Chromosome chromosome = new Chromosome(genes,genes.Length);
            int [] genes2 = new int[]{9,8,7,6,5,4,3};
            Chromosome chromosome2 = new Chromosome(genes2,genes.Length);
            List<int> points = new List<int>();
            points.Add(2);
            points.Add(4);
            IChromosome[] somes = chromosome.CrossChromosomes(chromosome2, points);
            Assert.AreEqual(2, somes[0][1]);
            Assert.AreEqual(7, somes[0][2]);
            Assert.AreEqual(6, somes[0][3]);
            Assert.AreEqual(5, somes[0][4]);
        }
    }
}
