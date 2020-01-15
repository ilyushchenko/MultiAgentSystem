using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using MultiAgentSystem.BLL.Units.ShipModel;
using Point = System.Windows.Point;

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

            var source = GetImage();

            _shipSprite = new DrawnObjectViewModel(source)
            {
                X =  _ship.CurrentPosition.X * Constants.CellSize,
                Y =  _ship.CurrentPosition.Y * Constants.CellSize
            };
            _pathSprites = new List<DrawnObjectViewModel>();

            Sprites.Add(_shipSprite);
            _ship.OnMoved += ShipMovedHandler;
            _ship.OnPathChanged += OnPathChangedHandler;
            _ship.OnDirectionChanged += DirectionChangedHandler;
            _ship.OnDie += DieHandler;
        }

        private void DieHandler()
        {
            _ship.OnMoved -= ShipMovedHandler;
            _ship.OnPathChanged -= OnPathChangedHandler;
            _ship.OnDirectionChanged -= DirectionChangedHandler;
            _ship.OnDie -= DieHandler;
            ClearSprites();
            OnSpriteRemoved?.Invoke(this, _shipSprite);
        }

        private void ClearSprites()
        {
            foreach (var pathSprite in _pathSprites) 
                OnSpriteRemoved?.Invoke(this, pathSprite);
            _pathSprites.Clear();
        }

    private RenderTargetBitmap GetImage()
        {
            RenderTargetBitmap source = null;
            Application.Current.Dispatcher.Invoke(() =>
            {
                var drawing = new DrawingVisual();
                using (var context = drawing.RenderOpen())
                {
                    context.DrawRectangle(Brushes.Blue, null,
                        new Rect(new Point(0, 0), new Point(Constants.CellSize, Constants.CellSize)));

                    var text = new FormattedText(_ship.MoveDirection.ToString(),
                        CultureInfo.CurrentCulture,
                        FlowDirection.LeftToRight,
                        new Typeface(
                            new FontFamily("Arial"),
                            new FontStyle(),
                            FontWeight.FromOpenTypeWeight(300),
                            new FontStretch()),
                        10,
                        new SolidColorBrush(Colors.White),
                        new NumberSubstitution(),
                        96);

                    context.DrawText(text, new Point(0, 0));
                }


                source =
                    new RenderTargetBitmap(Constants.CellSize, Constants.CellSize, 96, 96, PixelFormats.Pbgra32);
                source.Render(drawing);
            });
            return source;
        }

        public ObservableCollection<DrawnObjectViewModel> Sprites { get; set; }

        private void OnPathChangedHandler()
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                ClearSprites();

                var drawing = new DrawingVisual();
                using (var context = drawing.RenderOpen())
                {
                    context.DrawEllipse(Brushes.YellowGreen, null,
                        new Point(25, 25), 5, 5);
                }

                var source =
                    new RenderTargetBitmap(Constants.CellSize, Constants.CellSize, 96, 96, PixelFormats.Pbgra32);
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

        private void DirectionChangedHandler()
        {
            _shipSprite.Sprite = GetImage();
        }

        public event EventHandler<DrawnObjectViewModel> OnSpriteAdded;
        public event EventHandler<DrawnObjectViewModel> OnSpriteChanged;
        public event EventHandler<DrawnObjectViewModel> OnSpriteRemoved;
    }
}