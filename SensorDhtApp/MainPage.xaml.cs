using Sensors.Dht;
using System;
using System.Net.Http;
using Windows.Devices.Gpio;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace SensorDhtApp
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private DispatcherTimer timer = new DispatcherTimer();
        private GpioPin pin = null;
        private IDht sensor = null;
        private float umidade = 0f;
        private float temperatura = 0f;

        public MainPage()
        {
            this.InitializeComponent();

            timer.Interval = TimeSpan.FromSeconds(5);
            timer.Tick += timer_Tick;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            pin = GpioController.GetDefault().OpenPin(17, GpioSharingMode.Exclusive);
            sensor = new Dht11(pin, GpioPinDriveMode.Input);

            timer.Start();
        }

        private async void timer_Tick(object sender, object e)
        {
            DhtReading reading = new DhtReading();
            reading = await sensor.GetReadingAsync().AsTask();

            if (reading.IsValid)
            {
                this.temperatura = Convert.ToSingle(reading.Temperature);
                this.umidade = Convert.ToSingle(reading.Humidity);

                //Aqui é possível chamar uma API, passando os valores de temperatura e umidade, fazendo que sua API insira em seu banco de dados esses valores
                var url = "SUA_URL_AQUI";
                var token = "SEU_TOKEN_AQUI";
                var client = new HttpClient();
                var response = client.GetAsync(new Uri($"http://{url}/api/UpdateInfo/{token}?temperatura={this.temperatura}&umidade={this.umidade}"));
            }
        }
    }
}
