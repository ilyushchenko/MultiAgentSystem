using System;
using System.Collections.Generic;
using MultiAgentSystem.BLL.Interfaces;
using MultiAgentSystem.BLL.Models;
using MultiAgentSystem.BLL.Units.ShipModel.States;

namespace MultiAgentSystem.BLL.Units.ShipModel
{
    public class Ship : IUnit
    {
        private readonly Map _map;
        public Point CurrentPosition { get; private set; }
        public Direction MoveDirection { get; private set; }
        
        private IShipState _currentState;
        public float Time { get; set; }
        public List<Point> Path { get; private set; }
        public Point? TargetPosition { get; set; }
        public bool IsAlive { get; set; }

        public event Action OnPathChanged;
        public event Action OnMoved;

        public Ship(Map map)
        {
            _map = map ?? throw new ArgumentNullException(nameof(map));
            _currentState = new PathFindingState(this, _map);
            IsAlive = true;
            Path = new List<Point>();
        }

        public void DoActions()
        {
            if (IsAlive) _currentState.Handle();
        }

        public void SetPosition(Point newPosition)
        {
            CurrentPosition = newPosition;
            OnMoved?.Invoke();
        }

        public void SetState(IShipState newState)
        {
            _currentState = newState;
        }

        public void SetPath(List<Point> path)
        {
            Path = path;
            OnPathChanged?.Invoke();
        }
    }
}