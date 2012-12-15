using System;
using System.Threading;
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
        const string SECRETKEY = "a375f9c927c2ffa58c56e47cb6d9890eaefa74412271fbd72e1f7fcb3fb8b549";
        const string Version = "1.0";
        const string UpdateDate = "12/12/2012";
        const string Title = "Session Map v" + Version + " - " + UpdateDate + " - by: kossboss";
        string nl = Environment.NewLine;
       // Object lk = new Object();

        // bool running = false;
        // const string StopMessage = "Stop Displaying Sessions";
        // const string StartMessage = "Start Displaying Sessions";

        // my ip stuff
        string myIP;
        location myIPpt = new location();

        // ArrayList locs;
        List<location> locs = new List<location>();
        List<int> ThreadIDs = new List<int>();
        List<int> ThreadMappedIds = new List<int>();
        bool canmapnow = false;
        bool Working = false;
      
        location loc = new location();
        int numTh = 0;
        int numNonPublic = 0;
        int idall = 0;
        int idtcp = 0;


        // google stuff
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
            lv.Columns.Add("id", 20);
            lv.Columns.Add("Local IP", 60);
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
            Working = true;
            Cursor.Current = Cursors.WaitCursor;

                this.Text = Title + " - WORKING PLEASE WAIT";
         
        }
        public void doneworking()
        {
            Working = false;
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
        private bool checkinternet()
        {
            //simple check via domain name of icanhazip
            try
            {
                WebClient client1 = new WebClient();
                string eResult1 = client1.DownloadString("http://www.icanhazip.com").ToString();
                return true;
            }
            catch (Exception)
            {
                MessageBox.Show("No Internet Connection Detected. Fix your Internet Connection and make sure DNS is functioning good.");
                return false;
            }
        }
        private void updatemyip()
        {
            try
            {
                WebClient client1 = new WebClient();
                string eResult1 = client1.DownloadString("http://www.icanhazip.com").ToString();
                myIP = eResult1;
                myIPpt.self = true;
                myIPpt.localip = myIP;
                myIPpt.UpdateLocation();
                rrtb.AppendText("My IP:" + myIP);
                mapmyip();
            }
            catch (Exception)
            {
                MessageBox.Show("No Internet Connection Detected. Fix your Internet Connection and make sure DNS is functioning good.");
            }

        }

        private void mapmyip()
        {
            Self = new GMapOverlay(gmap, "Self");

            GMapMarker selfmark = new GMapMarkerGoogleRed(new PointLatLng(myIPpt.latitude, myIPpt.longitude));
            selfmark.ToolTipMode = MarkerTooltipMode.Always;
            selfmark.ToolTipText = "Local: " + myIP;

            Self.Markers.Add(selfmark);
            gmap.Overlays.Add(Self);
        }
        private void bstart_Click(object sender, EventArgs e)
        {

            if (Working == false)
            {
              //  Thread th;
              //  th = new Thread(new ThreadStart(Record));
              //  th.Start();
                Record();
            }

            //    if (running == false)
            //   {
            //     running = true;
            //    bstart.Text = StopMessage;
           
           
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
            gmap.Overlays.Clear();
            rrtb.Clear();
        }
        private void InvalidateAll()
        {
            lv.Invalidate();
            gmap.Invalidate();
            lv.Refresh();
        }
      //  public void result()
      //  {
            /// lock (rtb) { rtb.AppendText("+"); }
            /// MessageBox.Show("+");

            //if (loc.success)
            //{
            //    maplist += 1;
            //    mapid = maplist;
            //    loc.mapid = mapid;
            //    locbit = "(" + loc.city + ", " + loc.state + ", " + loc.country + ", " + loc.zip + ")";
            //    toolstr = "Remote: " + maplist + locbit + nl + " (" + loc.localip + ":" + loc.localport + " => " + loc.remoteip + ":" + loc.remoteport + "  " + loc.tcpstate + ")";
            //    SetMarker(toolstr, loc.latitude, loc.longitude);
            //}
            //locs.Add(loc);
            //InvalidateAll();
            //arr = new string[13];
            //arr[0] = Convert.ToString(id);
            //arr[1] = Convert.ToString(loc.localip);
            //arr[2] = Convert.ToString(loc.localport);
            //arr[3] = Convert.ToString(loc.remoteip);
            //arr[4] = Convert.ToString(loc.remoteport);
            //arr[5] = Convert.ToString(loc.tcpstate);
            //arr[6] = Convert.ToString("TCP");
            //arr[7] = Convert.ToString("Conn.");
            ////str for map id
            //if (mapid == -1) { arr[8] = Convert.ToString(""); } else { arr[8] = Convert.ToString(loc.mapid); }
            //arr[9] = Convert.ToString(loc.city);
            //arr[10] = Convert.ToString(loc.state);
            //arr[11] = Convert.ToString(loc.country);
            //arr[12] = Convert.ToString(loc.zip);
            //itm = new ListViewItem(arr);
            //lv.Items.Add(itm);
            //InvalidateAll();


//        }


        public void test()
        {
           // string locbit;
            //string toolstr;
           

            //  foreach (location ho in locs) 
            // {
       //     for (int i = 0; i < locs.Count; i++)
         //   {


             string bit;
            rtb.AppendText("~~~~~~~~~~~~~~~~~~~~~~~~" + nl );
            int v;
             for (int i = 0; i < ThreadIDs.Count; i++){
                 v=ThreadIDs[i];
                if (locs[v].isDone == true) { bit = "#"; } else { bit = "X"; }
                rtb.AppendText(bit);
             }
               rtb.AppendText(nl+"~~~~~~~~~~~~~~~~~~~~~~~~" + nl );
               string info;
               for (int i = 0; i < ThreadIDs.Count; i++)
               {
                   v = ThreadIDs[i];
                   if (locs[v].success  == true) { bit = "(A)"; } else { bit = "(B)"; }
                   info = "(" + locs[v].remoteip + ")=(" + locs[v].latitude + "," + locs[v].longitude + ")";
                   rtb.AppendText(bit+ " "+info+nl);
               }


              //  if (locs[i].success == true) { bit = "X"; } else { bit = "o"; }
                // rtb.AppendText(toolstr);
                //    rtb.AppendText("$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$");
            

           // }
           // rtb.AppendText(nl + "~~~~~~~~~~~~~~~~~~~~~~~~" + nl);
         //   rtb.AppendText("]" + nl + "~~~~~~~~~~~~~~~~~~~~~~~~" + nl);
          //  rtb.AppendText("~~~~~~~~~~~~~~~~~~~~~~~~" + nl + "[");
            //  foreach (location ho in locs) 
            // {
          //  for (int i = 0; i < locs.Count; i++)
           // {
             //   if (locs[i].isDone == true) { bit = "_"; } else { bit = "#"; }
            //    if (locs[i].success == true) { bit = "X"; } else { bit = "o"; }
                // rtb.AppendText(toolstr);
                //    rtb.AppendText("$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$");
             //   rtb.AppendText(bit);

            //}
            //rtb.AppendText(nl + "~~~~~~~~~~~~~~~~~~~~~~~~" + nl);
          //  rtb.AppendText("]" + nl + "~~~~~~~~~~~~~~~~~~~~~~~~" + nl);
      //      for (int i = 0; i < locs.Count; i++)
       //     {

         //       rtb.AppendText(nl);
          //      locbit = "(" + locs[i].city + ", " + locs[i].state + ", " + locs[i].country + ", " + locs[i].zip + ")";
           //     toolstr = "**** " + locs[i].isDone + locbit + nl + "    (" + locs[i].localip + ":" + locs[i].localport + " => " + locs[i].remoteip + ":" + locs[i].remoteport + "  " + locs[i].tcpstate + ")";
                // rtb.AppendText(toolstr);
                //    rtb.AppendText("$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$");
             //   rtb.AppendText(toolstr);

            //}
            //}

        }
        public bool isPublicIP(string ip)
        {
            bool temp = true;
            string[] oct = ip.Split('.');
//rtb.AppendText("-----> " + ip + " ");
            if (Convert.ToInt16(oct[0]) == 10) // private if 10.x.x.x
            {
                //rtb.AppendText(" ~A~ ");
                temp = false; // private
            }
            else if (Convert.ToInt16(oct[0]) == 127) // loopbacks are private
            {
                //rtb.AppendText(" ~A~ ");
                temp = false; // private
            }
            else if (Convert.ToInt16(oct[0]) == 192) // private if 192.168.x.x
            {
                if (Convert.ToInt16(oct[1]) == 168)
                {
                    // rtb.AppendText(" ~B~ ");
                    temp = false;  // private
                }
                else
                {
                    // rtb.AppendText(" ~C~ ");
                    temp = true; // public
                }
            }
            else if (Convert.ToInt16(oct[0]) == 172)
            {
                if ((Convert.ToInt16(oct[1]) >= 16) && (Convert.ToInt16(oct[1]) < 32)) // private if 172.16.0.0 and 172.31.255.255
                {
                    //  rtb.AppendText(" ~D~ ");
                    temp = false;  // public
                }
                else
                {
                    //  rtb.AppendText(" ~E~ ");
                    temp = true;  // private
                }
            }

            return temp;
        }

        private void Record()
        {
           
                working();
                ClearAll();
                updatemyip();
                mapmyip();
                IPGlobalProperties ipProperties = IPGlobalProperties.GetIPGlobalProperties();
                IPEndPoint[] endPointsTcp = ipProperties.GetActiveTcpListeners();
                IPEndPoint[] endPointsUdp = ipProperties.GetActiveUdpListeners();
                TcpConnectionInformation[] tcpConnections = ipProperties.GetActiveTcpConnections();
                UdpStatistics Ustats = ipProperties.GetUdpIPv4Statistics();
                TcpStatistics Tstats = ipProperties.GetTcpIPv4Statistics();
                //// ACTIVE CONNECTIONS
                //// ACTIVE CONNECTIONS
                //// ACTIVE CONNECTIONS
                //location loc = new location();
                int tcplist = 0;
                int udplist = 0;
                int tcpconnlist = 0;
                // int maplist = 0;
                // string toolstr = "";
                /// string locbit = "";

                idall = 0;
                idtcp = 0;
                numTh = 0;
                numNonPublic = 0;
                locs = new List<location>();
                ListViewItem itm;
                //  int mapid;
                string[] arr;
                Thread th;
                // Thread th1;
                ThreadIDs.Clear();
                foreach (TcpConnectionInformation info in tcpConnections)
                {
                    //    mapid = -1;
                    idall += 1;
                    idtcp += 1;
                    loc = new location();
                    tcpconnlist += 1;
                    loc.id = idtcp;
                    loc.localip = info.LocalEndPoint.Address.ToString();
                    loc.localport = info.LocalEndPoint.Port.ToString();
                    loc.remoteip = info.RemoteEndPoint.Address.ToString();
                    loc.remoteport = info.RemoteEndPoint.Port.ToString();
                    loc.localip = info.LocalEndPoint.Address.ToString();
                    loc.localport = info.LocalEndPoint.Port.ToString();
                    loc.tcpstate = info.State.ToString();
                    loc.islistener = false;
                    loc.isudp = false;
                    loc.isDone = true;


                    if (isPublicIP(loc.remoteip))
                    {
                        // rtb.AppendText("PUBLIC: " + loc.remoteip);
                        ThreadIDs.Add(numTh); // before it so that its exactly the enumarator in the array since my map ids/numths start at 1 and arrays start at 0
                        numTh += 1;

                        loc.mapid = numTh;
                        loc.isDone = false;
                        th = new Thread(new ThreadStart(loc.UpdateLocation));
                        th.Start();
                        // mappable if success = true, map id not -1 and isdone = true
                        // if something has mapid but unsuccessful, means it was a public ip that didnt get a location
                    }
                    else
                    {
                        //  rtb.AppendText("PRIVATE: " + loc.remoteip);
                        numNonPublic += 1;
                        loc.success = false;
                        loc.city = "";
                        loc.state = "";
                        loc.zip = "";
                        loc.country = "";
                        loc.latitude = 0;
                        loc.longitude = 0;
                        arr = new string[13];
                        arr[0] = Convert.ToString(loc.id);
                        arr[1] = Convert.ToString(loc.localip);
                        arr[2] = Convert.ToString(loc.localport);
                        arr[3] = Convert.ToString(loc.remoteip);
                        arr[4] = Convert.ToString(loc.remoteport);
                        arr[5] = Convert.ToString(loc.tcpstate);
                        arr[6] = Convert.ToString("TCP");
                        arr[7] = Convert.ToString("Conn.");
                        //str for map id
                        arr[8] = "";
                        arr[9] = Convert.ToString(loc.city);
                        arr[10] = Convert.ToString(loc.state);
                        arr[11] = Convert.ToString(loc.country);
                        arr[12] = Convert.ToString(loc.zip);
                        itm = new ListViewItem(arr);
                        lv.Items.Add(itm);
                        InvalidateAll();
                    }

                    //  rtb.AppendText(nl);
                    locs.Add(loc);




                    //th = new Thread(new Thread( () => loc.UpdateLocation(rtb) ));

                    //;// work here thread this
                    //th1 = new Thread(new ThreadStart(result));
                    //th1.Start();
                    //
                    //if (loc.success)
                    //{
                    //    maplist += 1;
                    //    mapid = maplist;
                    //    loc.mapid = mapid;
                    //     locbit = "(" + loc.city + ", " + loc.state + ", " + loc.country + ", " + loc.zip + ")";
                    //    toolstr = "Remote: " + locbit + nl + " (" + loc.localip + ":" + loc.localport + " => " + loc.remoteip + ":" + loc.remoteport + "  " + loc.tcpstate + ")";
                    //   rtb.AppendText("@@@@@@@@@@@@@ " + toolstr + nl);
                    //    SetMarker(toolstr, loc.latitude, loc.longitude);
                    //}

                    //locs.Add((location)loc);


                    //InvalidateAll();
                    //arr = new string[13];
                    //arr[0] = Convert.ToString(id);
                    //arr[1] = Convert.ToString(loc.localip);
                    //arr[2] = Convert.ToString(loc.localport);
                    //arr[3] = Convert.ToString(loc.remoteip);
                    //arr[4] = Convert.ToString(loc.remoteport);
                    //arr[5] = Convert.ToString(loc.tcpstate);
                    //arr[6] = Convert.ToString("TCP");
                    //arr[7] = Convert.ToString("Conn.");
                    ////str for map id
                    //if (mapid == -1) { arr[8] = Convert.ToString(""); } else { arr[8] = Convert.ToString(loc.mapid); }
                    //arr[9] = Convert.ToString(loc.city);
                    //arr[10] = Convert.ToString(loc.state);
                    //arr[11] = Convert.ToString(loc.country);
                    //arr[12] = Convert.ToString(loc.zip);
                    //itm = new ListViewItem(arr);
                    //lv.Items.Add(itm);
                    //InvalidateAll();
                }

                // check threads
                CheckThreads();

                // end check threads


                ////while (true)
                ////{
                ////    rtb.AppendText("-");
                ////    foreach (location al in locs) 
                ////    {
                ////        string r="";
                ////        if (al.isDone){r="#";}else{r="_";}
                ////        rtb.AppendText(al.remoteip +"==="+al.latitude+"=" +                      r+"||");
                ////    //    if (al.isDone) { r = "x"; return; } else { r = "o"; }
                ////        rtb.ScrollToCaret();
                ////        rtb.Invalidate();

                ////    }
                ////    rtb.AppendText(nl);
                ////    Thread.Sleep(1000);
                ////}
                //update tcp listeners
                //// TCP LISTENERS
                //// TCP LISTENERS
                //// TCP LISTENERS
                string ip2 = "";
                string port2 = "";
                foreach (IPEndPoint info in endPointsTcp)
                {
                    idall += 1;
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
                    arr[0] = Convert.ToString(idall);
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
                    idall += 1;
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
                    arr[0] = Convert.ToString(idall);
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
        public void CheckThreads() {
            int v;
            int t ;
            bool runagain = true;
           // string tt;
          //  rtb.AppendText("###############");
            // when done t = thread count
           ThreadMappedIds.Clear();
           
            while (runagain==true) // inf loop till all are is done ( mappable if isdone true and success is true)
            {
                canmapnow = false;
                t =0;
               
                for (int i = 0; i < ThreadIDs.Count; i++)
                {
                    v = ThreadIDs[i];
                    if (locs[v].isDone == true) { t += 1;  }
                }
                
             //   rtb.AppendText(nl + "i:" + ThreadIDs.Count + "  t:" + t + nl);
                
              //  Thread.Sleep(100);
               // rtb.ScrollToCaret();
               // rtb.Invalidate();
                if (t == ThreadIDs.Count) 
                { // done





                    for (int i = 0; i < ThreadIDs.Count; i++)
                    {
                        v = ThreadIDs[i];
                   //     tt = "threadids = v: " + v + " (" + locs[v].latitude + "," + locs[v].longitude + ")";
                     //   rtb.AppendText(tt + nl);
                        if ((locs[v].isDone == true) && (locs[v].success == true)) { ThreadMappedIds.Add(v);  }
                    }
                    // update everything here
              //      rtb.AppendText("Done");
                    canmapnow = true;
                    donedone();
                    runagain = false; //leaves loop and method
                    return; // just incase
                    
                }

            }
             

        
        }
        public void donedone() 
        {
            int v;
           // string tt;
            string[] arr;
            ListViewItem itm;
            string locbit = "";
            string toolstr = "";
            if (canmapnow) 
            {
               // rtb.AppendText("---CAN MAP NOW---"+nl);
                for (int i = 0; i < ThreadMappedIds.Count; i++)
                {
                    
                    v = ThreadMappedIds[i];
                  //  tt = "threammappedid=v: " + v + " (" + locs[v].latitude + "," + locs[v].longitude + ")";
              //      rtb.AppendText(tt+nl);

                    arr = new string[13];
                    arr[0] = Convert.ToString(locs[v].id);
                    arr[1] = Convert.ToString(locs[v].localip);
                    arr[2] = Convert.ToString(locs[v].localport);
                    arr[3] = Convert.ToString(locs[v].remoteip);
                    arr[4] = Convert.ToString(locs[v].remoteport);
                    arr[5] = Convert.ToString(locs[v].tcpstate);
                    arr[6] = Convert.ToString("TCP");
                    arr[7] = Convert.ToString("Conn.");
                    //str for map id
                //    if (mapid == -1) { arr[8] = Convert.ToString(""); } else { arr[8] = Convert.ToString(loc.mapid); }
                    locs[v].mapid = i + 1; //remap id it
                    arr[8] = Convert.ToString(locs[v].mapid);
                    arr[9] = Convert.ToString(locs[v].city);
                    arr[10] = Convert.ToString(locs[v].state);
                    arr[11] = Convert.ToString(locs[v].country);
                    arr[12] = Convert.ToString(locs[v].zip);
                    itm = new ListViewItem(arr);
                    lv.Items.Add(itm);
                    //    loc.mapid = mapid;
                    locbit = " (" + locs[v].city + ", " + locs[v].state + ", " + locs[v].country + ", " + locs[v].zip + ")";
                    toolstr = "Remote: " + locbit + nl + " (" + locs[v].localip + ":" + locs[v].localport + " => " + locs[v].remoteip + ":" + locs[v].remoteport + "  " + locs[v].tcpstate + ")";
                    //   rtb.AppendText("@@@@@@@@@@@@@ " + toolstr + nl);
                    SetMarker(toolstr, locs[v].latitude, locs[v].longitude);
                    InvalidateAll();

                }
            
            }
            canmapnow = false;
        
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
            if (checkinternet() == false) { return; }
            try
            {
                string locbit;
                string strres;
                string ip = tip.Text.Trim();
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
        private void Form1_Click(object sender, EventArgs e) {
          
            
        }
        private void rtb_TextChanged(object sender, EventArgs e) { }
        private void gmap_Load(object sender, EventArgs e)
        {
            configmap();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            CheckThreads();
          //  test();
        }



    }

    public class location
    {
        private const string SECRETKEY = "a375f9c927c2ffa58c56e47cb6d9890eaefa74412271fbd72e1f7fcb3fb8b549";
        Object mLock = new Object();
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
        public int mapid = -1; // -1 = not map success = false,, 1 or above map id and success == true hopefully
        public bool isDone = false;
        public void UpdateLocation()
        {

            lock (mLock)
            {
                isDone = false;
                try
                {
                    string ipip;
                    if (self == false) { ipip = this.remoteip; } else { ipip = this.localip; }
                    string apikey = SECRETKEY; // had to sign up for free account to get this api key
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
                  //  mapid = -1;
                    this.success = false;


                }
                if (this.latitude == 0 && this.longitude == 0) { this.success = false; }
                isDone = true;

            }
        }
    }
}


