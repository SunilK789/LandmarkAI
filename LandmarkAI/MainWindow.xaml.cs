using LandmarkAI.Models;
using Microsoft.Win32;
using Newtonsoft.Json;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
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
            dialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);

            if (dialog.ShowDialog() == true)
            {
                string fileName = dialog.FileName;
                selectedImage.Source = new BitmapImage(new Uri(fileName));

                _ = MakePredictionAsync(fileName);
            }
        }

        private async Task MakePredictionAsync(string fileName)
        {
            string predictionKey = "c72ba08f5b8211149b5a";
            string endpoint = "https://centralindia.api.cognitive.microsoft.com/"; // e.g., https://<region>.api.cognitive.microsoft.com/
            string projectId = "4da347d-460c-963a-c9dc34a691b4";
            string publishedModelName = "Iteration1";

            string url = $"{endpoint}/customvision/v3.0/Prediction/{projectId}/classify/iterations/{publishedModelName}/image";
            string content_type = "application/octet-stream";

            using HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("Prediction-Key", predictionKey);

            byte[] imageData = File.ReadAllBytes(fileName);
            using (var content = new ByteArrayContent(imageData))
            {
                content.Headers.ContentType = new MediaTypeHeaderValue(content_type);

                HttpResponseMessage response = await client.PostAsync(url, content);
                string result = await response.Content.ReadAsStringAsync();

                var predictions = JsonConvert.DeserializeObject<CustomVision>(result)?.Predictions;
                listViewPredictions.ItemsSource = predictions;
            }

        }
    }
}