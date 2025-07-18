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
    #region [Q6] StackPanel을 활용하여 상단에서 숫자를 선택하고 하단에 박스를 추가하는 윈도우 예제
    /// <summary>
    /// StackPanelDemo.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class StackPanelDemo : Window
    {
        private readonly List<int> topNumbers = new();
        private int? selectedNumber = null;

        public StackPanelDemo()
        {
            InitializeComponent();
            GenerateRandomNumbers();
        }

        private void GenerateRandomNumbers()
        {
            var rand = new Random();
            topNumbers.Clear();
            while (topNumbers.Count < 10)
            {
                int num = rand.Next(1, 100);
                if (!topNumbers.Contains(num))
                    topNumbers.Add(num);
            }

            RefreshTopPanel();
        }

        private void RefreshTopPanel()
        {
            TopPanel.Children.Clear();

            foreach (var num in topNumbers)
            {
                var border = new Border
                {
                    BorderBrush = Brushes.Gray,
                    BorderThickness = new Thickness(1),
                    Background = (selectedNumber == num) ? Brushes.LightBlue : Brushes.White,
                    Padding = new Thickness(10),
                    Margin = new Thickness(5),
                    Cursor = System.Windows.Input.Cursors.Hand
                };

                var tb = new TextBlock
                {
                    Text = num.ToString(),
                    FontSize = 16,
                    HorizontalAlignment = HorizontalAlignment.Center
                };

                border.Child = tb;
                border.MouseLeftButtonUp += (s, e) =>
                {
                    selectedNumber = num;
                    RefreshTopPanel();
                };

                TopPanel.Children.Add(border);
            }
        }

        private void AddSelected_Click(object sender, RoutedEventArgs e)
        {
            if (selectedNumber.HasValue && topNumbers.Contains(selectedNumber.Value))
            {
                var number = selectedNumber.Value;

                // 하단에 박스 추가
                var box = new Border
                {
                    BorderBrush = Brushes.Black,
                    BorderThickness = new Thickness(1),
                    Margin = new Thickness(5),
                    Padding = new Thickness(10),
                    Child = new TextBlock
                    {
                        Text = number.ToString(),
                        FontSize = 16,
                        HorizontalAlignment = HorizontalAlignment.Center
                    }
                };
                BottomPanel.Children.Add(box);

                // 상단에서 제거
                topNumbers.Remove(number);
                selectedNumber = null;
                RefreshTopPanel();
            }
        }

        private void RemoveLast_Click(object sender, RoutedEventArgs e)
        {
            if (BottomPanel.Children.Count > 0)
            {
                var last = BottomPanel.Children[BottomPanel.Children.Count - 1] as Border;
                if (last?.Child is TextBlock tb && int.TryParse(tb.Text, out int value))
                {
                    // 복원: 상단 맨 앞에 추가
                    topNumbers.Insert(0, value);
                }

                BottomPanel.Children.RemoveAt(BottomPanel.Children.Count - 1);
                RefreshTopPanel();
            }
        }
        #region [Q6-2] OrientationComboBox를 활용하여 StackPanel의 Orientation을 변경하는 기능
        private void OrientationComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(BottomPanel == null) return;

            if (OrientationComboBox.SelectedItem is ComboBoxItem selected)
            {
                string value = selected.Content.ToString();
                BottomPanel.Orientation = value == "Horizontal"
                    ? Orientation.Horizontal
                    : Orientation.Vertical;
            }
        }
        #endregion
    }
    #endregion
}
