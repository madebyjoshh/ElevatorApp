using ElevatorApp.Constants;

namespace ElevatorApp.Business
{
    /// <summary>
    /// Represents a passenger requesting elevator service.
    /// </summary>
    public class Passenger
    {
        /// <summary>
        /// Gets the floor where the passenger starts.
        /// </summary>
        public int StartFloor { get; }

        /// <summary>
        /// Gets the floor where the passenger wants to go.
        /// </summary>
        public int DestinationFloor { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Passenger"/> class.
        /// </summary>
        /// <param name="start">The starting floor.</param>
        /// <param name="destination">The destination floor.</param>
        public Passenger(int startFloor, int destinationFloor)
        {
            if (startFloor < 1 || destinationFloor < 1 ||
                        startFloor > ElevatorConstants.MaxFloor || destinationFloor > ElevatorConstants.MaxFloor)
            {
                throw new ArgumentOutOfRangeException("Floor must be within valid range.");
            }

            if (startFloor == destinationFloor)
            {
                throw new ArgumentException("Start and destination floors must be different.");
            }

            StartFloor = startFloor;
            DestinationFloor = destinationFloor;
        }
    }
}
