using ElevatorApp.Business;
using ElevatorApp.Constants;
using ElevatorApp.Contracts;

namespace ElevatorApp
{
    /// <summary>
    /// Simulates the operation of a group of elevators, generating passenger requests and assigning them to elevators.
    /// </summary>
    public class SimulateElevator
    {
        private readonly IList<IElevator> _elevators;
        private readonly Random _random;

        /// <summary>
        /// Initializes a new instance of the <see cref="SimulateElevator"/> class.
        /// </summary>
        /// <param name="elevators">The list of elevators to simulate.</param>
        /// <param name="random">The random number generator to use for passenger requests.</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="elevators"/> is null.</exception>
        public SimulateElevator(IList<IElevator> elevators, Random random)
        {
            _elevators = elevators ?? throw new ArgumentNullException(nameof(elevators));
            _random = random ?? new Random();
        }

        /// <summary>
        /// Runs a single simulation step: generates a passenger request, assigns it to an elevator, and advances all elevators.
        /// </summary>
        /// <returns>A task representing the asynchronous operation.</returns>
        public async Task RunStepAsync()
        {
            var passenger = GeneratePassengerRequest();
            AssignPassengerToElevator(passenger);

            var elevatorTasks = _elevators
                .Select(e => (e as IRequestHandler)?.StepAsync() ?? Task.CompletedTask);
            await Task.WhenAll(elevatorTasks);
        }

        /// <summary>
        /// Generates a random passenger request with random start and destination floors.
        /// </summary>
        /// <returns>A new <see cref="Passenger"/> instance.</returns>
        public Passenger GeneratePassengerRequest()
        {
            int start = _random.Next(1, ElevatorConstants.MaxFloor + 1);
            int dest;

            do
            {
                dest = _random.Next(1, ElevatorConstants.MaxFloor + 1);
            } while (dest == start);

            return new Passenger(start, dest);
        }

        /// <summary>
        /// Assigns a passenger request to the closest available elevator.
        /// </summary>
        /// <param name="passenger">The passenger to assign.</param>
        private void AssignPassengerToElevator(Passenger passenger)
        {
            var chosen = _elevators
                .OrderBy(e => Math.Abs(e.CurrentFloor - passenger.StartFloor))
                .FirstOrDefault() as IRequestHandler;

            chosen?.AssignRequest(passenger);
        }
    }
}
