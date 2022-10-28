using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using GeneticAlgorithm;
namespace GeneticAlgorithmTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void TestGenerationConstructor()
        {   
         
            Generation generation = new Generation(null, null, 0);
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void TestGenerationChromosomes()
        {   
                     
            Generation generation = new Generation(null);
         
        }

    }
}
