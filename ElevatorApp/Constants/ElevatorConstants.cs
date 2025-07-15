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
        /// The lowest floor number accessible by the elevator.
        /// </summary>
        public const int MinFloor = 0;

        /// <summary>
        /// The default floor where the elevator starts or resets.
        /// </summary>
        public const int DefaUltFloor = 0;

        /// <summary>
        /// The total number of elevators in the system.
        /// </summary>
        public const int ElevatorCount = 4;

        /// <summary>
        /// The number of simulation steps to run.
        /// </summary>
        public const int SimulationSteps = 10;

        public const int DelayInMilliseconds = 1000;
    }
}
