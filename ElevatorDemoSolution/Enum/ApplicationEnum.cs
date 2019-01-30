using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevatorDemoSolution
{
    public enum ElevatorStatus
    {
        Ideal,
        MovingUp,
        MovingDown
    }

    public enum FloorButton
    {
        UP,
        DOWN
    }

    public enum ElevatorMovingDirection
    {
        UP,
        DOWN
    }

    public enum Type
    {
        Passanger = 1,
        Service = 2
    }
}
