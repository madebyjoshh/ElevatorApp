namespace ElevatorApp.Contracts
{
    /// <summary>
    /// Represents a mover capable of moving to a specified floor asynchronously.
    /// </summary>
    public interface IMover
    {
        /// <summary>
        /// Moves the mover to the specified destination floor asynchronously.
        /// </summary>
        /// <param name="destinationFloor">The floor number to move to.</param>
        /// <returns>A task representing the asynchronous move operation.</returns>
        Task MoveToFloorAsync(int destinationFloor);
    }
}
