using System.Globalization;
using System.Windows;
using System.Windows.Media;

namespace MultiAgentSystem.UI.Internal
{
    internal class DrawingHelpers
    {
        public static FormattedText GetFormattedText(string text, int size, Brush brush)
        {
            var formattedText = new FormattedText(text,
                CultureInfo.CurrentCulture,
                FlowDirection.LeftToRight,
                new Typeface(
                    new FontFamily("Arial"),
                    new FontStyle(),
                    FontWeight.FromOpenTypeWeight(300),
                    new FontStretch()),
                10,
                brush,
                new NumberSubstitution(),
                96);
            return formattedText;
        }
    }
}
