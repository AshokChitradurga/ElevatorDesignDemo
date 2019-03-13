using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevatorDemoSolution
{
    /// <summary>
    /// This class implemeted to handle elevator directions
    /// </summary>
    public class ElevatorAction : IElevatorAction
    {
        private Elevator _elevator = default(Elevator);
        private int? stoppageFloor;
        public Elevator Elevator
        {
            get
            {
                return _elevator;
            }
            set
            {
                _elevator = value;
            }
        }
        public void Move(ElevatorStatus direction)
        {
            if (direction == ElevatorStatus.Ideal) Idel();
            if (direction == ElevatorStatus.MovingUp) MoveUp();
            if (direction == ElevatorStatus.MovingDown) MoveDown();
        }
        private void Idel()
        {
            int? nextStoppageUp = _elevator.FloorToStopUp.Min;
            int? nextStoppageDown = _elevator.FloorToStopDown.Max;
            if (nextStoppageUp.HasValue && nextStoppageUp.Value > _elevator.CurrentFloor)
            {
                //  Console.WriteLine($"{_elevator.Name} is going to move up.");
                ChangeStatus(ElevatorMovingDirection.UP);
            }
            else if (nextStoppageDown.HasValue && nextStoppageDown.Value < _elevator.CurrentFloor)
            {
                //  Console.WriteLine($"{_elevator.Name} is going to move down.");
                ChangeStatus(ElevatorMovingDirection.DOWN);
            }
        }
        private void MoveDown()
        {
            stoppageFloor = _elevator.FloorToStopDown.Max;

            while (stoppageFloor.HasValue && !_elevator.HoldElevator)
            {
                Task.Delay(3000).Wait();
                Console.WriteLine($"{_elevator.Name}: is moving down.");
                for (int i = _elevator.CurrentFloor; i > stoppageFloor + 1; i--)
                {
                    Task.Delay(1000).Wait();
                    this._elevator.CurrentFloor = i;
                    Console.WriteLine($"{_elevator.Name}: floor {i} is crossed");
                    this._elevator.OnFloorCrossingEvent();
                    Task.Delay(1000).Wait();
                    CheckStoppageUpdate_Down();
                }
                this._elevator.CurrentFloor = stoppageFloor.Value;
                Task.Delay(1000).Wait();
                Console.WriteLine($"{_elevator.Name} stopped at {stoppageFloor} floor");
                _elevator.FloorToStopUp.Remove(stoppageFloor.Value);
                Task.Delay(1000).Wait();
                if (_elevator.FloorToStopUp.Count > 0)
                    stoppageFloor = _elevator.FloorToStopUp.Max;
                else stoppageFloor = null;
            }
            if (_elevator.HoldElevator)
            {
                Console.WriteLine($"{_elevator.Name}: on hold");
            }
            else ChangeStatus(_elevator.ElevatorStatus);
        }
        private void MoveUp()
        {
            stoppageFloor = _elevator.FloorToStopUp.Min;
            if (_elevator.HoldElevator)
            {
                Console.WriteLine($"{_elevator.Name}: on hold");
            }
            while (stoppageFloor.HasValue && !_elevator.HoldElevator)
            {
                Task.Delay(3000).Wait();
                Console.WriteLine($"{_elevator.Name}: is moving up.");
                for (int i = _elevator.CurrentFloor; i <= stoppageFloor - 1; i++)
                {
                    Task.Delay(1000).Wait();
                    this._elevator.CurrentFloor = i;
                    Console.WriteLine($"{_elevator.Name}: floor {i} is crossed");
                    _elevator.OnFloorCrossingEvent();
                    Task.Delay(1000).Wait();
                    CheckStoppageUpdate_Up();
                }
                this._elevator.CurrentFloor = stoppageFloor.Value;
                Task.Delay(1000).Wait();
                Console.WriteLine($"{_elevator.Name} stopped at {stoppageFloor} floor");
                _elevator.FloorToStopUp.Remove(stoppageFloor.Value);
                Task.Delay(1000).Wait();
                if (_elevator.FloorToStopUp.Count > 0)
                    stoppageFloor = _elevator.FloorToStopUp.Min;
                else stoppageFloor = null;
            }
            if (_elevator.HoldElevator)
            {
                Console.WriteLine($"{_elevator.Name}: on hold");
            }
            else ChangeStatus(_elevator.ElevatorStatus);
        }
        public void Stop()
        {
        }
        private void CheckStoppageUpdate_Down()
        {
            var updatedNearestStoppage = _elevator.FloorToStopDown.Max;
            if (updatedNearestStoppage <= this._elevator.CurrentFloor && updatedNearestStoppage > stoppageFloor)
            {
                stoppageFloor = updatedNearestStoppage;
            }
        }
        private void CheckStoppageUpdate_Up()
        {
            var updatedNearestStoppage = _elevator.FloorToStopUp.Min;
            if (updatedNearestStoppage > this._elevator.CurrentFloor && updatedNearestStoppage < stoppageFloor)
            {
                stoppageFloor = updatedNearestStoppage;
            }
        }
        private void ChangeStatus(ElevatorMovingDirection direction)
        {
            _elevator.ElevatorStatus = direction == ElevatorMovingDirection.UP ? ElevatorStatus.MovingUp : ElevatorStatus.MovingDown;
            if (_elevator.ElevatorStatus == ElevatorStatus.MovingUp)
                MoveUp();
            else MoveDown();
        }
        private void ChangeStatus(ElevatorStatus staus)
        {
            _elevator.ElevatorStatus = staus;
        }
    }

    public interface IElevatorActionCtx
    {
        void Move();
    }

    public class ElevatorActionInvoker
    {
        public IElevatorActionCtx ActionCtx { get; set; }

        public void GetElevatorAction()
        {
            ActionCtx.Move();
        }
    }

    public class ElevatorMoveUp : IElevatorActionCtx
    {
        public ElevatorMoveUp()
        {

        }
        public void Move()
        {

        }
    }

    public class ElevatorMoveDown : IElevatorActionCtx
    {
        public ElevatorMoveDown()
        {

        }

        public void Move()
        {

        }
    }
}
