using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevatorDemoSolution
{
    public class PassangerElevator : IElevatorBuilder
    {
        private Elevator _elevator = default(Elevator);

        public PassangerElevator()
        {
            _elevator = new Elevator();
        }
        public void SetName(string name)
        {
            _elevator.Name = name;
        }
        public void SetType()
        {
            _elevator.Type = Type.Passanger;
        }
        public Elevator GetElevator()
        {
            return _elevator;
        }
    }
}
