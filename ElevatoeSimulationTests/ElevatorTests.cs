using ElevatorSimulation.Elevator;

namespace ElevatoeSimulationTests
{
    [TestFixture]
    public class ElevatorTests
    {
        private Elevator _elevator;

        [SetUp]
        public void Setup()
        {
            _elevator = new Elevator(10); // Assuming 10 floors in the building
        }

        [Test]
        public void IsMoving_InitiallyNotMoving_ReturnsFalse()
        {
            Assert.That(_elevator.IsMoving(), Is.False);
        }

        [Test]
        public void StartAtGround_ElevatorStartsAtGround_CurrentFloorIsZero()
        {
            _elevator.StartAtGround();
            Assert.That(_elevator.GetCurrentFloor(), Is.EqualTo(0));
        }

        [Test]
        public void ProcessNextRequest_NoRequests_ElevatorNotMoving()
        {
            _elevator.ProcessNextRequest();
            Assert.That(_elevator.IsMoving(), Is.False);
        }
        
    }

}