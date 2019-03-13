using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevatorDemoSolution
{
    public class ElevatorController : IElevatorController
    {
        private Elevator[] elevators = null;
        private BuildingSettings _elevatorsettings = default(BuildingSettings);
        private int numElevators;
        private int numFloors;
        private int min;
        private int max;

        public ElevatorController()
        {
            _elevatorsettings = ConfigurationManager.GetSection("ElevatorSettings") as BuildingSettings;
            numElevators = _elevatorsettings.ElevatorConfig.MaxElevators;
            numFloors = _elevatorsettings.ElevatorConfig.MaxFloor;
            min = _elevatorsettings.ElevatorConfig.MinFloor;
            max = _elevatorsettings.ElevatorConfig.MaxFloor;
            InitializeElevators();
        }

        private void InitializeElevators()
        {
            if (numElevators < 0) throw new Exception("Elevator should be greater than one");
            elevators = new Elevator[numElevators];

            for (int elcount = 0; elcount < numElevators; elcount++)
            {
                Console.Write("Enter floor for Elevator{0}: ", elcount + 1);
                var floor = Console.ReadLine().Trim();
                var passenger = DependencyResolver.Instance.GetDependency<IElevatorBuilder>();
                ElevatorManager _manger = new ElevatorManager(passenger); // responsible to create elevator.
                _manger.ConstructElevator("ElevatorId" + (elcount + 1));
                var elevator = _manger.GetElevator();
                elevator.CurrentFloor = int.Parse(floor);
                elevator.OnFloorChanged += HandleFloorCrossingEvent;
                elevators[elcount] = elevator;
            }
        }

        public void Run()
        {
            var stoppageFloor = _elevatorsettings.ElevatorConfig.StoppagePoints;

            //Task t = Task.Run(() =>
            //{
            for (int elcount = 0; elcount < numElevators; elcount++)
                elevators[elcount].AddStoppage(stoppageFloor[elcount], elevators[elcount]);
            //});
            Console.WriteLine("Elevators stared operating. please give below information to check nearest floor");
            Console.Write("Enter the floor to calculate closer elevator for you: ");
            var cfloor = Console.ReadLine().Trim();
            Console.Write("Enter the direction(1/0): ");
            var direction = Console.ReadLine().Trim();
            //t.Wait();
            var nElevator = FindClosetElevator(int.Parse(direction), int.Parse(cfloor));
            Console.WriteLine(string.Format("The closet elevator is {0} and it is at {1} floor",
                nElevator.Name, nElevator.CurrentFloor));
        }

        public void HoldElevator()
        {
            throw new NotImplementedException();
        }

        public Elevator FindClosetElevator(int floorButton, int floor)
        {
            Console.WriteLine($"{floorButton} key is pressed on floor {floor}");
            if (floor < min || floor > max)
                throw new ArgumentException($"{floor} should be between {min} and {max}");

            var elevatorsDistancefromCurrentFloor = new SortedList<int, Elevator>();

            foreach (var elevator in elevators)
            {
                int distance = 0;
                if (elevator.CurrentFloor > floor && elevator.ElevatorStatus == ElevatorStatus.MovingDown)
                {
                    distance = CalculateRoute(floor, elevator.CurrentFloor);
                }
                else if (elevator.CurrentFloor < floor && elevator.ElevatorStatus == ElevatorStatus.MovingUp)
                {
                    distance = CalculateRoute(floor, elevator.CurrentFloor);
                }
                else if (elevator.CurrentFloor != floor && elevator.ElevatorStatus == ElevatorStatus.Ideal)
                {
                    distance = CalculateRoute(floor, elevator.CurrentFloor);
                }

                if (distance != 0 && !elevatorsDistancefromCurrentFloor.ContainsKey(distance))
                    elevatorsDistancefromCurrentFloor.Add(distance, elevator);
            }
            var nearestElevator = elevatorsDistancefromCurrentFloor.First().Value;
            Console.WriteLine($"Elevator {nearestElevator.Name} is assigned to stop at {floor}");
            nearestElevator.AddStoppage(floor, nearestElevator);
            return nearestElevator;
        }

        public int CalculateRoute(int cf, int ecf)
        {
            var distance = Math.Abs(cf - ecf);
            return distance;
        }

        private void HandleFloorCrossingEvent(object sender, FloorCrossingEventArgs arg)
        {
            var elevator = arg.Elevator;
            int holdelevator = -1;

            if (elevator.ElevatorStatus == ElevatorStatus.MovingUp)
            {
                holdelevator = elevators.ToList().FindIndex(el => el.HoldElevator &&
                (elevator.CurrentFloor + 1) == el.GetNearestStoppage() && el.ElevatorStatus == ElevatorStatus.MovingUp);
                if (holdelevator > -1)
                {
                    elevator.AddStoppage(elevator.CurrentFloor + 1, elevator);
                }
            }
            else
            {
                holdelevator = elevators.ToList().FindIndex(el => el.HoldElevator && ((elevator.CurrentFloor - 1) == el.GetNearestStoppage()) && el.ElevatorStatus == ElevatorStatus.MovingDown);
                if (holdelevator > -1)
                {
                    elevator.AddStoppage(elevator.CurrentFloor - 1, elevator);
                }
            }
        }
    }
}
