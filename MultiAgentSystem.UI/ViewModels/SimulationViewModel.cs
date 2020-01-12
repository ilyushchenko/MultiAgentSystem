using System.Collections.Generic;
using System.Timers;
using System.Windows.Input;
using MultiAgentSystem.BLL.Models;
using MultiAgentSystem.BLL.Units.ShipModel;
using MultiAgentSystem.UI.Internal;

namespace MultiAgentSystem.UI.ViewModels
{
    internal class SimulationViewModel : BaseViewModel
    {
        private readonly Timer _autoCycleTimer;
        private readonly List<Ship> _ships;
        private int _cycle;
        private int _delay;
        private Map _map;
        private int _mapSize;

        public SimulationViewModel()
        {
            _autoCycleTimer = new Timer();
            _ships = new List<Ship>();
            Drawer = new DrawerViewModel();
            CreateMapCommand = new RelayCommand(CreateMapExecute, CreateMapCanExecute);
            SpawnShipCommand = new RelayCommand(SpawnShipExecute);
            StepCommand = new RelayCommand(StepExecute);
            Delay = 100;
            MapSize = 10;

            _autoCycleTimer.Elapsed += AutoCycleTimerOnElapsed;
            _autoCycleTimer.Interval = Delay;

            StartCommand = new RelayCommand(StartExecute);
            StopCommand = new RelayCommand(StopExecute);
        }

        public DrawerViewModel Drawer { get; }

        public ICommand CreateMapCommand { get; }
        public ICommand SpawnShipCommand { get; }
        public ICommand StepCommand { get; }
        public ICommand StartCommand { get; }
        public ICommand StopCommand { get; }


        public int MapSize
        {
            get => _mapSize;
            set
            {
                _mapSize = value;
                OnPropertyChanged();
            }
        }

        public int Cycle
        {
            get => _cycle;
            private set
            {
                _cycle = value;
                OnPropertyChanged();
            }
        }

        public int Delay
        {
            get => _delay;
            set
            {
                _delay = value;
                _autoCycleTimer.Interval = value;
                OnPropertyChanged();
            }
        }

        private void StopExecute(object obj)
        {
            _autoCycleTimer.Stop();
        }

        private void StartExecute(object obj)
        {
            _autoCycleTimer.Start();
        }

        private void StepExecute(object obj)
        {
            MakeStep();
        }

        private void SpawnShipExecute(object obj)
        {
            var ship = new Ship(_map);
            _ships.Add(ship);
            _map.Move(ship, new Point(0, 0));
            Drawer.DrawShip(ship);
        }

        private bool CreateMapCanExecute(object arg)
        {
            if (_map != null) return false;
            if (MapSize <= 0) return false;
            return true;
        }

        private void CreateMapExecute(object obj)
        {
            _map = new Map(MapSize, MapSize);
            Drawer.DrawMap(_map);
        }

        private void AutoCycleTimerOnElapsed(object sender, ElapsedEventArgs e)
        {
            _autoCycleTimer.Stop();
            MakeStep();
            _autoCycleTimer.Start();
        }

        private void MakeStep()
        {
            Cycle++;
            foreach (var ship in _ships) ship.DoActions();
        }
    }
}