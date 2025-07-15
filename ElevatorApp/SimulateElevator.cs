using ElevatorApp.Business;
using ElevatorApp.Constants;
using ElevatorApp.Contracts;

namespace ElevatorApp
{
    public class SimulateElevator
    {
        private readonly IList<IElevator> _elevators;
        private readonly Random _random;

        public SimulateElevator(IList<IElevator> elevators, Random random)
        {
            _elevators = elevators ?? throw new ArgumentNullException(nameof(elevators));
            _random = random ?? new Random();
        }

        public async Task RunStepAsync()
        {
            var passenger = GeneratePassengerRequest();
            AssignPassengerToElevator(passenger);

            var elevatorTasks = _elevators
                .Select(e => (e as IRequestHandler)?.StepAsync() ?? Task.CompletedTask);
            await Task.WhenAll(elevatorTasks);
        }

        public void DisplayElevators()
        {
            foreach (var e in _elevators)
            {
                e.DisplayStatus();
            }
        }

        private Passenger GeneratePassengerRequest()
        {
            int start = _random.Next(1, ElevatorConstants.MaxFloor + 1);
            int dest = _random.Next(1, ElevatorConstants.MaxFloor + 1);
            return new Passenger(start, dest);
        }

        private void AssignPassengerToElevator(Passenger passenger)
        {
            var chosen = _elevators
                .OrderBy(e => Math.Abs(e.CurrentFloor - passenger.StartFloor))
                .FirstOrDefault() as IRequestHandler;

            chosen?.AssignRequest(passenger);
        }
    }
}
