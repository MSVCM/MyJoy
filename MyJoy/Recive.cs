using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Sockets;
using System.Net;
using System.Threading;

namespace MyJoy
{
    public class Receive
    {
        public Socket client;
        public string result;
        public Thread receiveThread;

        public float time { get; set; }

        public int num = 0;

        public Receive(Socket c,int i,Thread t)
        {
            client = c;
            num = i;
            receiveThread = t;
            time = 5;
        }

    }
}
