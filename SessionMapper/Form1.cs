using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Net.NetworkInformation;



namespace SessionMapper
{
    public partial class Form1 : Form
    {

        const string Version = "1.0";
        const string Title = "Session Map v" + Version;
        string nl = Environment.NewLine;
        bool running = false;
        const string StopMessage = "Stop Displaying Sessions";
        const string StartMessage = "Start Displaying Sessions";
        ArrayList mappoints;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            bstart.Text = StartMessage;
            this.Text = Title;
        }

        private void bstart_Click(object sender, EventArgs e)
        {
            if (running == false)
            {
                running = true;
                bstart.Text = StopMessage;
                Record();
            }
            else
            {
                running = false;
                bstart.Text = StartMessage;
            }

        }
        private void ClearAll()
        {

            rtb.Clear();
            rtbUl.Clear();
            rtbTl.Clear();
        }
        private void InvalidateAll()
        {
            rtb.Invalidate();
            rtbUl.Invalidate();
            rtbTl.Invalidate();
        }
        private void Record()
        {
            ClearAll();
            IPGlobalProperties ipProperties = IPGlobalProperties.GetIPGlobalProperties();
            IPEndPoint[] endPointsTcp = ipProperties.GetActiveTcpListeners();
            IPEndPoint[] endPointsUdp = ipProperties.GetActiveUdpListeners();
            TcpConnectionInformation[] tcpConnections = ipProperties.GetActiveTcpConnections();
            UdpStatistics Ustats = ipProperties.GetUdpIPv4Statistics();
            TcpStatistics Tstats = ipProperties.GetTcpIPv4Statistics();
            //// ACTIVE CONNECTIONS
            //// ACTIVE CONNECTIONS
            //// ACTIVE CONNECTIONS
            rtb.AppendText("== Active Connections ==" + nl);
            string remoteip = "";
            string localip = "";
            string localport = "";
            string remoteport = "";
            string TcpState1 = "";
            location loc = new location();
            int tcplist = 0;
            int udplist = 0;
            int tcpconnlist = 0;
            int maplist=0;
            Boolean mapable = false;
            point pt;
            foreach (TcpConnectionInformation info in tcpConnections)
            {
                localip = info.LocalEndPoint.Address.ToString();
                localport = info.LocalEndPoint.Port.ToString();
                remoteip = info.RemoteEndPoint.Address.ToString();
                remoteport = info.RemoteEndPoint.Port.ToString();
                TcpState1 = info.State.ToString();
                loc = GetLocation(remoteip);
                mapable = loc.success;
                if (mapable)
                {
                    maplist+=1;
                    pt=new point(loc.latitude,loc.longitude);
                    mappoints.Add(pt);
                }
                rtb.AppendText(localip + ":" + localport + " ==> " + remoteip + ":" + remoteport + "  " + TcpState1 + " (" + mapable + "," + loc.longitude + "," + loc.latitude + ")" + nl);
                tcpconnlist += 1;
                InvalidateAll();

            }
            //// TCP LISTENERS
            //// TCP LISTENERS
            //// TCP LISTENERS
            string ip2 = "";
            string port2 = "";
            rtbTl.AppendText("== TCP Listeners ==" + nl);
            foreach (IPEndPoint info in endPointsTcp)
            {
                ip2 = info.Address.ToString();
                if (ip2.Contains(":"))
                { continue; }
                port2 = info.Port.ToString();
                rtbTl.AppendText(ip2 + ":" + port2 + nl);
                tcplist += 1;
            }
            //// UDP LISTENERS
            //// UDP LISTENERS
            //// UDP LISTENERS
            string ip3 = "";
            string port3 = "";
            rtbUl.AppendText("== UDP Listeners ==" + nl);
            foreach (IPEndPoint info in endPointsUdp)
            {
                ip3 = info.Address.ToString();
                if (ip3.Contains(":"))
                { continue; }
                port3 = info.Port.ToString();
                rtbUl.AppendText(ip3 + ":" + port3 + nl);
                udplist += 1;
            }
            lstats.Text = "TCP connections = " + tcpconnlist + " IPv4 || UDP listeners = " + udplist + " IPv4 / " + Ustats.UdpListeners.ToString() + " IPv4&&6 || TCP listeners = " + tcplist + " IPv4 / " + Tstats.CurrentConnections.ToString() + " IPv4&&6";
            InvalidateAll();
        }

         private void bb_Click(object sender, EventArgs e)
        {
            rrtb.Clear();
            string ip = tip.Text;
            location tmp;
           tmp= GetLocation(ip);
            string strres = "(" + tmp.success + "," + tmp.latitude + "," + tmp.longitude + ")";
            rrtb.AppendText(strres);

        }
        public location GetLocation(string strIP)
        {
            location temp1 = new location();
           
           try
            {
               string apikey = "a375f9c927c2ffa58c56e47cb6d9890eaefa74412271fbd72e1f7fcb3fb8b549"; // had to sign up for free account to get this api key
               string path = "http://api.ipinfodb.com/v3/ip-city/?key=" + apikey + "&ip=" + strIP;
               WebClient client = new WebClient();
               string eResult = client.DownloadString(path).ToString();
               char delimiter= ';';
               string[] splits = eResult.Split(delimiter);
               temp1.success = true;
               temp1.latitude=Convert.ToDouble(splits[8]);
               temp1.longitude=Convert.ToDouble(splits[9]);
            }
            catch
            {
                temp1.success = false;
                return temp1;
            }
           if (temp1.latitude == 0 && temp1.longitude == 0) { temp1.success = false; }
            return temp1;
        }
    }
    public class location
    {
        public bool success = false;
        public double latitude = 0;
        public double longitude = 0;
    }
    public class point
    {
        public double latitude = 0;
        public double longitude = 0;
        public point(double latitudenum, double longitudenum) { latitude = latitudenum; longitude = longitudenum; }
    }
  }


