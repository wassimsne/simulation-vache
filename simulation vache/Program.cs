// See https://aka.ms/new-console-template for more information

using simulation_vache;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Azure.Devices.Client;
using System.Text;
using Newtonsoft.Json;
using simulation_vache.StringExtension;

string connectionString = "HostName=bovin-iothub.azure-devices.net;DeviceId=Rasp;SharedAccessKeyName=iothubowner;SharedAccessKey=3aIMlO3n3vq/dREdDBrEh0T5pwgeZvzXLQHnZ5l9fww=";
 DeviceClient deviceClient= DeviceClient.CreateFromConnectionString(connectionString);
Console.WriteLine("Hello World Modified From VS Code!");
//String message;




 static async void SendDeviceToCloudMessageAsync(string Message, DeviceClient deviceClient)
{
    
    Message message = new Message(Encoding.ASCII.GetBytes(Message));

    message.Properties.Add("buttonEvent", "true");
    
    await deviceClient.SendEventAsync(message);

    Console.WriteLine("Sending Message {0}", Message);
}




List<Vache> _vaches = new List<Vache>
{
    new Vache(1),
    new Vache(2),
    new Vache(3)
};

Random _random = new Random();

List<Mesure> _mesures = new List<Mesure>();

Mutex _mutex = new Mutex();

Thread[] _poolthread = new Thread[_vaches.Count];

int numerothread = 0;

foreach (Vache v in _vaches)
{
    _poolthread[numerothread++] = new Thread(() => Capture(v));
}

foreach (Thread t in _poolthread)
{
    t.Start();
}

Thread _tmesure = new Thread(() => emetteur());
_tmesure.Start();

async Task emetteur()
{
    try
    {
        while (true)
        {
            await Task.Delay(1000);

            _mutex.WaitOne();

            if (_mesures.Count > 10)
            {
                foreach (var mesure in _mesures)
                {
                    var Message = JsonConvert.SerializeObject(mesure);

                    // Console.WriteLine("Tappez votre message");
                    //String MonMessage = Console.ReadLine();
                    SendDeviceToCloudMessageAsync(Message, deviceClient);
                    "message envoyé à IOTHUB".dump(ConsoleColor.Green);
                    Thread.Sleep(2000);
                    Console.WriteLine("Les parametres: {0}", mesure);
                    await Task.Delay(1000);
                }
               
                
                _mesures.Clear();
            }

            _mutex.ReleaseMutex();
        }
    }
    catch (AbandonedMutexException ex)
    {
        // Handle the abandoned mutex exception
        Console.WriteLine("Mutex abandoned: " + ex);
        // Perform cleanup operations or retry acquiring the mutex
    }
}

void Capture(Vache v)
{
    try
    {
        while (true)
        {
            _mutex.WaitOne();

            float m1 = _random.Next(10, 20);
            float m2 = _random.Next(10, 20);

            _mesures.Add(new Mesure { idMesure = 1, idVache = v.IDVache, valeur = m1, time = DateTime.Now, Type = TypeMesure.Cardiaque });
            _mesures.Add(new Mesure { idMesure = 2, idVache = v.IDVache, valeur = m2, time = DateTime.Now, Type = TypeMesure.Temperature });

            _mutex.ReleaseMutex();
        }
    }
    catch (Exception ex)
    {
        // Handle the exception
        Console.WriteLine("An error occurred: " + ex.Message);
    }
}


