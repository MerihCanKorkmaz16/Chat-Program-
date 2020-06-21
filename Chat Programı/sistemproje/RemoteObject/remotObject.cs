using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Runtime.Remoting.Messaging;

namespace RemoteObject
{
    public delegate void messDelegate(string text);
    public delegate void alertDelegate(string kisi);
    public delegate void drawDelegate(int x, int y, Color renk);

    public class remotObject : MarshalByRefObject
    {
        public string getMachineName()
        {
            return Environment.MachineName;
        }
        public event messDelegate messEvent;

        public void SendMessage(string text)
        {
            if (messEvent != null)
            {
                messEvent(text);
            }
        }
        public event alertDelegate alertEvent;
        public void alert(string kisi)
        {
            if (alertEvent != null)
            {
                alertEvent(kisi);
            }
        }
        public event drawDelegate drawEvent;

        public void draw(int x, int y, Color renk)
        {
            if (drawEvent != null)
            {
                drawEvent(x, y, renk);
            }
        }

    }

    public class messObject : MarshalByRefObject
    {
        public event messDelegate messEvent;

        [OneWay]
        public void SendMessage(string text)
        {
            if (messEvent != null)
            {
                messEvent(text);
            }
        }

    }

    public class alertObject : MarshalByRefObject
    {
        public event alertDelegate alertEvent;

        [OneWay]
        public void alert(string kisi)
        {
            if (alertEvent != null)
            {
                alertEvent(kisi);
            }
        }
    }

    public class drawObject : MarshalByRefObject
    {
        public event drawDelegate drawEvent;
        [OneWay]
        public void draw(int x, int y, Color renk)
        {
            if (drawEvent != null)
            {
                drawEvent(x, y, renk);
            }
        }
    }
}