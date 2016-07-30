using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using SensorRealTime.DAL;
using System;
using System.Threading;

namespace SensorRealTime.Hubs
{
    /// <summary>
    /// Controla a exibição e transmissão dos valores do sensor
    /// </summary>
    public class ControleSensor
    {
        private readonly static Lazy<ControleSensor> _instancia = new Lazy<ControleSensor>(() => new ControleSensor());
        private const int _intervalo = 1000; // 1 segundo
        private Random tempRandom = new Random(0);
        private Random humidRandom = new Random(1);

        public static ControleSensor Instancia
        {
            get
            {
                return _instancia.Value;
            }
        }

        private ControleSensor() { }


        public void RealizaTransmissaoSensor()
        {
            // Cria timer para transmitir as informações em intervalo de tempo
            new Timer(TransmitirDadosClientes, null, _intervalo, _intervalo);
        }

        private void TransmitirDadosClientes(object state)
        {
            CustomContext.UpdateSensorValues();
            // Transmite a informação para todos os clientes, chamando a função atualizaTemperatura e atualizaUmidade
            Clientes<SensorHub>().All.atualizaTemperatura(CustomContext.Temperatura);
            Clientes<SensorHub>().All.atualizaUmidade(CustomContext.Umidade);
        }

        /// <summary>
        /// Obtém os clientes conectados
        /// </summary>
        /// <typeparam name="T">Tipo do Hub</typeparam>
        /// <returns></returns>
        private IHubConnectionContext Clientes<T>() where T : Hub
        {
            return GlobalHost.ConnectionManager.GetHubContext<T>().Clients;
        }
    }
}