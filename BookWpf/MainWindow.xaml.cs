using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

/*
 * 요구사항(requirements) project: BookWpf
 * 
 * (25.7.13)
 * Q1) wpf에는 Color를 생성하는 방법이 여러가지 있다. 이를 구현하는 윈도우를 설계해줘
 * Q1-1) CreateSlider() 메서드를 완성
 * Q1-2) AddPreviewButton 도 완성
 * Q1-3) ShowColor도 완성
 * Q1-4) AddRgbInputs 완성
 * Q1-5) AddScRgbInputs 완성
 * Q1-6) AddNamedColorInput 완성
 * Q1-7) AddHexInput 완성
 * 
 * Q2) Colors는 141개의 전역 칼라가 지정되어 있다. 이를 Grid로 화면에 모두 출력하는 코드 작성. 사용자가 칼라를 click하면 clipboard에 Colors.xxx 으로 복사해줘
 *  - ColorsPickDemo.xaml
 * Q3) 지정된 디렉토리에서 모든 .xaml 파일을 읽은 후 목록으로 출력하는 Window를 작성해줘. 단, App.xaml, MainWindow.xaml 등 예외로 지정된 윈도우는 목록에 포함하지 않는다. 목록 중 하나를 선택하면 해당 윈도우를 동적으로 load하여 Show() 메서드를 호출한다.
 * Q4) 특정 이미지 경로를 지정하면 화면에 출력하는 윈도우 예제
 *  - ImageViewerWindow.xaml
 * 이후 - requirements.txt 사용
 */
namespace BookWpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly string xamlDirectory = @"c:\developing\git\wpf\BookWpf";//System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory);
        private readonly List<string> excludedFiles = new() { "App.xaml", "MainWindow.xaml" };
        public MainWindow()
        {
            InitializeComponent();
            LoadXamlFileList();
        }
        #region [Q3] 지정된 디렉토리에서 모든 .xaml 파일을 읽은 후 목록으로 출력하는 Window. 예외 허용
        private void LoadXamlFileList()
        {
            var xamlFiles = Directory.GetFiles(xamlDirectory, "*.xaml")
                                     .Select(System.IO.Path.GetFileName)
                                     .Where(file => !excludedFiles.Contains(file, StringComparer.OrdinalIgnoreCase))
                                     .ToList();

            foreach (var file in xamlFiles)
            {
                string className = System.IO.Path.GetFileNameWithoutExtension(file);
                XamlListBox.Items.Add(className);
            }
        }

        private void XamlListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (XamlListBox.SelectedItem is string className)
            {
                try
                {
                    // 전체 네임스페이스 경로 설정 필요 (예: XamlLoaderDemo.Views.MyWindow)
                    string fullTypeName = $"BookWpf.{className}";
                    Type type = Type.GetType(fullTypeName);

                    if (type == null)
                    {
                        MessageBox.Show($"클래스 '{fullTypeName}'를 찾을 수 없습니다.", "오류", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }

                    if (Activator.CreateInstance(type) is Window window)
                    {
                        Clipboard.SetDataObject(type.Name);
                        window.Show();
                    }
                    else
                    {
                        MessageBox.Show($"{className}은(는) Window 타입이 아닙니다.", "오류", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"오류 발생: {ex.Message}", "예외", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
        #endregion
    }
}