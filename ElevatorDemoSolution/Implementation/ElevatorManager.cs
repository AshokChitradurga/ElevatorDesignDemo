using System;

namespace ElevatorDemoSolution
{
    public class ElevatorManager 
    {
        private IElevatorBuilder _elevatorBuilder = default(IElevatorBuilder);
        public ElevatorManager(IElevatorBuilder iElevator)
        {
            _elevatorBuilder = iElevator;
        }
        public void ConstructElevator(string name)
        {
            _elevatorBuilder.SetName(name);
            _elevatorBuilder.SetType();
        }

        public Elevator GetElevator()
        {
            return _elevatorBuilder.GetElevator();
        }
    }
}