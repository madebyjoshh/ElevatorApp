using ElevatorApp.Constants;
using ElevatorApp.Contracts;
using ElevatorApp.Enums;
using System.Collections.Concurrent;

namespace ElevatorApp.Business
{
    /// <summary>
    /// Represents an elevator that can move between floors, handle passenger requests, and display its status.
    /// </summary>
    public class Elevator : IElevator, IMover, IRequestHandler
    {
        /// <inheritdoc/>
        public int Id { get; }

        /// <inheritdoc/>
        public int CurrentFloor { get; private set; }

        public int QueueLength => passengerQueue.Count;

        public Direction Direction { get; private set; } = Direction.None;
        private readonly ConcurrentQueue<Passenger> passengerQueue = new();
        private readonly object moveLock = new();

        /// <summary>
        /// Initializes a new instance of the <see cref="Elevator"/> class.
        /// </summary>
        /// <param name="id">The unique identifier for the elevator.</param>
        /// <param name="startFloor">The starting floor of the elevator.</param>
        public Elevator(int id, int startFloor)
        {
            Id = id;
            CurrentFloor = startFloor;
        }

        /// <inheritdoc/>
        public void DisplayStatus()
        {
            Console.WriteLine($"Elevator {Id} at Floor {CurrentFloor}");
        }

        /// <inheritdoc/>
        public void AssignRequest(Passenger passenger)
        {
            lock (moveLock)
            {
                passengerQueue.Enqueue(passenger);

                var direction = passenger.DestinationFloor > passenger.StartFloor
                    ? Direction.Up
                    : Direction.Down;
                Console.WriteLine($"\n ------- Assign to nearest elevator -------");
                Console.WriteLine(
                    $"Passenger requested {direction} from Floor {passenger.StartFloor} to {passenger.DestinationFloor}. " +
                    $"Assigned to Elevator {Id}");
            }
        }

        /// <inheritdoc/>
        public async Task StepAsync()
        {
            while (!passengerQueue.IsEmpty)
            {
                if (passengerQueue.TryDequeue(out Passenger? passenger))
                {
                    // Move to pickup
                    await MoveToFloorAsync(passenger.StartFloor);
                    Console.WriteLine($"Elevator {Id} picked up passenger at Floor {passenger.StartFloor}");

                    // Move to destination
                    await MoveToFloorAsync(passenger.DestinationFloor);
                    Console.WriteLine($"Elevator {Id} dropped off passenger at Floor {passenger.DestinationFloor}");

                    Direction = Direction.None; // Reset direction after drop-off
                }
            }
        }

        /// <inheritdoc/>
        public async Task MoveToFloorAsync(int targetFloor)
        {
            // Determine movement direction
            Direction = targetFloor > CurrentFloor
                ? Direction.Up
                : targetFloor < CurrentFloor
                    ? Direction.Down
                    : Direction.None;

            // Simulate movement floor by floor
            while (CurrentFloor != targetFloor)
            {
                lock (moveLock)
                {
                    CurrentFloor += targetFloor > CurrentFloor ? 1 : -1;
                    Console.WriteLine($"Elevator {Id} moving {Direction} to Floor {CurrentFloor}");
                }

                await Task.Delay(ElevatorConstants.DelayInMilliseconds); // simulate travel time
            }

            Console.WriteLine($"Elevator {Id} stopped at Floor {CurrentFloor}");
            await Task.Delay(ElevatorConstants.DelayInMilliseconds); // simulate boarding/disembarking
        }

    }
}
