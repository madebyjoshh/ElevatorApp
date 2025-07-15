namespace ElevatorApp.Contracts
{
    /// <summary>
    /// Represents an elevator with basic properties and operations.
    /// </summary>
    public interface IElevator
    {
        /// <summary>
        /// Gets the unique identifier of the elevator.
        /// </summary>
        int Id { get; }

        /// <summary>
        /// Gets the current floor where the elevator is located.
        /// </summary>
        int CurrentFloor { get; }

        /// <summary>
        /// Displays the current status of the elevator.
        /// </summary>
        void DisplayStatus();
    }
}
