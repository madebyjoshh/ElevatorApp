using ElevatorApp.Business;

namespace ElevatorApp.Contracts
{
    /// <summary>
    /// Defines methods for handling elevator requests and processing elevator steps.
    /// </summary>
    public interface IRequestHandler
    {
        /// <summary>
        /// Assigns a passenger's elevator request to the handler.
        /// </summary>
        /// <param name="passenger">The passenger requesting elevator service.</param>
        void AssignRequest(Passenger passenger);

        /// <summary>
        /// Advances the elevator system by one step asynchronously.
        /// </summary>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task StepAsync();
    }
}
