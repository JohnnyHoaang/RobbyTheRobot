using System;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using RobbyTheRobot;
namespace RobbyIterationGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            IRobbyTheRobot robby = null;
            while (true)
            {
                try
                {
                    // Receive input from user to create generation
                    Console.WriteLine("Give input to generate Robby!\n");
                    Console.WriteLine("Enter the number of generations: ");
                    int numberOfGenerations = Int32.Parse(Console.ReadLine());
                    Console.WriteLine("Enter the population size: ");
                    int populationSize = Int32.Parse(Console.ReadLine());
                    Console.WriteLine("Enter the number of trials: ");
                    int numberOfTrials = Int32.Parse(Console.ReadLine());
                    Console.WriteLine("Enter the mutation rate: ");
                    double mutationRate = Double.Parse(Console.ReadLine());
                    Console.WriteLine("Enter the elite rate: ");
                    double eliteRate = Double.Parse(Console.ReadLine());
                    robby = Robby.CreateRobby(numberOfGenerations, populationSize, numberOfTrials, mutationRate, eliteRate);
                    break;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message + '\n');
                }
            }
            Console.WriteLine("Enter folder name that will hold generation results:");
            String folderPath = "./" + Console.ReadLine();
            if (!Directory.Exists(folderPath))
            {
                // Create Directory if does not exist
                Directory.CreateDirectory(folderPath);
            }
            Stopwatch stopwatch = new Stopwatch();
            Console.WriteLine("\nGenerating generations...");
            Console.WriteLine("Press ESC to stop\n");
            CancellationTokenSource cancel = new CancellationTokenSource();
            Task task = Task.Run(() =>
            {
                stopwatch.Start();
                robby.GeneratePossibleSolutions(folderPath);
            });
            // Keeps generating until escape press
            Console.WriteLine(task.IsCompleted);
            while (Console.ReadKey(true).Key != ConsoleKey.Escape || task.IsCompleted) ;
            // Force stops generation
            cancel.Cancel();
            stopwatch.Stop();
            Console.WriteLine("Generation time: {0} seconds", (float)stopwatch.ElapsedMilliseconds / 1000);
        }
    }
}
