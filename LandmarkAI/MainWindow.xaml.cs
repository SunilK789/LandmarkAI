using Microsoft.Win32;
using System.Windows;
using System.Windows.Media.Imaging;

namespace LandmarkAI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void selectButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Image files (*.png;*.jpg)|*.png;*.jpg;*.jpeg|All files (*.*)|*.*";
            if (dialog.ShowDialog() == true)
            {
                string fileName = dialog.FileName;
                selectedImage.Source = new BitmapImage(new Uri(fileName));
            }
        }
    }
}