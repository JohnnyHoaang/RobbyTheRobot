using System.Diagnostics.CodeAnalysis;
using System;
namespace GeneticAlgorithm
{
    internal class Chromosome : IChromosome
    {
        private int? _seed;
        public Chromosome(int[] genes, long length, int? seed = null)
        {
            if(length == 0)
                throw new ApplicationException("Length cannot be 0");
            Genes = genes;
            Length = length;
            _seed = seed;
        }
        public Chromosome(Chromosome chromosone)
        {
            if(chromosone == null)
                throw new ApplicationException("Chromosone cannot be null");
            Genes = chromosone.Genes;
            Length = chromosone.Length;
            Fitness = chromosone.Fitness;
        }
        public double Fitness { get; }

        public int[] Genes { get; }

        public long Length { get; }

        public int this[int index] => Genes[index];

        public void CrossChromosones()
        {

        }
        public IChromosome[] Reproduce(IChromosome spouse, double mutationProb)
        {
            return null;
        }

        public int CompareTo(IChromosome other)
        {
            return Fitness.CompareTo(other.Fitness);
        }

    }
}