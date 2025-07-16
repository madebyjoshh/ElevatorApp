using ElevatorApp;
using ElevatorApp.Business;
using ElevatorApp.Contracts;
using ElevatorApp.Enums;
using FluentAssertions;
using Xunit;

namespace ElevatorAppTests.Tests
{
    public class ElevatorTests
    {
        [Fact]
        public void Constructor_WithNullElevators_ShouldThrowArgumentNullException()
        {
            // Arrange and Act
            var random = new Random();
            Action act = () => new SimulateElevator(null, 1, 10, random);

            // Assert
            act.Should().Throw<ArgumentNullException>()
               .WithParameterName("elevators");
        }

        [Fact]
        public void GeneratePassengerRequest_ShouldHaveDifferentStartAndDestinationFloors()
        {
            // Arrange
            var simulator = new SimulateElevator(new List<IElevator>(), 3, 9, new Random());

            // Act
            var passenger = simulator.GeneratePassengerRequest();

            // Assert
            passenger.StartFloor.Should().NotBe(passenger.DestinationFloor);
        }

        [Theory]
        [InlineData(3, 5, 8)]  // Elevator at 3, passenger from 5 to 8
        [InlineData(1, 2, 6)]  // Elevator at 1, passenger from 2 to 6
        [InlineData(7, 4, 2)]  // Elevator at 7, passenger from 4 to 2
        public async Task Elevator_ShouldMoveToCorrectDestination(int startFloor, int passengerStart, int passengerDestination)
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
        public void Passenger_InvalidFloor_ShouldThrowOutOfRange(int start, int dest)
        {
            Action act = () => new Passenger(start, dest);
            act.Should().Throw<ArgumentOutOfRangeException>();
        }
    }
}
