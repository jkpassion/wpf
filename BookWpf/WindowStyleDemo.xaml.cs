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
    #region [Q5] 다양한 WindowStyle을 적용해 볼 수 있는 윈도우 예제
    /// <summary>
    /// WindowStyleDemo.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class WindowStyleDemo : Window
    {
        public WindowStyleDemo()
        {
            InitializeComponent();
            LoadWindowStyles();
        }

        private void LoadWindowStyles()
        {
            foreach (WindowStyle style in Enum.GetValues(typeof(WindowStyle)))
            {
                StyleListBox.Items.Add(style);
            }
        }

        private void StyleListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (StyleListBox.SelectedItem is WindowStyle selectedStyle)
            {
                var sample = new Window
                {
                    Title = $"Style: {selectedStyle}",
                    Width = 400,
                    Height = 200,
                    WindowStyle = selectedStyle,
                    Content = new TextBlock
                    {
                        Text = $"이 창은 WindowStyle.{selectedStyle} 스타일입니다.",
                        FontSize = 16,
                        Margin = new Thickness(20),
                        TextWrapping = TextWrapping.Wrap
                    }
                };

                sample.Show();
            }
        }
    }
    #endregion
}
