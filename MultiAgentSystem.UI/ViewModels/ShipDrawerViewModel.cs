using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using MultiAgentSystem.BLL.Units.ShipModel;

namespace MultiAgentSystem.UI.ViewModels
{
    public class ShipDrawerViewModel : BaseViewModel
    {
        private readonly List<DrawnObjectViewModel> _pathSprites;
        private readonly Ship _ship;
        private readonly DrawnObjectViewModel _shipSprite;

        public ShipDrawerViewModel(Ship ship)
        {
            _ship = ship;
            Sprites = new ObservableCollection<DrawnObjectViewModel>();

            var drawing = new DrawingVisual();
            using (var context = drawing.RenderOpen())
            {
                context.DrawRectangle(Brushes.Blue, null,
                    new Rect(new Point(0, 0), new Point(Constants.CellSize, Constants.CellSize)));
            }

            var source = new RenderTargetBitmap(Constants.CellSize, Constants.CellSize, 96, 96, PixelFormats.Pbgra32);
            source.Render(drawing);

            _shipSprite = new DrawnObjectViewModel(source);
            _pathSprites = new List<DrawnObjectViewModel>();

            Sprites.Add(_shipSprite);
            _ship.OnMoved += ShipMovedHandler;
            _ship.OnPathChanged += OnPathChangedHandler;
        }

        public ObservableCollection<DrawnObjectViewModel> Sprites { get; set; }

        private void OnPathChangedHandler()
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                foreach (var pathSprite in _pathSprites) OnSpriteRemoved?.Invoke(this, pathSprite);
                _pathSprites.Clear();

                var drawing = new DrawingVisual();
                using (var context = drawing.RenderOpen())
                {
                    context.DrawEllipse(Brushes.YellowGreen, null,
                        new Point(25, 25), 5, 5);
                }

                var source = new RenderTargetBitmap(Constants.CellSize, Constants.CellSize, 96, 96, PixelFormats.Pbgra32);
                source.Render(drawing);

                foreach (var point in _ship.Path)
                {
                    var pathSprite = new DrawnObjectViewModel(source)
                    {
                        X = point.X * Constants.CellSize,
                        Y = point.Y * Constants.CellSize
                    };
                    _pathSprites.Add(pathSprite);
                    OnSpriteAdded?.Invoke(this, pathSprite);
                }
            });
        }

        private void ShipMovedHandler()
        {
            _shipSprite.X = _ship.CurrentPosition.X * Constants.CellSize;
            _shipSprite.Y = _ship.CurrentPosition.Y * Constants.CellSize;

            OnPathChangedHandler();
        }

        public event EventHandler<DrawnObjectViewModel> OnSpriteAdded;
        public event EventHandler<DrawnObjectViewModel> OnSpriteChanged;
        public event EventHandler<DrawnObjectViewModel> OnSpriteRemoved;
    }
}