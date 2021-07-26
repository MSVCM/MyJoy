#define ROBUST
//#define EFFICIENT
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using vJoyInterfaceWrap;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.IO;

namespace MyJoy
{
    public partial class Form1 : Form
    {
        public struct User
        {
            public string name;
            public uint ID;
            public vJoy joystick;
            public String linkName;
            public Receive socket;
            public bool connected;
        }
        Dictionary<string, uint> dict = new Dictionary<string, uint>();

        Socket server;
        List<User> users = new List<User>();
        int userCount = 0;
        long maxval;

        List<Receive> clients = new List<Receive>();

        System.Windows.Forms.Label[] labels = new System.Windows.Forms.Label[4];

        public Form1()
        {
            InitializeComponent();
            Control.CheckForIllegalCrossThreadCalls = false;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Create one joystick object and a position structure.
            Get_VJD();
            AddListen(int.Parse(textBox_port.Text), IPAddress.Parse(GetIPAddress()));
            label_ip.Text = GetIPAddress();
            try
            {
                string s = File.ReadAllText(Application.StartupPath + "/inf.txt");
                string[] str = s.Split('|');
                foreach (string st in str)
                {
                    string[] strs = st.Split('=');
                    dict.Add(strs[0], uint.Parse(strs[1]));
                }
            }
            catch (Exception E)
            {
                MessageBox.Show(E.Message);
                System.Environment.Exit(0);
            }
            Thread thread = new Thread(CheckLink);
            thread.Start();
            this.FormClosing += new FormClosingEventHandler(Form1_FormClosing);

        }
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            for (int i = 0; i < clients.Count; i++)
            {
                clients[i].client.Shutdown(SocketShutdown.Both);
                //clients[i].client.Close(1000);
            }
            System.Environment.Exit(0);
        }

        void CheckLink()
        {
            while (true)
            {
                for (int i = clients.Count - 1; i >= 0; i--)
                {
                    try
                    {
                        Thread.Sleep(100);
                        clients[i].time -= 0.1f;
                        if (clients[i].time < 0)
                        {
                            clients[i].receiveThread.Abort();
                            clients[i].receiveThread.Join();
                            User u = users[clients[i].num];
                            u.linkName = "未连接";
                            u.connected = false;
                            users[clients[i].num] = u;
                            if (ComboBox_Target.SelectedIndex == clients[i].num)
                                label_linkState.Text = "连接状态：" + users[ComboBox_Target.SelectedIndex].linkName;
                            ComboBox_Target.SelectedIndex = clients[i].num;

                            clients[i] = null;
                            clients.RemoveAt(i);
                        }
                    }
                    catch
                    {
                    }
                }
            }
        }

        private void Get_VJD()
        {
            vJoy joy = new vJoy();
            int k = 1;
            for (uint i = 1; i <= 16; i++)
            {
                if (joy.GetVJDStatus(i) == VjdStat.VJD_STAT_FREE)
                {
                    joy.AcquireVJD(i);
                    joy.ResetAll();
                    joy.GetVJDAxisMax(i, HID_USAGES.HID_USAGE_X, ref maxval);
                    User u = new User();
                    u.name = "User" + k++;
                    u.ID = i;
                    u.joystick = joy;
                    u.linkName = "未连接";
                    u.connected = false;
                    u.name = string.Format("vjoy device {0}", i);
                    users.Add(u);
                    ComboBox_Target.Items.Add(u.name);
                    userCount++;
                }
            }
            maxval /= 2;
            ComboBox_Target.SelectedIndex = 0;
        }

        private void ComboBox_Target_SelectedIndexChanged(object sender, EventArgs e)
        {
            label_linkState.Text = "连接状态:" + users[ComboBox_Target.SelectedIndex].linkName;
        }

        public string GetIPAddress()
        {
            foreach (IPAddress i in Dns.GetHostEntry(Dns.GetHostName()).AddressList)
            {
                if (i.AddressFamily == AddressFamily.InterNetwork)
                    return i.ToString();
            }
            return "";
        }

        void AddListen(int port, IPAddress ip)
        {
            server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            server.Bind(new IPEndPoint(ip, port));
            server.Listen(16);
            Thread thread = new Thread(ListenClientCount);
            thread.IsBackground = true;
            thread.Start();
        }

        void ListenClientCount()
        {
            while (true)
            {
                Socket client = server.Accept();
                client.SetSocketOption(SocketOptionLevel.Tcp, SocketOptionName.NoDelay, true);
                byte[] buffer = new byte[1024];
                int recLen = client.Receive(buffer);
                int i = 0;
                for (i = 0; i < users.Count; i++)
                {
                    if (users[i].connected)
                        continue;
                    User u = users[i];
                    u.linkName = Encoding.UTF8.GetString(buffer, 0, recLen);
                    u.connected = true;
                    users[i] = u;
                    client.Send(Encoding.UTF8.GetBytes("SUCCEED|" + u.name));

                    if (ComboBox_Target.SelectedIndex == i)
                        label_linkState.Text = "连接状态：" + users[ComboBox_Target.SelectedIndex].linkName;
                    ComboBox_Target.SelectedIndex = i;

                    Thread thread = new Thread(ReceiveClient);
                    Receive r = new Receive(client, i, thread);
                    clients.Add(r);
                    thread.Start(r);
                    break;
                }
                client.Send(Encoding.UTF8.GetBytes("FAIL|连接已满"));
            }

            void ReceiveClient(Object o)
            {
                while (true)
                {
                    try
                    {
                        Receive rec = o as Receive;
                        byte[] buffer = new byte[1024];
                        int recLen = rec.client.Receive(buffer);
                        rec.result = Encoding.UTF8.GetString(buffer, 0, recLen);
                        foreach (string s in rec.result.Split('+'))
                        {
                            if (s == "0")
                                rec.time = 6;
                           else if (s != "")
                                SetOption(s, rec.num);
                        }
                    }
                    catch
                    {

                    }
                }
            }

            void SetOption(string re, int i)
            {
                User u = users[i];
                string[] a = re.Split('|');
                try
                {
                    switch (a[0])
                    {
                        case "JOYSTICK":
                            HID_USAGES h1 = HID_USAGES.HID_USAGE_X
                                , h2 = HID_USAGES.HID_USAGE_Y;
                            int x = (int)(float.Parse(a[2]) * maxval + maxval), y = (int)(-float.Parse(a[3]) * maxval + maxval);
                            if (a[1] == "RIGHT")
                            {
                                h1 = HID_USAGES.HID_USAGE_RX;
                                h2 = HID_USAGES.HID_USAGE_RY;
                            }
                            u.joystick.SetAxis(x, u.ID, h1);
                            u.joystick.SetAxis(y, u.ID, h2);
                            break;

                        case "POV":
                            //0左1右2上3下
                            u.joystick.SetDiscPov(int.Parse(a[1]), u.ID, 1);
                            break;
                        case "ABXY":
                            u.joystick.SetBtn(a[2] == "False" ? false : true, u.ID, dict[a[1]]);
                            u.joystick.SetBtn(a[4] == "False" ? false : true, u.ID, dict[a[3]]);
                            u.joystick.SetBtn(a[6] == "False" ? false : true, u.ID, dict[a[5]]);
                            u.joystick.SetBtn(a[8] == "False" ? false : true, u.ID, dict[a[7]]);
                            break;
                        case "OTHER":
                            u.joystick.SetBtn(a[2] == "False" ? false : true, u.ID, dict[a[1]]);
                            break;
                    }
                }
                catch
                {

                }
            }
        }

        private void ExistToolStripMenuItem_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < clients.Count; i++)
            {
                clients[i].client.Shutdown(SocketShutdown.Both);
                //clients[i].client.Close(1000);
            }
            System.Environment.Exit(0);
        }

        private void ShowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowInTaskbar = !ShowInTaskbar;
            Visible = ShowInTaskbar;
        }
    }
}
