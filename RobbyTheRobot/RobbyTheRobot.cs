using System;

namespace RobbyTheRobot
{
    public class RobbyTheRobot : IRobbyTheRobot
    {
        public int NumberOfActions {get;}
        public int NumberOfTestGrids {get;}
        public int GridSize {get;}
        public int NumberOfGenerations {get;}

        public double MutationRate {get;}

        public double EliteRate {get;}

        /// <summary>
        /// Used to generate a single test grid filled with cans in random locations. Half of 
        /// the grid (rounded down) will be filled with cans. Use the GridSize to determine the size of the grid
        /// </summary>
        /// <returns>Rectangular array of Contents filled with 50% Cans, and 50% Empty </returns>
        public ContentsOfGrid[,] GenerateRandomTestGrid()
        {
            return null;
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

    }
}

