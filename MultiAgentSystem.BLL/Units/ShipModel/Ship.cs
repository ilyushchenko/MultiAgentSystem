using System;
using System.Collections.Generic;
using MultiAgentSystem.BLL.Interfaces;
using MultiAgentSystem.BLL.Models;
using MultiAgentSystem.BLL.Tools;
using MultiAgentSystem.BLL.Units.ShipModel.States;

namespace MultiAgentSystem.BLL.Units.ShipModel
{
    public class Ship : IUnit
    {
        private readonly Map _map;

        private IShipState _currentState;

        public Ship(float draft, float maxSpeed, Map map)
        {
            if (draft <= 0f) throw new ArgumentException("Draft must be positive value", nameof(draft));
            Draft = draft;

            if (maxSpeed <= 0f) throw new ArgumentException("MaxSpeed must be positive value", nameof(maxSpeed));
            MaxSpeed = maxSpeed;
            Speed = MaxSpeed;
            
            _map = map ?? throw new ArgumentNullException(nameof(map));

            _currentState = new WaitState();
            IsAlive = true;
            Path = new Queue<Point>();
        }

        public float Speed { get; set; }
        public float MaxSpeed { get; set; }

        public float Draft { get; }
        public Queue<Point> Path { get; private set; }
        public Point? TargetPosition { get; private set; }
        public bool IsAlive { get; set; }

        public event Action OnPathChanged;
        public event Action OnMoved;
        public event Action OnDirectionChanged;
        public Point CurrentPosition { get; private set; }
        public Direction MoveDirection { get; private set; }

        public void SetPosition(Point newPosition)
        {
            CurrentPosition = newPosition;
            OnMoved?.Invoke();
        }

        public void SetDirection(Direction newDirection)
        {
            MoveDirection = newDirection;
            OnDirectionChanged?.Invoke();
        }

        public void DoActions()
        {
            if (IsAlive) _currentState.Handle();
        }

        public void SetState(IShipState newState)
        {
            _currentState = newState;
        }

        public void SetTarget(Point target)
        {
            TargetPosition = target;
            
            var arr = new int[_map.Width, _map.Height];
            var path = AStarPathFinding.FindPath(arr, CurrentPosition, target);

            if (path == null) return;

            var pathQueue = new Queue<Point>(path);

            // Remove current position from path queue
            if (pathQueue.Peek() == CurrentPosition) pathQueue.Dequeue();

            SetPath(pathQueue);

            SetState(new TransferState(this, _map, Cell.Size));
        }

        private void SetPath(Queue<Point> path)
        {
            Path = path;
            OnPathChanged?.Invoke();
        }
    }
}