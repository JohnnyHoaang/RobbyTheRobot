using System.Diagnostics.CodeAnalysis;
using System;
using System.Collections.Generic;

namespace GeneticAlgorithm
{
    internal class Chromosome : IChromosome
    {
        private int? _seed;
        private int _numOfGenes;
        public Chromosome(int numOfGenes, long length, int? seed = null)
        {
            // (int numberOfGenes, long length, int? seed = null)
            // generate random genes here
            if (length == 0)
                throw new ApplicationException("Length cannot be 0");
            else if(numOfGenes == 0)
                throw new ApplicationException("Num of genes cannot be 0");
            _numOfGenes = numOfGenes;
            Genes = GenerateGenes();
            Length = length;
            _seed = seed;
        }
        public Chromosome(Chromosome chromosome)
        {
            if (chromosome == null)
                throw new ApplicationException("Chromosone cannot be null");
            Genes = Copy(chromosome.Genes);
            Length = chromosome.Length;
            Fitness = chromosome.Fitness;
        }
        public double Fitness { get; set; }

        public int[] Genes { get; set; }

        public long Length { get; }

        public int this[int index] => Genes[index];
        // Crosses 2 parent chromosomes and returns 2 child chromosomes
        private int[] GenerateGenes()
        {
            Random random = new Random();
            int [] genes =  new int[_numOfGenes];
            for(int i=0; i<genes.Length; i++)
            {
                genes[i] = (int)random.Next((int)Length);
            }
            return genes;
        }
        private IChromosome[] Crossover(IChromosome spouse, List<int> points)
        {
            Chromosome firstChild = new Chromosome(_numOfGenes,Length);
            Chromosome secondChild = new Chromosome(_numOfGenes,Length);
            int[] firstGenes = Copy(spouse.Genes);
            int[] secondGenes = Copy(Genes);
            for (int i = 0; i < Genes.Length; i++)
            {
                if (i < points[0] || i > points[1])
                {
                    // Swap chromosome
                    firstGenes[i] = this[i];
                    secondGenes[i] = spouse[i];
                }
            }
            firstChild.Genes = firstGenes;
            secondChild.Genes = secondGenes;
            IChromosome[] children = { firstChild, secondChild };
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
            Random random = new Random();
            if (_seed != null)
                random = new Random((int)_seed);
            List<int> points = new List<int>();

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
            if (_seed != null)
                random = new Random((int)_seed);
            int actions = (int)Length;
            int count = 0;
            foreach (IChromosome chromosome in chromosomes)
            {
                count = 0;
                // Iterates through each gene to check mutation possibilities
                foreach (int gene in chromosome.Genes)
                {
                    // Mutates according to probability
                    if (random.NextDouble() < mutationProb)
                    {
                        int randomMutation = random.Next(actions);
                        chromosome.Genes[count] = randomMutation;
                        count++;
                    }
                }
            }
            return chromosomes;
        }
        public IChromosome[] Reproduce(IChromosome spouse, double mutationProb)
        {
            // Generates 2 points and cross chromosomes
            List<int> points = GeneratePoints(Convert.ToInt32(Genes.Length));
            IChromosome[] chromosomes = Crossover(spouse, points);
            chromosomes = Mutate(chromosomes, mutationProb);
            return chromosomes;
        }
        public int CompareTo(IChromosome other)
        {
            return other.Fitness.CompareTo(Fitness);
        }
    }
}