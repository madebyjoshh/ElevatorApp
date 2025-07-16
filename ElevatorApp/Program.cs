using ElevatorApp.Business;
using ElevatorApp.Contracts;
using ElevatorApp.Constants;
using ElevatorApp;

class Program
{
    static async Task Main(string[] args)
    {
        var random = new Random();

        int elevatorCount = ElevatorConstants.ElevatorCount;
        int maxFloor = ElevatorConstants.MaxFloor;
        int queueCount = 3;
        int simulationSteps = ElevatorConstants.SimulationSteps;

        var elevators = CreateElevators(elevatorCount, maxFloor, random);
        var simulator = new SimulateElevator(elevators, queueCount, maxFloor, random);

        for (int step = 1; step < simulationSteps; step++)
        {
            DisplaySimulationStep(step, elevators);
            await simulator.RunStepAsync();
        }
    }

    static List<IElevator> CreateElevators(int count, int maxFloor, Random random) =>
        Enumerable.Range(1, count)
                  .Select(id => new Elevator(id, random.Next(1, maxFloor + 1)))
                  .Cast<IElevator>()
                  .ToList();

    static void DisplaySimulationStep(int step, List<IElevator> elevators)
    {
        Console.WriteLine($"\n=== Simulation Step {step} ===");
        elevators.ForEach(e => e.DisplayStatus());
    }
}
