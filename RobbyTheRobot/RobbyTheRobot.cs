using System;
using System.Linq;
using GeneticAlgorithm;

namespace RobbyTheRobot
{
    public class RobbyTheRobot : IRobbyTheRobot
    {
        private int? _seed;
        private int _populationSize;
        private int _numberOfTrials;
        private int _numberOfGenerations;
        public RobbyTheRobot(int numberOfGenerations, int populationSize, int numberOfTrials, int? seed = null)
        {
            _numberOfGenerations = numberOfGenerations;
            _populationSize = populationSize;
            _numberOfTrials = numberOfTrials;
            _seed = seed;
        }
        public int NumberOfActions { get => 200; }
        public int NumberOfTestGrids { get => 100; }
        public int GridSize { get => 10; }
        public int NumberOfGenerations
        {
            get => _numberOfGenerations;
            set
            {
                _numberOfGenerations = value;
            }
        }
        public double MutationRate { get; }

        public double EliteRate { get; }

        public delegate void FileWriteAction(string fileName, int progress);
        public event FileWriteAction FileWritten;

        public ContentsOfGrid[,] GenerateRandomTestGrid()
        {
            Random random = new Random();
            int numberOfCans = Convert.ToInt32(NumberOfTestGrids / 2);
            int positionX = random.Next(GridSize);
            int positionY = random.Next(GridSize);
            int count = 0;
            ContentsOfGrid[,] grid = new ContentsOfGrid[GridSize, GridSize];
            while (count < numberOfCans)
            {
                bool check = false;
                while (!check)
                {
                    // Check if grid already has a can 
                    if (grid[positionX, positionY] == ContentsOfGrid.Can)
                    {
                        positionX = random.Next(GridSize);
                        positionY = random.Next(GridSize);
                    }
                    else
                    {
                        check = true;
                    }
                }
                // Place can grid
                grid[positionX, positionY] = ContentsOfGrid.Can;
                count++;
            }
            for (int i = 0; i < grid.GetLength(0); i++)
            {
                for (int j = 0; j < grid.GetLength(1); j++)
                {
                    if (grid[i, j] != ContentsOfGrid.Can)
                    {
                        // Place empty grid
                        grid[i, j] = ContentsOfGrid.Empty;
                    }
                }
            }
            return grid;
        }

        public void GeneratePossibleSolutions(string folderPath)
        {
            FileWritten = ShowGenerationProgress;
            IGeneticAlgorithm geneticAlgorithm = GeneticLib.CreateGeneticAlgorithm(_populationSize, 243, 7, 0.01, 0.10, _numberOfTrials, ComputeFitness);
            int count = 0;
            int[] savedGenerations = { 1, 20, 100, 200, 500, 1000 };
            for (int i = 0; i < NumberOfGenerations; i++)
            {
                IGeneration generation = geneticAlgorithm.GenerateGeneration();
                // Check number of generations
                if (savedGenerations.Contains(i + 1))
                {
                    // Choose best generation
                    int[] highestGeneration = generation[0].Genes;
                    Console.WriteLine("Fitness: " + generation[0].Fitness);
                    string genes = "";
                    for (int j = 0; j < highestGeneration.Length; j++)
                    {
                        genes += highestGeneration[j];
                    }
                    String fileName = String.Format("/solution{0}.txt", i + 1);
                    String solution = String.Format("{0},{1},{2}", 500, NumberOfActions, genes);
                    // Write Generation solutions on file
                    using (System.IO.StreamWriter sw = System.IO.File.CreateText(folderPath + fileName))
                    {
                        sw.WriteLine(solution);
                        // Invoke event when file is written
                        count++;
                        FileWritten.Invoke(fileName, count);
                        sw.Close();
                    }
                }
            }
        }
        private void ShowGenerationProgress(String fileName, int progress)
        {
            Console.WriteLine("Generated file: " + fileName);
            Console.WriteLine("Progress: " + progress + " out of 6 files generated");
        }

        public double ComputeFitness(IChromosome chromosome, IGeneration generation)
        {
            Random random = new Random();
            int x = random.Next(10);
            int y = random.Next(10);
            double fitness = 0;
            ContentsOfGrid[,] grid = GenerateRandomTestGrid();
            for (int i = 0; i < NumberOfActions; i++)
            {
                fitness += RobbyHelper.ScoreForAllele(chromosome.Genes, grid, random, ref x, ref y);
            }

            return fitness;
        }
    }
}


