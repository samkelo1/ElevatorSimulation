using ElevatorSimulation.Directions;
using ElevatorSimulation.Elevator;

namespace ElevatorApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // Start Simulation
            // Initialize elevator and get total floors from user input
            Console.WriteLine("Enter the total number of floors of the building: ");
            int totalFloors = Convert.ToInt32(Console.ReadLine());
            Elevator elevator = new Elevator(totalFloors);
            
            // Prompt user for elevator starting direction
            Console.WriteLine("Enter 1 for Up (from Ground level) or 0 to exit:");
            int groundChoice = Convert.ToInt32(Console.ReadLine());

            if (groundChoice == 1)
            {
                // Start the elevator at ground level and handle inside and outside floor requests
                elevator.StartAtGround();
                bool exitInsideLoop = false;
                bool exitOutsideLoop = false;
                
                // Loop to handle inside floor requests
                while (!exitInsideLoop)
                {
                    exitInsideLoop = RequestInsideFloors(totalFloors, exitInsideLoop, elevator);
                }

                // Loop to handle outside floor requests
                while (!exitOutsideLoop)
                {
                    Console.WriteLine(
                        $"Current elevator floor is {elevator.GetCurrentFloor()}. Enter the floor number you are on (0-{totalFloors}) to request the elevator direction from outside, or -1 to exit:");
                    int outsideFloor = Convert.ToInt32(Console.ReadLine());

                    if (outsideFloor == -1)
                    {
                        Console.WriteLine("Exiting outside elevator program.");
                        exitOutsideLoop = true;
                        break;
                    }
                    else if (outsideFloor < 0 || outsideFloor > totalFloors)
                    {
                        Console.WriteLine(
                            $"Invalid floor number. Please enter a floor number between 0 and {totalFloors}");
                        continue;
                    }

                    if (!exitOutsideLoop)
                    {
                        if (outsideFloor == 0)
                        {
                            Console.WriteLine("Enter direction (1 for Up:)");
                        }
                        else if(outsideFloor == totalFloors)
                        {
                            Console.WriteLine("Enter direction (2 for Down:)");
                        }
                        else
                        {
                            Console.WriteLine("Enter direction (1 for Up, 2 for Down):");
                        }
                        
                        int dirChoice = Convert.ToInt32(Console.ReadLine());
                        Direction requestDirection = dirChoice == 1 ? Direction.Up : Direction.Down;
                        elevator.RequestOutsideFloor(outsideFloor, requestDirection);
                        if (!elevator.IsMoving())
                        {
                            elevator.ProcessNextRequest();
                        }

                        exitInsideLoop = false;
                        while (!exitInsideLoop)
                        {
                            exitInsideLoop = RequestInsideFloors(totalFloors, exitInsideLoop, elevator);
                        }
                    }
                }
            }
            else if (groundChoice == 0)
            {
                Console.WriteLine("Exiting elevator program");
            }
            else
            {
                Console.WriteLine("Invalid choice. Exiting elevator program");
            }
        }

        // Method to handle inside floor requests
        private static bool RequestInsideFloors(int totalFloors, bool exitInsideLoop, Elevator elevator)
        {
            Console.WriteLine(
                $"Enter the floor number you want to go to 0 to {totalFloors} from inside the elevator, or -1 to exit:");

            int[] insideFloors = Console.ReadLine().Split(" ").Select(int.Parse).ToArray();
            foreach (int insideFloor in insideFloors)
            {
                if (insideFloor == -1)
                {
                    Console.WriteLine("Exiting inside elevator program.");
                    exitInsideLoop = true;
                    break;
                }
                else if (insideFloor < 0 || insideFloor > totalFloors)
                {
                    Console.WriteLine(
                        $"Invalid floor number. Please enter a floor number between 0 and {totalFloors}");
                }
            }

            if (!exitInsideLoop)
            {
                elevator.RequestInsideFloor(insideFloors);
                if (!elevator.IsMoving())
                {
                    elevator.ProcessNextRequest();
                }
            }

            return exitInsideLoop;
        }
    }
}
