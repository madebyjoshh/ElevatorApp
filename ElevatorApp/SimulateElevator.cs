using ElevatorApp.Business;
using ElevatorApp.Contracts;
using System.Collections.Concurrent;

namespace ElevatorApp
{
    /// <summary>
    /// Simulates the operation of a group of elevators, generating passenger requests and assigning them to elevators.
    /// </summary>
    public class SimulateElevator
    {
        private readonly IList<IElevator> _elevators;
        private readonly List<ConcurrentQueue<Passenger>> _passengerQueues;
        private readonly Random _random;
        private readonly int _maxFloor;

        public SimulateElevator(IList<IElevator> elevators, int queueCount, int maxFloor, Random random)
        {
            _elevators = elevators ?? throw new ArgumentNullException(nameof(elevators));
            _random = random ?? new Random();
            _maxFloor = maxFloor;

            _passengerQueues = Enumerable.Range(0, queueCount)
                .Select(_ => new ConcurrentQueue<Passenger>())
                .ToList();
        }

        public async Task RunStepAsync()
        {
            ScheduleRequests();

            foreach (var queue in _passengerQueues)
            {
                if (queue.TryDequeue(out Passenger passenger))
                {
                    AssignPassengerToElevator(passenger);
                }
            }

            var elevatorTasks = _elevators
                .Select(e => (e as IRequestHandler)?.StepAsync() ?? Task.CompletedTask);

            await Task.WhenAll(elevatorTasks);
        }
        public Passenger GeneratePassengerRequest()
        {
            int start = _random.Next(1, _maxFloor + 1);
            int dest;
            do
            {
                dest = _random.Next(1, _maxFloor + 1);
            } while (dest == start);

            return new Passenger(start, dest);
        }

        private void ScheduleRequests()
        {
            foreach (var queue in _passengerQueues)
            {
                if (_random.NextDouble() < 0.8) // 50% chance per queue
                {
                    queue.Enqueue(GeneratePassengerRequest());
                }
            }
        }

        private void AssignPassengerToElevator(Passenger passenger)
        {
            var chosen = _elevators
                .OrderBy(e => EvaluateElevator(e, passenger))
                .FirstOrDefault() as IRequestHandler;

            chosen?.AssignRequest(passenger);
        }

        private double EvaluateElevator(IElevator e, Passenger p)
        {
            var elevator = e as Elevator;
            int distance = Math.Abs(elevator.CurrentFloor - p.StartFloor);
            int queueLength = elevator.QueueLength;
            return distance + (queueLength * 2); 
        }
    }
}
