using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;

namespace SensorRealTime.Hubs
{
    [HubName("sensorHub")]
    public class SensorHub : Hub
    {
        private readonly ControleSensor _controleSensor;

        public SensorHub() : this(ControleSensor.Instancia) { }

        public SensorHub(ControleSensor controle)
        {
            _controleSensor = controle;
        }

        public void IniciarTransmissaoSensor()
        {
            _controleSensor.RealizaTransmissaoSensor();
        }
    }
}