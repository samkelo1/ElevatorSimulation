# ElevatorSimulation
This program simulates the operation of an elevator in a building with multiple floors. It includes classes for the Elevator itself and a Program class to run the simulation.

## Elevator Class

The `Elevator` class represents the functionality and state of the elevator. It includes methods to start the elevator, request inside and outside floors, process floor requests, and move the elevator accordingly.

### Usage

1. Instantiate an `Elevator` object with the total number of floors in the building.
2. Use the provided methods to interact with the elevator:
   - `StartAtGround`: Start the elevator at the ground floor.
   - `RequestInsideFloor`: Request specific floors from inside the elevator. Multiple floors can be selected at once.
   - `RequestOutsideFloor`: Request floors and directions from outside the elevator.
   - `ProcessNextRequest`: Process and move to the next requested floor.

## Program Class

The `Program` class contains the `Main` method to run the elevator simulation program. It handles user inputs, initiates elevator operations, and controls the flow of the simulation.

### Usage

1. Run the program and follow the on-screen instructions.
2. Enter the total number of floors in the building when prompted.
3. Choose to start the elevator at ground level and provide inside and outside floor requests as needed. Use -1 as the exit key from inside the elevator.

## Unit Tests

Unit tests for this project are located in the `Tests` folder. These tests ensure the functionality and correctness of the Elevator class and its methods. To run the unit tests:

1. Navigate to the `Tests` folder in your terminal or command prompt.
2. Run the unit tests using a testing framework such as NUnit or MSTest.
3. Ensure that all tests pass successfully to validate the functionality of the Elevator class.

## How to Run

1. Ensure you have the necessary .NET SDK installed on your machine.
2. Clone or download the repository containing the ElevatorApp project.
3. Open a terminal or command prompt and navigate to the project directory.
4. Compile the project using `dotnet build`.
5. Run the program using `dotnet run`.

## Dependencies

- .NET Core SDK

## Author
Samkelo Siyabonga Ngubo
