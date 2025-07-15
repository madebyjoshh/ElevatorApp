namespace ElevatorApp.Constants
{
    /// <summary>
    /// Contains constant values used throughout the Elevator application.
    /// </summary>
    public static class ElevatorConstants
    {
        /// <summary>
        /// The highest floor number accessible by the elevator.
        /// </summary>
        public const int MaxFloor = 10;

        /// <summary>
        /// The total number of elevators in the system.
        /// </summary>
        public const int ElevatorCount = 4;

        /// <summary>
        /// The number of simulation steps to run.
        /// </summary>
        public const int SimulationSteps = 10;

        /// <summary>
        /// The delay in milliseconds to simulate the time taken for the elevator to move between floors. Default is 10 seconds.
        /// </summary>
        public const int DelayInMilliseconds = 1000;
    }
}
