using ElevatorSimulation.Directions;

namespace ElevatorSimulation.Elevator
{
    public class Elevator
    {
        #region Ctor and members

        // Private fields to store elevator state
        private int _currentFloor; // Current floor where the elevator is
        private Direction _direction; // Current direction of the elevator
        private bool _isMoving; // Flag to indicate if the elevator is moving
        private HashSet<int> _insideRequests; // Set to store inside floor requests
        private Dictionary<int, Direction> _outsideRequests; // Dictionary to store outside floor requests and their directions
        private int _totalFloors; // Total number of floors in the building

        // Constructor to initialize elevator state
        public Elevator(int totalFloors)
        {
            _totalFloors = totalFloors;
            _currentFloor = 0; // Elevator starts at ground floor
            _direction = Direction.None; // Initial direction is set to None
            _isMoving = false; // Elevator is not moving initially
            _insideRequests = new HashSet<int>(); // Initialize inside floor requests set
            _outsideRequests = new Dictionary<int, Direction>(); // Initialize outside floor requests dictionary
        }

        #endregion

        #region Methods

        // Method to check if the elevator is currently moving
        public bool IsMoving()
        {
            return _isMoving;
        }

        // Method to get the current floor where the elevator is
        public int GetCurrentFloor()
        {
            return _currentFloor;
        }

        // Method to start the elevator at the ground floor
        public void StartAtGround()
        {
            _currentFloor = 0;
            _direction = Direction.Up; // Start moving up from ground floor
        }

        // Method to add inside floor requests
        public void RequestInsideFloor(int[] floors)
        {
            foreach (int floor in floors)
            {
                _insideRequests.Add(floor);
            }
        }

        // Method to add outside floor requests
        public void RequestOutsideFloor(int floor, Direction requestDirection)
        {
            if (!_outsideRequests.ContainsKey(floor))
            {
                _outsideRequests[floor] = requestDirection;
            }
            else
            {
                Console.WriteLine($"Elevator is already requested from outside for floor {floor} for direction {requestDirection}");
            }
        }

        // Method to process the next floor request and move the elevator
        public void ProcessNextRequest()
        {
            // If there are no more requests, stop moving
            if (_outsideRequests.Count == 0 && _insideRequests.Count == 0)
            {
                _isMoving = false;
                _direction = Direction.None;
                return;
            }

            // Determine the next floor to move to based on requests and direction
            int nextFloor = GetNextFloor();
            _direction = nextFloor > _currentFloor ? Direction.Up : Direction.Down; // Update direction

            Console.WriteLine($"Elevator moving from floor {_currentFloor} to floor {nextFloor}");
            _isMoving = true;

            // Move the elevator to the next floor
            while (_currentFloor != nextFloor)
            {
                if (_direction == Direction.Up)
                {
                    _currentFloor++;
                }
                else if (_direction == Direction.Down)
                {
                    _currentFloor--;
                }

                Console.WriteLine($"Elevator is now at floor {_currentFloor}");
            }
            Console.WriteLine($"Elevator arrived at floor {_currentFloor}");

            // Process the next request recursively
            ProcessNextRequest();
        }

        // Method to determine the next floor based on elevator requests and direction
        private int GetNextFloor()
        {
            int nextFloor = _currentFloor;

            // Check if there's an outside request for the current floor in the current direction
            if (_outsideRequests.ContainsKey(_currentFloor) && _outsideRequests[_currentFloor] == _direction)
            {
                nextFloor = _currentFloor;
                _outsideRequests.Remove(_currentFloor); // Remove processed request
            }
            // If there are no inside requests and there are outside requests, prioritize outside requests
            else if (_insideRequests.Count == 0 && _outsideRequests.Count > 0)
            {
                // Sort outside requests based on direction
                List<int> sortedOutsideRequests = _outsideRequests.Keys.OrderBy(x => x).ToList();
                foreach (int floor in sortedOutsideRequests)
                {
                    if (_direction != Direction.None && _direction == Direction.Up && floor > _currentFloor)
                    {
                        nextFloor = floor;
                        _outsideRequests.Remove(floor); // Remove processed request
                        break;
                    }
                    else if (_direction != Direction.None && _direction == Direction.Down && floor < _currentFloor)
                    {
                        nextFloor = floor;
                        _outsideRequests.Remove(floor); // Remove processed request
                        break;
                    }
                    else
                    {
                        nextFloor = floor;
                        _direction = floor > _currentFloor ? Direction.Up : Direction.Down; // Update direction
                        _outsideRequests.Remove(floor); // Remove processed request
                        break;
                    }
                }
            }
            // Handle inside requests or remaining outside requests
            else
            {
                List<int> sortedInsideRequests = _direction == Direction.Up
                    ? _insideRequests.OrderBy(x => x).ToList()
                    : _insideRequests.OrderByDescending(x => x).ToList();
                foreach (int floor in sortedInsideRequests)
                {
                    if (_direction != Direction.None && floor > _currentFloor && _direction == Direction.Up)
                    {
                        nextFloor = floor;
                        _insideRequests.Remove(floor); // Remove processed request
                        break;
                    }
                    else if (_direction != Direction.None && floor < _currentFloor && _direction == Direction.Down)
                    {
                        nextFloor = floor;
                        _insideRequests.Remove(floor); // Remove processed request
                        break;
                    }
                    else
                    {
                        nextFloor = floor;
                        _direction = floor > _currentFloor ? Direction.Up : Direction.Down; // Update direction
                        _insideRequests.Remove(floor); // Remove processed request
                        break;
                    }
                }
            }

            return nextFloor;
        }

        #endregion
    }
}