using System;
using System.Collections.Generic;
using System.Linq;
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
    /// ColorDemoWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class ColorDemoWindow : Window
    {
        public ColorDemoWindow()
        {
            InitializeComponent();
        }

        private void MethodComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string method = (MethodComboBox.SelectedItem as ComboBoxItem)?.Content.ToString();
            InputPanel.Children.Clear();

            switch (method)
            {
                case "FromArgb":
                    AddArgbInputs();
                    break;
                case "FromRgb":
                    AddRgbInputs();
                    break;
                case "FromScRgb":
                    AddScRgbInputs();
                    break;
                case "FromName":
                    AddNamedColorInput();
                    break;
                case "FromHex":
                    AddHexInput();
                    break;
            }
        }
        private void AddHexInput()
        {
            // 입력 필드 구성
            var stack = new StackPanel { Orientation = Orientation.Horizontal, Margin = new Thickness(0, 10, 0, 0) };

            var label = new TextBlock
            {
                Text = "HEX:",
                Width = 40,
                VerticalAlignment = VerticalAlignment.Center
            };

            var textBox = new TextBox
            {
                Width = 150,
                Text = "#FF5733", // 기본값
                Margin = new Thickness(5, 0, 0, 0)
            };

            stack.Children.Add(label);
            stack.Children.Add(textBox);
            InputPanel.Children.Add(stack);

            // 미리보기 버튼 추가
            AddPreviewButton(() =>
            {
                try
                {
                    var hex = textBox.Text.Trim();
                    var color = (Color)ColorConverter.ConvertFromString(hex);
                    string code = $"ColorConverter.ConvertFromString(\"{hex}\")";
                    ShowColor(color, code);
                }
                catch
                {
                    MessageBox.Show("HEX 문자열이 유효하지 않습니다. 예: #RRGGBB 또는 #AARRGGBB", "오류", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            });
        }

        private void AddScRgbInputs()
        {
            // ScRGB 범위는 0.0 ~ 1.0
            var a = CreateSlider("A", 0.0, 1.0);
            var r = CreateSlider("R", 0.0, 1.0);
            var g = CreateSlider("G", 0.0, 1.0);
            var b = CreateSlider("B", 0.0, 1.0);

            // 소수점 표현을 위한 슬라이더 세팅
            a.TickFrequency = 0.01;
            r.TickFrequency = 0.01;
            g.TickFrequency = 0.01;
            b.TickFrequency = 0.01;

            a.IsSnapToTickEnabled = false;
            r.IsSnapToTickEnabled = false;
            g.IsSnapToTickEnabled = false;
            b.IsSnapToTickEnabled = false;

            // 미리보기 버튼
            AddPreviewButton(() =>
            {
                var color = Color.FromScRgb((float)a.Value, (float)r.Value, (float)g.Value, (float)b.Value);
                string code = $"Color.FromScRgb({a.Value:F2}f, {r.Value:F2}f, {g.Value:F2}f, {b.Value:F2}f)";
                ShowColor(color, code);
            });
        }
        private void AddNamedColorInput()
        {
            // 입력 텍스트 박스 구성
            var stack = new StackPanel { Orientation = Orientation.Horizontal, Margin = new Thickness(0, 10, 0, 0) };

            var label = new TextBlock
            {
                Text = "이름:",
                Width = 40,
                VerticalAlignment = VerticalAlignment.Center
            };

            var textBox = new TextBox
            {
                Width = 150,
                Text = "Red", // 기본값
                Margin = new Thickness(5, 0, 0, 0)
            };

            stack.Children.Add(label);
            stack.Children.Add(textBox);
            InputPanel.Children.Add(stack);

            // 미리보기 버튼 추가
            AddPreviewButton(() =>
            {
                try
                {
                    var color = (Color)ColorConverter.ConvertFromString(textBox.Text.Trim());
                    string code = $"ColorConverter.ConvertFromString(\"{textBox.Text.Trim()}\")";
                    ShowColor(color, code);
                }
                catch
                {
                    MessageBox.Show("유효하지 않은 색상 이름입니다. 예: Red, Blue, CornflowerBlue 등", "오류", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            });
        }

        private void AddRgbInputs()
        {
            // RGB 슬라이더 생성
            var r = CreateSlider("R", 0, 255);
            var g = CreateSlider("G", 0, 255);
            var b = CreateSlider("B", 0, 255);

            // 미리보기 버튼 추가
            AddPreviewButton(() =>
            {
                var color = Color.FromRgb((byte)r.Value, (byte)g.Value, (byte)b.Value);
                string code = $"Color.FromRgb({(byte)r.Value}, {(byte)g.Value}, {(byte)b.Value})";
                ShowColor(color, code);
            });
        }

        private void AddArgbInputs()
        {
            var a = CreateSlider("A", 0, 255);
            var r = CreateSlider("R", 0, 255);
            var g = CreateSlider("G", 0, 255);
            var b = CreateSlider("B", 0, 255);
            AddPreviewButton(() => {
                var color = Color.FromArgb((byte)a.Value, (byte)r.Value, (byte)g.Value, (byte)b.Value);
                ShowColor(color, $"Color.FromArgb({a.Value}, {r.Value}, {g.Value}, {b.Value})");
            });
        }
        private void ShowColor(Color color, string codeSnippet)
        {
            // 배경색 미리보기
            PreviewBox.Background = new SolidColorBrush(color);

            // 코드 스니펫 표시
            CodeTextBlock.Text = codeSnippet;

            // 텍스트 색상 자동 반전 (명암비 고려)
            var brightness = (color.R * 0.299 + color.G * 0.587 + color.B * 0.114) / 255;
            CodeTextBlock.Foreground = brightness > 0.5 ? Brushes.Black : Brushes.White;
        }

        private void AddPreviewButton(Action onClick)
        {
            var button = new Button
            {
                Content = "색상 미리보기",
                Width = 120,
                Height = 30,
                Margin = new Thickness(0, 10, 0, 0),
                HorizontalAlignment = HorizontalAlignment.Left
            };

            button.Click += (s, e) => onClick();

            InputPanel.Children.Add(button);
        }

        // 기타 방식도 유사하게 구현 가능
        private Slider CreateSlider(string label, double min, double max)
        {
            var stack = new StackPanel { Orientation = Orientation.Horizontal, Margin = new Thickness(0, 5, 0, 5) };

            // Label
            var textBlock = new TextBlock
            {
                Text = $"{label}:",
                Width = 30,
                VerticalAlignment = VerticalAlignment.Center
            };

            // Slider
            var slider = new Slider
            {
                Minimum = min,
                Maximum = max,
                Width = 200,
                Margin = new Thickness(10, 0, 10, 0),
                TickFrequency = 1,
                IsSnapToTickEnabled = true,
                Tag = label
            };

            // Value TextBox (optional, shows current value)
            var valueBox = new TextBox
            {
                Width = 40,
                IsReadOnly = true,
                Text = slider.Value.ToString(),
                VerticalContentAlignment = VerticalAlignment.Center
            };

            // Event handler to sync value
            slider.ValueChanged += (s, e) =>
            {
                valueBox.Text = ((int)slider.Value).ToString();
            };

            // Add to InputPanel
            stack.Children.Add(textBlock);
            stack.Children.Add(slider);
            stack.Children.Add(valueBox);
            InputPanel.Children.Add(stack);

            return slider;
        }

    }
}
