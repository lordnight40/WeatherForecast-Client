using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Net.Http;
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

using Client.Model;
using Client.WeatherService;

namespace Client.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private WeatherForecastService weatherForecast;

        public MainWindow()
        {
            InitializeComponent();
            weatherForecast = new WeatherForecastService();
            DateInput.DisplayDateStart = DateTime.Now;
            DateInput.DisplayDateEnd = DateInput.DisplayDateStart.Value.AddDays(9);
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            if (DateInput.SelectedDate > (DateInput.DisplayDateStart.Value.AddDays(9)))
            {
                DateInput.SelectedDate = DateInput.DisplayDateStart.Value.AddDays(9);
            }

            List<WeatherRecord> records;
            weatherPanel.Children.Clear();
            try
            {
                Progress.Visibility = Visibility.Visible;
                SearchButton.IsEnabled = false;
                records = await weatherForecast.GetForecast(CityInput.Text, DateInput.SelectedDate ?? DateTime.Now);

                if (records == null || records.Count == 0)
                {
                    MessageBox.Show(this, "По Вашему запросу ничего не найдено. Проверьте корректность запроса.", "Неверный запрос", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }

                foreach (var item in records)
                {
                    WeatherView weatherRecordView = new WeatherView();
                    weatherRecordView.TimeBox.Text = item.Time;
                    weatherRecordView.TemperatureBox.Text = item.Temperature;
                    weatherRecordView.PresureBox.Text = item.Pressure;
                    weatherRecordView.Humidity.Text = item.Humidity;
                    weatherRecordView.WindBox.Text = item.WindSpeed;
                    weatherRecordView.Precipiation.Text = item.Precipiation;
                    weatherPanel.Children.Add(weatherRecordView);
                }

            } catch (HttpRequestException ex)
            {
                MessageBox.Show(this,  ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                Progress.Visibility = Visibility.Hidden;
                SearchButton.IsEnabled = true;
            }
        }

        private void CityInput_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (CityInput.Text.Length == 0)
            {
                CityInput.BorderBrush = Brushes.Red;
                SearchButton.IsEnabled = false;
            }
            else
            {
                CityInput.BorderBrush = System.Windows.SystemColors.ActiveBorderBrush;
                SearchButton.IsEnabled = true;
            }
        }
    }
}
