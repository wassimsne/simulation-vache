// See https://aka.ms/new-console-template for more information

using simulation_vache;
using System.Collections.Generic;


List<Vache> _vaches = new List <Vache>
     {
            new Vache(1),
            new Vache (2),
            new Vache (3)
     };
Mutex _mutex = new Mutex();
Thread[] _poolthread = new Thread[_vaches.Count];
List<Mesure> _mesures = new List<Mesure>();
int numerothread = 0;
foreach(Vache V in _vaches)
{
    _poolthread[numerothread++] = new Thread(() => Capture(V));

}
foreach(Thread t in _poolthread)
{

    t.Start();

}
Thread _tmesure = new Thread(() => emetteur());
_tmesure.Start();


void emetteur()
{
    _mutex.WaitOne();
    try
    {
        if (_mesures.Count> 10)
        {
            int i = 0;
            while(i<_mesures.Count)
            {
                Console.WriteLine("Les parametres:{0}", _mesures[i++]);
                
            }
            _mesures.Clear();

        }
    }
    finally
    {
        _mutex.ReleaseMutex();
    }
   
   
}

void Capture(Object v)
{
    while (true)
    {
        var vache = v as Vache;
        Random Mesure = new Random();
        float m1 = Mesure.Next(10, 20);
        float m2 = Mesure.Next(10, 20);

        _mesures.Add(new Mesure { idMesure = 1, idVache = vache.IDVache, valeur = m1, time = DateTime.Now, Type = TypeMesure.Cardiaque });
        _mesures.Add(new Mesure { idMesure = 2, idVache = vache.IDVache, valeur = m2, time = DateTime.Now, Type = TypeMesure.Temperature });
    }
}