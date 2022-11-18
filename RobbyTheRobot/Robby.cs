namespace RobbyTheRobot
{
  public static class Robby
  {
    /// <summary>
    /// Handles the creation of a RobbyTheRobot without exposing the implementation
    /// </summary>
    /// <returns>Robby the robot : IRobbyTheRobot</returns>
    public static IRobbyTheRobot CreateRobby(int numberOfGenerations, int populationSize, int numberOfTrials, double mutationRate, double eliteRate, int? seed = null)
    {
      return new RobbyTheRobot(numberOfGenerations, populationSize, numberOfTrials, mutationRate, eliteRate, seed);
    }
  }
}