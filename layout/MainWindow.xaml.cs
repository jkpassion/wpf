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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace layout
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            //windowMenu.SubmenuOpened += WindowMenu_SubmenuOpened;
        }

        //private void WindowMenu_SubmenuOpened(object sender, RoutedEventArgs e)
        //{
        //    windowMenu.Items.Clear();
        //    foreach (Window window in Application.Current.Windows)
        //    {
        //        MenuItem item = new MenuItem();
        //        item.Header = window.Title;
        //        item.Click += windowMenuItem_Click;
        //        item.Tag = window;
        //        item.IsChecked = window.IsActive;
        //        windowMenu.Items.Add(item);
        //    }

        //}
        //void windowMenuItem_Click(object sender, RoutedEventArgs e)
        //{
        //    Window window = (Window)((MenuItem)sender).Tag;
        //    window.Activate();

        //    MessageBox.Show(new Config().Conf1);
        //}

        private void windowMenu_Loaded(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
