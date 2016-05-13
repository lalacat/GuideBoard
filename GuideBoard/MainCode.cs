using System;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Timer = System.Timers.Timer;

namespace GuideBoard
{
    public partial class MainWindow : Form
    {
        private readonly Timer _revMessageDealTimer;
        private readonly Timer _reConnectTimer;
        private IPEndPoint _ipp;
        private Socket _clientSocket;
        private byte[] _revMessage = new byte[10240];
        private readonly string STX = "$$";
        private static readonly ManualResetEvent NetWorkAllDone = new ManualResetEvent(false);
        private static readonly ManualResetEvent GetReConnect=new ManualResetEvent(false);
        private XML_Window startWindow;
        private Thread td;

        public MainWindow()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
            NetWork();

            _revMessageDealTimer = new Timer(interval: 50);
            _revMessageDealTimer.Elapsed += _revMessageDealTimer_Elapsed;
        }



        private void getConnect_Click(object sender, EventArgs e)
        {
            try
            {
                _clientSocket.BeginConnect(_ipp, new AsyncCallback(GetConnect), _clientSocket);
            }
            catch
            {
            }

        }
        private class RequestState
        {
            public Socket WorkSocket = null;
            public const int BufferSize = 1024;
            public readonly byte[] Buffer = new byte[BufferSize];
        }

        private void NetWork()
        {
            try
            {
                _ipp = new IPEndPoint(IPAddress.Parse(IpAddress.Text), int.Parse(PortValue.Text));
                _clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                Thread getconnecThread = new Thread(new ThreadStart(GetConnectThread)){IsBackground = true};
                getconnecThread.Start();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void GetConnectThread()
        {
            while (true)
            {
                GetReConnect.Reset();
               _clientSocket.BeginConnect(_ipp, new AsyncCallback(GetConnect), _clientSocket);

                GetReConnect.WaitOne();
            }
        }
        private void GetConnect(IAsyncResult ar)
        {
            try
            {
                Socket handler = (Socket) ar.AsyncState;
                handler.EndConnect(ar);
                RequestState state = new RequestState();
                state.WorkSocket = handler;
                state.WorkSocket.BeginReceive(state.Buffer, 0, RequestState.BufferSize, 0,
                    new AsyncCallback(ReadCallBack), state);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                if (_clientSocket.Connected)
                {

                    _clientSocket.Disconnect(true);
                }
                GetReConnect.Set();
            }
        }

        private int _revlength = 0;
        
        private void ReadCallBack(IAsyncResult ar)
        {                
            _revMessageDealTimer.Enabled = false;
            RequestState state = (RequestState)ar.AsyncState;
            try
            {
                if (_revMessage == null)
                    _revMessage = new byte[10240];
                Socket handler = state.WorkSocket;
                int byteRead = handler.EndReceive(ar);
                string str = Encoding.GetEncoding("gb2312").GetString(state.Buffer, 0,5);
                if ( str!= "<EOF>")
                {
                    for (int i = 0; i < byteRead; i++)
                    {
                        _revMessage[_revlength + i] = state.Buffer[i];
                    }
                    _revlength += byteRead;
                    _revMessageDealTimer.Enabled = true;
                }
                else
                {
                    Send("Connecting.............\n\r");
                }

                state.WorkSocket.BeginReceive(state.Buffer, 0, RequestState.BufferSize, 0,
                    new AsyncCallback(ReadCallBack), state);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                _clientSocket.Disconnect(true);
                GetReConnect.Set();
            }            
        }
        private void Send(String data)
        {
            try
            {
                // Convert the string data to byte data using ASCII encoding.
                byte[] byteData = Encoding.GetEncoding("gb2312").GetBytes(data);

                // Begin sending the data to the remote device.
                _clientSocket.BeginSend(byteData, 0, byteData.Length, 0,
                    new AsyncCallback(SendCallback), _clientSocket);

            }
            catch (Exception ex)
            {
;
                //handler.Dispose();
                //_beConnectedFlag = false;
            }
        }

        private void SendCallback(IAsyncResult ar)
        {
            try
            {
                // Retrieve the socket from the state object.
                Socket handler = (Socket)ar.AsyncState;
                // Complete sending the data to the remote device.
                int bytesSent = handler.EndSend(ar);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public string context;
        private void _revMessageDealTimer_Elapsed(object sender, EventArgs e)
        { 
            _revMessageDealTimer.Enabled = false;


            if (_revMessage != null)
            {

                context = Encoding.GetEncoding("gb2312").GetString(_revMessage);             
                revMessage.Text = context;
                startWindow=new XML_Window(context);
                          
                td=new Thread(new ThreadStart(OpenWindow)){IsBackground = true};   

           //     if (!td.IsAlive)
           //     {
               //     td.Start();
            //    }             
            
                
                try
                {
                    XmlDealClass myXml = new XmlDealClass(context);
                    ContextInfromation[] ciTemp = myXml.GetInfromations;
                    foreach (ContextInfromation cit in ciTemp)
                    {
                        string str = null;
                        Console.Write("ID:" + cit.ID + "\tcommand is " + cit.Command + "\n\r");
                        if (cit.Command == 1)
                        {
                            continue;
                        }
                        else if (2 <= cit.Command && cit.Command <= 10)
                        {
                            if (cit.Order != null)
                            {
                                Console.Write("Order is " + cit.Order + "\tDegree is " + cit.Degree+"\n\r");
                                Console.Write(cit.Details.Aggregate(str, (current, dt) => current + ("Color is " + dt.Color + "\tFormat is " + dt.Format + "\tData is " + dt.Data + "\n\r")));

                            }
                            else if(cit.Direction!=null)
                            {
                                 Console.Write("Drection is " + cit.Direction + "\tDegree is " + cit.Degree + "\n\r");
                                 Console.Write(cit.Details.Aggregate(str, (current, dt) => current + ("Color is " + dt.Color + "\tFormat is " + dt.Format+"\tData is " + dt.Data + "\n\r")));
                            }
                            else if (cit.Degree != null)
                            {
                                Console.Write("Degree is " + cit.Degree + "\n\r");
                                Console.Write(cit.Details.Aggregate(str, (current, dt) => current + ("Color is " + dt.Color + "\tFormat is " + dt.Format + "\tData is " + dt.Data + "\n\r")));
                            }
                            else if (cit.Details != null)
                            {
                                Console.Write(cit.Details.Aggregate(str, (current, dt) => current + ("Color is " + dt.Color + "\tFormat is " + dt.Format + "\tData is " + dt.Data + "\n\r")));
                            }
                            else if(cit.Context!=null)
                            {
                                 Console.WriteLine( "Context is" + cit.Context);
                            }
                        }
 
                    }                       

                }
                catch (Exception ex)
                {               
                    Console.WriteLine(ex.Message);

                }
                _revlength = 0;  
                _revMessage = null;
            }
                context = null;

        }

        private void OpenWindow()
        {
            Application.Run(startWindow);
        }
    }
}

