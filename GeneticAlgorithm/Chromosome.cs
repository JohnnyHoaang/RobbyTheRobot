using System.Diagnostics.CodeAnalysis;
using System;
using System.Collections.Generic;

namespace GeneticAlgorithm
{
    public class Chromosome : IChromosome
    {
        private int? _seed;
        public Chromosome(int[] genes, long length, int? seed = null)
        {
            if (length == 0)
                throw new ApplicationException("Length cannot be 0");
            Genes = genes;
            Length = length;
            _seed = seed;
        }
        public Chromosome(Chromosome chromosome)
        {
            if (chromosome == null)
                throw new ApplicationException("Chromosone cannot be null");
            Genes = chromosome.Genes;
            Length = chromosome.Length;
            Fitness = chromosome.Fitness;
        }
        public double Fitness { get; private set; }

        public int[] Genes { get; }

        public long Length { get; }

        public int this[int index] => Genes[index];
        // Crosses 2 parent chromosomes and returns 2 child chromosomes
        private IChromosome[] CrossChromosomes(IChromosome spouse, List<int> points)
        {
            int[] firstGenes = Copy(spouse.Genes);
            int[] secondGenes = Copy(Genes);
            for (int i = 0; i < Length; i++)
            {
                if (i < points[0] || i > points[1])
                {
                    // Swap chromosome
                    firstGenes[i] = this[i];
                    secondGenes[i] = spouse[i];
                }
            }
            IChromosome first = new Chromosome(firstGenes, firstGenes.Length);
            IChromosome second = new Chromosome(secondGenes, secondGenes.Length);
            IChromosome[] children = { first, second };
            return children;
        }
        // Returns deep copy of given int array
        private int[] Copy(int[] genes)
        {
            int[] newGenes = new int[genes.Length];
            for (int i = 0; i < genes.Length; i++)
            {
                newGenes[i] = genes[i];
            }
            return newGenes;
        }
        private List<int> GeneratePoints(int size)
        {
            List<int> points = new List<int>();
            Random random = new Random();
            int first = 0;
            int second = 0;
            // Generates different points
            while (first == second)
            {
                first = random.Next(size);
                second = random.Next(size);
            }
            points.Add(first);
            points.Add(second);
            // Sort the points by ascending order
            points.Sort();
            return points;
        }
        private IChromosome[] Mutate(IChromosome[] chromosomes, double mutationProb)
        {
            Random random = new Random();
            const int ACTIONS = 7;
            foreach (IChromosome chromosome in chromosomes)
            {
                int randomIndex = random.Next(Convert.ToInt32(chromosome.Length));
                int randomMutation = random.Next(ACTIONS);
                // Mutates according to probability
                if (random.NextDouble() < mutationProb)
                {
                    chromosome.Genes[randomIndex] = randomMutation;
                }
            }
            return chromosomes;
        }
        public IChromosome[] Reproduce(IChromosome spouse, double mutationProb)
        {
            // Generates 2 points and cross chromosomes
            List<int> points = GeneratePoints(Convert.ToInt32(this.Length));
            IChromosome[] chromosomes = CrossChromosomes(spouse, points);
            chromosomes = Mutate(chromosomes, mutationProb);
            return chromosomes;
        }
        public int CompareTo(IChromosome other)
        {
            return Fitness.CompareTo(other.Fitness);
        }

    }
}