using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Remoting.Channels;
using System.Collections;
using System.Runtime.Remoting.Channels.Tcp;
using System.Runtime.Remoting;
using RemoteObject;

namespace Sunucu
{
    class Program
    {
        static void Main(string[] args)
        {
            BinaryClientFormatterSinkProvider cli = new
           BinaryClientFormatterSinkProvider();
            BinaryServerFormatterSinkProvider slv = new
            BinaryServerFormatterSinkProvider();
            slv.TypeFilterLevel = System.Runtime.Serialization.Formatters.
                TypeFilterLevel.Full;
            Hashtable ht = new Hashtable();
            ht["port"] = 1797;
            TcpChannel c = new TcpChannel(ht, cli, slv);
            ChannelServices.RegisterChannel(c, false);
            RemotingConfiguration.RegisterWellKnownServiceType(typeof
                (remotObject), "chatUri", WellKnownObjectMode.Singleton);
            Console.WriteLine("Sunucu aktif");
            Console.ReadLine();
        }
    }
}
