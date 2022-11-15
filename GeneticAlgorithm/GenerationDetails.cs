namespace GeneticAlgorithm
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    internal class GenerationDetails : IGenerationDetails
    {
        private IGeneticAlgorithm _geneticAlgorithm;
        private FitnessEventHandler _fitnessEventHandler;
        private int? _seed;
        private IChromosome[] _chromosomes;
        public GenerationDetails(IGeneticAlgorithm geneticAlgorithm, FitnessEventHandler fitnessEventHandler, int? seed = null)
        {
            if (geneticAlgorithm == null || fitnessEventHandler == null)
                throw new NullReferenceException("null object");

            _geneticAlgorithm = geneticAlgorithm;
            // create a random generation
            _chromosomes = GenerateFirstGeneration();
            _fitnessEventHandler = fitnessEventHandler;
            _seed = seed;
            if (_seed == null)
                EvaluateFitnessOfPopulation();

        }


        public GenerationDetails(IChromosome[] chromosome, IGeneticAlgorithm geneticAlgorithm, int? seed = null)
        {
            if (chromosome == null || geneticAlgorithm == null)
                throw new NullReferenceException("null object");
            // initialize array chromosomes
            _chromosomes = new Chromosome[chromosome.Length];
            for (int i = 0; i < chromosome.Length; i++)
            {
                _chromosomes[i] = chromosome[i];
            }
            _seed = seed;
            _geneticAlgorithm = geneticAlgorithm;
            _fitnessEventHandler = geneticAlgorithm.FitnessCalculation;
            if (_seed == null)
                EvaluateFitnessOfPopulation();
        }
        public double AverageFitness
        {
            get
            {
                double totalFitness = 0;
                foreach (IChromosome chromosome in _chromosomes)
                {
                    totalFitness += chromosome.Fitness;
                }
                return totalFitness / _chromosomes.Length;
            }
        }
        public double MaxFitness
        {
            get
            {
                int highest = 0;
                for (int i = 1; i < _chromosomes.Length; i++)
                {
                    if (_chromosomes[highest].Fitness < _chromosomes[i].Fitness)
                    {
                        highest = i;
                    }
                }
                return _chromosomes[highest].Fitness;
            }
        }

        public long NumberOfChromosomes { get => _chromosomes.Length; }

        public IChromosome this[int index]
        {
            get
            {
                return _chromosomes[index];
            }
        }
        public IChromosome SelectParent()
        {
            Random random = new Random();
            if (_seed != null)
                random = new Random((int)_seed);


            int size = 20;
            int highest = random.Next((int)NumberOfChromosomes);
            // Select highest parent based on subset of chromosomes
            for (int i = 0; i < size; i++)
            {
                int index = random.Next((int)NumberOfChromosomes);
                if (_chromosomes[index].Fitness > _chromosomes[highest].Fitness)
                {
                    highest = index;
                }
            }
            return _chromosomes[highest];
        }

        public void EvaluateFitnessOfPopulation()
        {


            if (_geneticAlgorithm.NumberOfTrials > 1)
            {
                // Evaluate fitness for each chromosome
                Parallel.ForEach(_chromosomes as Chromosome[], chromosome =>
              {
                  double total = 0;
                  for (int i = 0; i < _geneticAlgorithm.NumberOfTrials; i++)
                  {
                      total += _fitnessEventHandler.Invoke(chromosome, this);
                  }

                  double averageFitness = total / _geneticAlgorithm.NumberOfTrials;
                  chromosome.Fitness = averageFitness;
              });
                // Sort chromosomes based on their fitness after evaluation
                List<IChromosome> list = new List<IChromosome>(_chromosomes);
                list.Sort();
                _chromosomes = list.ToArray();
            }

        }
        private IChromosome[] GenerateFirstGeneration()
        {
            Random random = new Random();

            IChromosome[] chromosomes = new Chromosome[_geneticAlgorithm.PopulationSize];
            // move to chromosomes constructor 
            for (int i = 0; i < chromosomes.Length; i++)
            {
                chromosomes[i] = new Chromosome(_geneticAlgorithm.NumberOfGenes, _geneticAlgorithm.LengthOfGene);
            }
            return chromosomes;
        }
    }
}
