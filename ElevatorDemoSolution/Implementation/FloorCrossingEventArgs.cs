using System;

namespace ElevatorDemoSolution
{
    public class FloorCrossingEventArgs : EventArgs
    {
        public Elevator Elevator { get; set; }
    }
}