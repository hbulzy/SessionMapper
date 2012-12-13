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
        const string UpdateDate = "12/12/2012";
        const string Title = "Session Map v" + Version +" - " + UpdateDate + " - by: kossboss";
        string nl = Environment.NewLine;
       // bool running = false;
        const string StopMessage = "Stop Displaying Sessions";
        const string StartMessage = "Start Displaying Sessions";
        string myIP;
        location myIPpt = new location();
        ArrayList locs;
        GMapOverlay remotes;
        GMapOverlay Self;
        public Form1()
        {
            InitializeComponent();
        }
        public void setuplv() 
        {
            lv.View = View.Details;
            lv.GridLines = true;
            lv.FullRowSelect = true;
            lv.Columns.Add("id",20);
            lv.Columns.Add("Local IP",60);
            lv.Columns.Add("Local Port", 70);
            lv.Columns.Add("Remote IP", 70);
            lv.Columns.Add("Remote Port", 80);
            lv.Columns.Add("TCP State", 65);
            lv.Columns.Add("TCP/UDP", 70);
            lv.Columns.Add("Listener", 50);
            lv.Columns.Add("Map Pt.", 50);
            lv.Columns.Add("City", 40);
            lv.Columns.Add("State", 40);
            lv.Columns.Add("Country", 50);
            lv.Columns.Add("Zip", 40);

        }
        public void working()
        {
            Cursor.Current = Cursors.WaitCursor;
            
           
            this.Text = Title + " - WORKING PLEASE WAIT";
        }
        public void doneworking()
        {
            Cursor.Current = Cursors.Default;
          
            
            this.Text = Title;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            working();
            setuplv();
            tip.Text = "8.8.8.8";
           // bstart.Text = StartMessage;
            bstart.Text = "Gather Network Info";
            this.Text = Title;
            updatemyip();
        }
        private void updatemyip()
        {
            WebClient client1 = new WebClient();
            string eResult1 = client1.DownloadString("http://www.icanhazip.com").ToString();
            myIP = eResult1;
            myIPpt.self = true;
            myIPpt.localip = myIP;
            myIPpt.UpdateLocation();
            Self = new GMapOverlay(gmap, "Self");
            GMapMarker selfmark = new GMapMarkerGoogleRed(new PointLatLng(myIPpt.latitude, myIPpt.longitude));
            selfmark.ToolTipMode = MarkerTooltipMode.Always;
            selfmark.ToolTipText = "Local: " + myIP;
            Self.Markers.Add(selfmark);
            gmap.Overlays.Add(Self);
        }
        private void bstart_Click(object sender, EventArgs e)
        {
        //    if (running == false)
         //   {
           //     running = true;
            //    bstart.Text = StopMessage;
                updatemyip();
                Record();
          //  }
          //  else
         //   {
          //      running = false;
         //       bstart.Text = StartMessage;
        //    }
        }
        private void configmap()
        {

            gmap.MinZoom = 1;
            gmap.MaxZoom = 17;
            gmap.Zoom = 1;
            gmap.MapProvider = GMapProviders.BingMap;
            gmap.Manager.Mode = AccessMode.ServerAndCache;
            //GMapProvider.WebProxy = null;
            gmap.Position = new PointLatLng(0, 0);
            Size siz = new System.Drawing.Size(gmap.Width, gmap.Height);
            gmap.ClientSize = siz;
        }
        private void ClearAll()
        {
            lv.Clear();
            setuplv();
            rrtb.Clear();
        }
        private void InvalidateAll()
        {
            lv.Invalidate();
            gmap.Invalidate();
            lv.Refresh();
        }
        private void Record()
        {
            working();
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
            location loc = new location();
            int tcplist = 0;
            int udplist = 0;
            int tcpconnlist = 0;
            int maplist = 0;
            string toolstr = "";
            string locbit = "";
            int id = 0;
            locs = new ArrayList();
            ListViewItem itm;
            int mapid = -1;
            string[] arr;
            foreach (TcpConnectionInformation info in tcpConnections)
            {
                mapid = -1;
                id += 1;
                loc = new location();
                tcpconnlist += 1;
                loc.id = id;
                loc.localip = info.LocalEndPoint.Address.ToString();
                loc.localport = info.LocalEndPoint.Port.ToString();
                loc.remoteip = info.RemoteEndPoint.Address.ToString();
                loc.remoteport = info.RemoteEndPoint.Port.ToString();
                loc.localip = info.LocalEndPoint.Address.ToString();
                loc.localport = info.LocalEndPoint.Port.ToString();
                loc.tcpstate = info.State.ToString();
                loc.islistener = false;
                loc.isudp = false;
                loc.UpdateLocation();
                if (loc.success)
                {
                    maplist += 1;
                    mapid = maplist;
                    loc.mapid = mapid;
                    locbit = "(" + loc.city + ", " + loc.state + ", " + loc.country + ", " + loc.zip + ")";
                    toolstr = "Remote: " + maplist + locbit + nl + " (" + loc.localip + ":" + loc.localport + " => " + loc.remoteip + ":" + loc.remoteport + "  " + loc.tcpstate + ")";
                    SetMarker(toolstr, loc.latitude, loc.longitude);
                }
                locs.Add(loc);
                InvalidateAll();
                arr = new string[13];
                arr[0] = Convert.ToString(id);
                arr[1] = Convert.ToString(loc.localip);
                arr[2] = Convert.ToString(loc.localport);
                arr[3] = Convert.ToString(loc.remoteip);
                arr[4] = Convert.ToString(loc.remoteport);
                arr[5] = Convert.ToString(loc.tcpstate);
                arr[6] = Convert.ToString("TCP");
                arr[7] = Convert.ToString("Conn.");
                //str for map id
                if (mapid == -1) { arr[8] = Convert.ToString(""); } else { arr[8] = Convert.ToString(loc.mapid); }
                arr[9] = Convert.ToString(loc.city);
                arr[10] = Convert.ToString(loc.state);
                arr[11] = Convert.ToString(loc.country);
                arr[12] = Convert.ToString(loc.zip);
                itm = new ListViewItem(arr);
                lv.Items.Add(itm);
                InvalidateAll();
            }
            //// TCP LISTENERS
            //// TCP LISTENERS
            //// TCP LISTENERS
            string ip2 = "";
            string port2 = "";
            foreach (IPEndPoint info in endPointsTcp)
            {
                id += 1;
                ip2 = info.Address.ToString();
                if (ip2.Contains(":"))
                { continue; }
                port2 = info.Port.ToString();
                loc = new location();
                loc.localip = ip2;
                loc.localport = port2;
                loc.remoteip = "0.0.0.0";
                loc.remoteport = "0";
                loc.success = false;
                loc.islistener = true;
                loc.isudp = false;
                locs.Add(loc);
                tcplist += 1;
                arr = new string[13];
                arr[0] = Convert.ToString(id);
                arr[1] = Convert.ToString(loc.localip);
                arr[2] = Convert.ToString(loc.localport);
                arr[3] = Convert.ToString(loc.remoteip);
                arr[4] = Convert.ToString(loc.remoteport);
                arr[5] = Convert.ToString(loc.tcpstate);
                arr[6] = Convert.ToString("TCP");
                arr[7] = Convert.ToString("Listener");
                arr[8] = Convert.ToString(""); 
                arr[9] = Convert.ToString(loc.city);
                arr[10] = Convert.ToString(loc.state);
                arr[11] = Convert.ToString(loc.country);
                arr[12] = Convert.ToString(loc.zip);
                itm = new ListViewItem(arr);
                lv.Items.Add(itm);
                InvalidateAll();
            }
            //// UDP LISTENERS
            //// UDP LISTENERS
            //// UDP LISTENERS
            string ip3 = "";
            string port3 = "";
            foreach (IPEndPoint info in endPointsUdp)
            {
                id += 1;
                ip3 = info.Address.ToString();
                if (ip3.Contains(":"))
                { continue; }
                port3 = info.Port.ToString();
                loc = new location();
                loc.localip = ip3;
                loc.localport = port3;
                loc.remoteip = "0.0.0.0";
                loc.remoteport = "0";
                loc.success = false;
                loc.islistener = true;
                loc.isudp = true;
                locs.Add(loc);
                udplist += 1;
                arr = new string[13];
                arr[0] = Convert.ToString(id);
                arr[1] = Convert.ToString(loc.localip);
                arr[2] = Convert.ToString(loc.localport);
                arr[3] = Convert.ToString(loc.remoteip);
                arr[4] = Convert.ToString(loc.remoteport);
                arr[5] = Convert.ToString(loc.tcpstate);
                arr[6] = Convert.ToString("UDP");
                arr[7] = Convert.ToString("Listener");
                arr[8] = Convert.ToString("");
                arr[9] = Convert.ToString(loc.city);
                arr[10] = Convert.ToString(loc.state);
                arr[11] = Convert.ToString(loc.country);
                arr[12] = Convert.ToString(loc.zip);
                itm = new ListViewItem(arr);
                lv.Items.Add(itm);
                InvalidateAll();
            }
            lstats.Text = "TCP connections = " + tcpconnlist + " IPv4 || UDP listeners = " + udplist + " IPv4 / " + Ustats.UdpListeners.ToString() + " IPv4&&6 || TCP listeners = " + tcplist + " IPv4 / " + Tstats.CurrentConnections.ToString() + " IPv4&&6";
            lv.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
            doneworking();
            
        }
        public void SetMarker(string markertext, double lat1, double long1)
        {
            //green
            GMapMarker somecity = new GMapMarkerGoogleGreen(new PointLatLng(lat1, long1));
            somecity.ToolTipMode = MarkerTooltipMode.Always;
            somecity.ToolTipText = markertext;
            remotes = new GMapOverlay(gmap, "remotes");
            remotes.Markers.Add(somecity);
            gmap.Overlays.Add(remotes);
            gmap.Refresh();
        }
        public void SetMarkerCross(string markertext, double lat1, double long1)
        {
            //green
            GMapMarker somecity = new GMapMarkerCross(new PointLatLng(lat1, long1));
            somecity.ToolTipMode = MarkerTooltipMode.Always;
            somecity.ToolTipText = markertext;
            remotes = new GMapOverlay(gmap, "remotes");
            remotes.Markers.Add(somecity);
            gmap.Overlays.Add(remotes);
            gmap.Refresh();
        }
        private void bb_Click(object sender, EventArgs e)
        {
            try
            {
                string locbit;
                string strres;
                string ip = tip.Text;
                rrtb.Clear();
                location tmp;
                tmp = TESTGetLocation(ip);
                locbit = "(" + tmp.city + ", " + tmp.state + ", " + tmp.country + ", " + tmp.zip + ")";
                strres = "(" + tmp.success + "," + tmp.latitude + "," + tmp.longitude + ")";
                rrtb.AppendText(strres);
                SetMarkerCross("TEST Point, IP: " + tip.Text + nl + strres + nl + locbit, tmp.latitude, tmp.longitude);
            }
            catch (Exception)
            {
                
                throw;
            }
          
        }
        public location TESTGetLocation(string strIP)
        {
            location temp1 = new location();
            try
            {
                string apikey = "a375f9c927c2ffa58c56e47cb6d9890eaefa74412271fbd72e1f7fcb3fb8b549"; // had to sign up for free account to get this api key
                string path = "http://api.ipinfodb.com/v3/ip-city/?key=" + apikey + "&ip=" + strIP;
                WebClient client = new WebClient();
                string eResult = client.DownloadString(path).ToString();
                char delimiter = ';';
                string[] splits = eResult.Split(delimiter);
                temp1.success = true;
                temp1.city = Convert.ToString(splits[6]);
                temp1.state = Convert.ToString(splits[5]);
                temp1.zip = Convert.ToString(splits[7]);
                temp1.country = Convert.ToString(splits[4]);
                temp1.latitude = Convert.ToDouble(splits[8]);
                temp1.longitude = Convert.ToDouble(splits[9]);
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
            doneworking();
        }
        private void Form1_Click(object sender, EventArgs e) { }
        private void rtb_TextChanged(object sender, EventArgs e) { }
        private void gmap_Load(object sender, EventArgs e)
        {
            configmap();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

       

    }
    public class location
    {
        public int id;
        public bool success = false; //got stuff
        public bool self = false;
        public bool isudp = false;
        public bool islistener = false;
        public string localip = "";
        public string remoteip = "";
        public string localport = "";
        public string remoteport = "";
        public string state = "";
        public double latitude = 0;
        public double longitude = 0;
        public string city = "";
        public string country = "";
        public string tcpstate = "";
        public string zip = "";
        public int mapid = -1;

        public void UpdateLocation()
        {
            try
            {
                string ipip;
                if (self == false) { ipip = this.remoteip; } else { ipip = this.localip; }
                string apikey = "a375f9c927c2ffa58c56e47cb6d9890eaefa74412271fbd72e1f7fcb3fb8b549"; // had to sign up for free account to get this api key
                string path = "http://api.ipinfodb.com/v3/ip-city/?key=" + apikey + "&ip=" + ipip;
                WebClient client = new WebClient();
                string eResult = client.DownloadString(path).ToString();
                char delimiter = ';';
                string[] splits = eResult.Split(delimiter);
                this.success = true;
                this.city = Convert.ToString(splits[6]);
                this.state = Convert.ToString(splits[5]);
                this.zip = Convert.ToString(splits[7]);
                this.country = Convert.ToString(splits[4]);
                this.latitude = Convert.ToDouble(splits[8]);
                this.longitude = Convert.ToDouble(splits[9]);
            }
            catch
            {
                this.success = false;

            }
            if (this.latitude == 0 && this.longitude == 0) { this.success = false; }

        }
    }
 }


