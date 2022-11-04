using System;

namespace RobbyTheRobot
{
  internal class RobbyTheRobot : IRobbyTheRobot
  {
    private int? _seed;
    private int _populationSize;
    private int _numberOfTrials;
    public RobbyTheRobot(int numberOfGenerations, int populationSize, int numberOfTrials, int? seed = null){
        NumberOfGenerations = numberOfGenerations;
        _populationSize = populationSize;
        _numberOfTrials = numberOfTrials;
        _seed = seed;
    }
    public int NumberOfActions { get; }
    public int NumberOfTestGrids { get; }
    public int GridSize { get; }
    public int NumberOfGenerations { get; }

    public double MutationRate { get; }

    public double EliteRate { get; }

    public delegate void FileWriteAction(string folderPath);

    /// <summary>
    /// Used to generate a single test grid filled with cans in random locations. Half of 
    /// the grid (rounded down) will be filled with cans. Use the GridSize to determine the size of the grid
    /// </summary>
    /// <returns>Rectangular array of Contents filled with 50% Cans, and 50% Empty </returns>
    public ContentsOfGrid[,] GenerateRandomTestGrid()
    {
        Random random = new Random();
      const int numberOfCans = 50;
      const int size = 10;
      int positionX = random.Next(size);
      int positionY = random.Next(size);
      int count = 0;
      ContentsOfGrid[,] grid = new ContentsOfGrid[size, size];
      while (count < numberOfCans)
      {
        bool check = false;
        while (!check)
        {
          // Check if grid already has a can 
          if (grid[positionX, positionY] == ContentsOfGrid.Can){
            positionX = random.Next(size);
            positionY =  random.Next(size);
          } else {
            check = true;
          }
        }
        // Place can grid
        grid[positionX, positionY] = ContentsOfGrid.Can;
        count++;
      }
      for(int i = 0; i<grid.GetLength(0); i++){
        for(int j = 0; j<grid.GetLength(1); j++){
            if(grid[i,j] != ContentsOfGrid.Can){
                // Place empty grid
                grid[i,j] = ContentsOfGrid.Empty;
            }
        }
      }
      return grid;
    }

    /// <summary>
    /// Generates a series of possible solutions based on the generations and saves them to disk.
    /// The text files generated must contain a comma seperated list of the max score, number of moves to display in the gui and all the actions robby will take (i.e the genes in the Chromosome).
    /// The top candidate of the 1st, 20th, 100, 200, 500 and 1000th generation will be saved.
    /// </summary>
    /// <param name="folderPath">The path of the folder where the text files will be saved</param>
    public void GeneratePossibleSolutions(string folderPath)
    {

    }

    /// <summary>
    /// An event raised when a file is written to disk
    /// </summary>
    //event TODOMYCUSTOMDELEGATE FileWritten;
    public event FileWriteAction FileWritten;
    public double ComputeFitness(){
        return 0;
    }
  }
}

