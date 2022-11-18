namespace RobbyTheRobot
{
  public static class Robby
  {
    public static IRobbyTheRobot CreateRobby(int numberOfGenerations, int populationSize, int numberOfTrials, double mutationRate, double eliteRate, int? seed = null)
    {
      return new RobbyTheRobot(numberOfGenerations, populationSize, numberOfTrials, mutationRate, eliteRate, seed);
    }
  }
}