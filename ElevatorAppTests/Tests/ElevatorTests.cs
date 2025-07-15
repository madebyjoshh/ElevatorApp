using ElevatorApp.Business;
using ElevatorApp.Enums;
using FluentAssertions;
using Xunit;

namespace ElevatorAppTests.Tests
{
    public class ElevatorTests
    {
        [Theory]
        [InlineData(3, 5, 8)]  // Elevator at 3, passenger from 5 to 8
        [InlineData(1, 2, 6)]  // Elevator at 1, passenger from 2 to 6
        [InlineData(7, 4, 2)]  // Elevator at 7, passenger from 4 to 2
        public async Task Elevator_MovesToCorrectDestination(int startFloor, int passengerStart, int passengerDestination)
        {
            // Arrange
            var elevator = new Elevator(id: 1, startFloor: startFloor);
            var passenger = new Passenger(passengerStart, passengerDestination);

            // Act
            elevator.AssignRequest(passenger);
            await elevator.StepAsync();

            // Assert
            elevator.CurrentFloor.Should().Be(passengerDestination);
            elevator.Direction.Should().Be(Direction.None);
        }

        [Theory]
        [InlineData(0, 5)]
        [InlineData(1, 0)]
        [InlineData(12, 1)] // Assuming MaxFloor = 10
        public void Passenger_InvalidFloor_ThrowsOutOfRange(int start, int dest)
        {
            Action act = () => new Passenger(start, dest);
            act.Should().Throw<ArgumentOutOfRangeException>();
        }
    }
}
