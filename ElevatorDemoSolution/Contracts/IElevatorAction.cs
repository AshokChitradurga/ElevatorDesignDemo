using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevatorDemoSolution
{
    public interface IElevatorAction
    {
        Elevator Elevator { get; set; }
        void Move(ElevatorStatus status);
    }
}
