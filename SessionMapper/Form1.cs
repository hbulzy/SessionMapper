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
using GMap.NET;
using GMap.NET.MapProviders;
using GMap.NET.WindowsForms;
using GMap.NET.WindowsForms.Markers;
using GMap.NET.WindowsForms.ToolTips;


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
        ArrayList mappoints = new ArrayList();
        string myIP;
        point myIPpt = new point(0,0);

       
        GMapOverlay remotes;
       
        GMapOverlay Self;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            tip.Text = "8.8.8.8";
            bstart.Text = StartMessage;
            this.Text = Title;
            updatemyip();
            
        }
        private void updatemyip() 
        {
            // get current ip
            WebClient client1 = new WebClient();
            string eResult1 = client1.DownloadString("http://www.icanhazip.com").ToString();
            myIP = eResult1;
            location temp1 = GetLocation(myIP);
            myIPpt.latitude = temp1.latitude;
            myIPpt.longitude = temp1.longitude;
            //red
            GMapOverlay Self = new GMapOverlay(gmap, "Self");
            GMapMarker selfmark = new GMapMarkerGoogleRed(new PointLatLng(myIPpt.latitude, myIPpt.longitude));
            selfmark.ToolTipMode = MarkerTooltipMode.Always;
            selfmark.ToolTipText = "Local";
            Self.Markers.Add(selfmark);
            gmap.Overlays.Add(Self);
        }
        private void bstart_Click(object sender, EventArgs e)
        {
            if (running == false)
            {
                updatemyip();
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
        private void configmap() {
         
           // gmap.SetCurrentPositionByKeywords("United States");
            gmap.MinZoom = 1;
            gmap.MaxZoom = 17;
            gmap.Zoom = 1;
            gmap.MapProvider = GMapProviders.GoogleMap;
            gmap.Manager.Mode = AccessMode.ServerOnly;
            GMapProvider.WebProxy = null;
            gmap.Position = new PointLatLng(0, 0);
       
            Size siz = new System.Drawing.Size(gmap.Width, gmap.Height);
            gmap.ClientSize = siz;
         

       
        
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
            string toolstr="";
            foreach (TcpConnectionInformation info in tcpConnections)
            {
                localip = info.LocalEndPoint.Address.ToString();
                localport = info.LocalEndPoint.Port.ToString();
                remoteip = info.RemoteEndPoint.Address.ToString();
                remoteport = info.RemoteEndPoint.Port.ToString();
                TcpState1 = info.State.ToString();
                loc = GetLocation(remoteip);
                mapable = loc.success;
                rtb.AppendText("* " + localip + ":" + localport + " => " + remoteip + ":" + remoteport + nl + "    " + TcpState1 + " (" + mapable + "," + loc.longitude + "," + loc.latitude + ")");
                if (mapable)
                {
                    maplist+=1;
                    pt=new point(loc.latitude,loc.longitude);
                    mappoints.Add(pt);
                    rtb.AppendText(" Point: " + maplist);
                    toolstr = "Remote: " + maplist +nl + "(" +localip + ":" + localport + " => " + remoteip + ":" + remoteport + "  " + TcpState1+ ")";
                    SetMarker(toolstr , loc.latitude, loc.longitude);
                }
                rtb.AppendText( nl);
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
            SetMarker("TEST", tmp.latitude, tmp.longitude);

        }
         public void SetMarker(string markertext, double lat1, double long1) 
         {
             //green
             GMapMarker myCity = new GMapMarkerGoogleGreen(new PointLatLng(lat1, long1));
             myCity.ToolTipMode = MarkerTooltipMode.Always;
             myCity.ToolTipText = markertext;
             remotes = new GMapOverlay(gmap, "remotes");
             remotes.Markers.Add(myCity);
             gmap.Overlays.Add(remotes);
             gmap.Refresh();

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

        private void Form1_Shown(object sender, EventArgs e)
        {
            configmap();
        }

        private void Form1_Click(object sender, EventArgs e)
        {
           
         
        }

        private void rtb_TextChanged(object sender, EventArgs e)
        {

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


