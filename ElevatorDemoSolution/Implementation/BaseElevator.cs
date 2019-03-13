using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevatorDemoSolution
{
    public class BaseElevator
    {
        private int currentfloor = 0;
        private int min;
        private int max;
        private BuildingSettings _elevatorsettings = default(BuildingSettings);
        private SortedSet<int> floorToStopUp = new SortedSet<int>();
        private SortedSet<int> floorToStopDown = new SortedSet<int>();
        Object _lock = new object();
        IElevatorAction _elevatorAction;
        ElevatorActionInvoker _invoker;

        public ElevatorMovingDirection ElevatorMovingDirection { get; set; }
        public ElevatorStatus ElevatorStatus { get; set; }
        public bool HoldElevator { get; set; }
        public SortedSet<int> FloorToStopUp { get { return floorToStopUp; } }
        public SortedSet<int> FloorToStopDown { get { return floorToStopDown; } }

        public BaseElevator()
        {
            _elevatorsettings = ConfigurationManager.GetSection("ElevatorSettings") as BuildingSettings;
            min = _elevatorsettings.ElevatorConfig.MinFloor;
            max = _elevatorsettings.ElevatorConfig.MaxFloor;
            ElevatorStatus = ElevatorStatus.Ideal;
            _elevatorAction = DependencyResolver.Instance.GetDependency<IElevatorAction>();
            _invoker = new ElevatorActionInvoker();
        }

        public int CurrentFloor
        {
            get { return currentfloor; }
            set
            {
                if (currentfloor < min || currentfloor > max)
                    throw new ArgumentException(string.Format("CurrentFloor should be between {0} and {1}", min, max));
                currentfloor = value;
            }
        }

        public void AddStoppage(int floorNumber, Elevator actionelevator)
        {
            if (floorNumber < min || floorNumber > max)
                throw new ArgumentException(string.Format("Floor should be between {0} and {1}", min, max));

            if ((ElevatorStatus == ElevatorStatus.MovingUp) ||
                (ElevatorStatus == ElevatorStatus.Ideal && CurrentFloor < floorNumber))
            {
                floorToStopUp.Add(floorNumber);
                _invoker.ActionCtx = DependencyResolver.Instance.GetDependencyByName<IElevatorActionCtx>("up");
                _invoker.GetElevatorAction();
            }
            else
            {
                floorToStopDown.Add(floorNumber);
                _invoker.ActionCtx = DependencyResolver.Instance.GetDependencyByName<IElevatorActionCtx>("down");
                _invoker.GetElevatorAction();
            }

            _elevatorAction.Elevator = actionelevator;
            _elevatorAction.Move(ElevatorStatus);
        }

        public int GetNearestStoppage()
        {
            int? nearestStoppage;
            if (floorToStopUp.Count > 0)
                nearestStoppage = floorToStopUp.Min;
            else
            {
                nearestStoppage = floorToStopDown.Max;
            }
            return nearestStoppage.Value;
        }
    }
}
