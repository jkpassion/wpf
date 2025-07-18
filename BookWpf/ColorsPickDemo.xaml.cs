using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace BookWpf
{
    /// <summary>
    /// ColorsPickDemo.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class ColorsPickDemo : Window
    {
        public ColorsPickDemo()
        {
            InitializeComponent();
            LoadColors();
        }

        private void LoadColors()
        {
            var colorProps = typeof(Colors).GetProperties(BindingFlags.Public | BindingFlags.Static);
            foreach (var prop in colorProps.OrderBy(p => p.Name))
            {
                var color = (Color)prop.GetValue(null);
                var brush = new SolidColorBrush(color);

                var border = new Border
                {
                    Width = 120,
                    Height = 30,
                    Background = brush,
                    BorderBrush = Brushes.Black,
                    BorderThickness = new Thickness(1),
                    Margin = new Thickness(3),
                    Cursor = System.Windows.Input.Cursors.Hand,
                    Tag = prop.Name
                };

                var text = new TextBlock
                {
                    Text = prop.Name,
                    Foreground = GetReadableTextColor(color),
                    VerticalAlignment = VerticalAlignment.Center,
                    HorizontalAlignment = HorizontalAlignment.Center
                };

                border.Child = text;

                border.MouseLeftButtonUp += (s, e) =>
                {
                    var colorName = (s as Border)?.Tag?.ToString();
                    if (!string.IsNullOrEmpty(colorName))
                    {
                        Clipboard.SetText($"Colors.{colorName}");
                        MessageBox.Show($"복사됨: Colors.{colorName}", "Clipboard", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                };

                ColorGrid.Children.Add(border);
            }
        }

        private Brush GetReadableTextColor(Color color)
        {
            double brightness = color.R * 0.299 + color.G * 0.587 + color.B * 0.114;
            return brightness > 186 ? Brushes.Black : Brushes.White;
        }
    }
}
