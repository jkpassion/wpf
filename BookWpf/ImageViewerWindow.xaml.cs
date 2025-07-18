using System;
using System.Collections.Generic;
using System.IO;
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
    /// ImageViewerWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class ImageViewerWindow : Window
    {
        public ImageViewerWindow()
        {
            InitializeComponent();
        }
        #region [Q4] 특정 이미지 경로를 지정하면 화면에 출력하는 윈도우 예제
        void LoadImg(string imagePath)
        {
            if (!File.Exists(imagePath))
            {
                MessageBox.Show("이미지 파일이 존재하지 않습니다.", "오류", MessageBoxButton.OK, MessageBoxImage.Error);
                Close();
                return;
            }

            try
            {
                var bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.UriSource = new Uri(imagePath, UriKind.Absolute);
                bitmap.CacheOption = BitmapCacheOption.OnLoad;
                bitmap.EndInit();
                DisplayImage.Source = bitmap;

                MessageBox.Show($"이미지 해상도 (DpiX: {bitmap.DpiX}, DpiY: {bitmap.DpiY})");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"이미지를 불러오는 중 오류 발생:\n{ex.Message}", "예외", MessageBoxButton.OK, MessageBoxImage.Error);
                Close();
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoadImg(@"c:\developing\_design\_ui\web\web1.png");
        }
        #endregion
    }
}
