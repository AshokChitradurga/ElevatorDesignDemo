using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevatorDemoSolution
{
    public class Elevator : BaseElevator
    {
        public string Name { get; set; }
        public Type Type { get; set; }

        public Elevator()
        {

        }

        public event EventHandler<FloorCrossingEventArgs> OnFloorChanged;
        public void OnFloorCrossingEvent()
        {
            var arg = new FloorCrossingEventArgs() { Elevator = this };
            OnEventChange(arg);
        }
        private void OnEventChange(FloorCrossingEventArgs e)
        {
            OnFloorChanged?.Invoke(this, e);
        }

        
    }
}
