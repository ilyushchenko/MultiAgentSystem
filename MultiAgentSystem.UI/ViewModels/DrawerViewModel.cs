using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using MultiAgentSystem.BLL.Models;
using MultiAgentSystem.BLL.Units.ShipModel;
using GraphicsPoint = System.Windows.Point;
using Point = MultiAgentSystem.BLL.Models.Point;

namespace MultiAgentSystem.UI.ViewModels
{
    public class DrawerViewModel : BaseViewModel
    {
        private readonly ObservableCollection<DrawnObjectViewModel> _drawableObjects;

        public DrawerViewModel()
        {
            _drawableObjects = new ObservableCollection<DrawnObjectViewModel>();
            DrawableObjects = new ReadOnlyObservableCollection<DrawnObjectViewModel>(_drawableObjects);
        }

        public ReadOnlyObservableCollection<DrawnObjectViewModel> DrawableObjects { get; }

        public void DrawShip(Ship ship)
        {
            var drawer = new ShipDrawerViewModel(ship);

            drawer.OnSpriteAdded += SpriteAddedHandler;
            drawer.OnSpriteChanged += SpriteChangedHandler;
            drawer.OnSpriteRemoved += SpriteRemovedHandler;

            foreach (var sprite in drawer.Sprites) _drawableObjects.Add(sprite);
        }

        public void DrawMap(Map map)
        {
            var maxDepth = map.GetMaxDepth();

            for (var x = 0; x < map.Width; x++)
            for (var y = 0; y < map.Height; y++)
            {
                var cell = map.GetCell(new Point(x, y));

                var drawing = new DrawingVisual();

                SolidColorBrush brush;
                if (cell.Depth > 0)
                {
                    var depthCoefficient = cell.Depth / maxDepth;
                    var redValue = 255 - 255 * depthCoefficient;
                    brush = new SolidColorBrush(Color.FromArgb(255, (byte) redValue, 255, 0));
                }
                else
                {
                    brush = new SolidColorBrush(Color.FromArgb(64, 255, 0, 0));
                }

                using (var context = drawing.RenderOpen())
                {
                    context.DrawRectangle(brush, new Pen(Brushes.Black, 2),
                        new Rect(new GraphicsPoint(0, 0), new GraphicsPoint(Constants.CellSize, Constants.CellSize)));
                }


                var source = new RenderTargetBitmap(Constants.CellSize, Constants.CellSize, 96, 96, PixelFormats.Pbgra32);
                source.Render(drawing);

                var drawer = new DrawnObjectViewModel(source)
                {
                    X = x * Constants.CellSize,
                    Y = y * Constants.CellSize
                };
                _drawableObjects.Add(drawer);
            }
        }

        private void SpriteChangedHandler(object sender, DrawnObjectViewModel e)
        {
            throw new NotImplementedException();
        }

        private void SpriteRemovedHandler(object sender, DrawnObjectViewModel e)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                _drawableObjects.Remove(e);
                OnPropertyChanged(nameof(DrawableObjects));
            });
        }

        private void SpriteAddedHandler(object sender, DrawnObjectViewModel e)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                _drawableObjects.Add(e);
                OnPropertyChanged(nameof(DrawableObjects));
            });
        }
    }
}