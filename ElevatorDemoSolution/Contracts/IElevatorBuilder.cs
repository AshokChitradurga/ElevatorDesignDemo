using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevatorDemoSolution
{
    public interface IElevatorBuilder
    {
        void SetName(string name);
        void SetType();
        Elevator GetElevator();
    }
}
