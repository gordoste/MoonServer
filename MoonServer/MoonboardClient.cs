using MoonServer.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace MoonServer
{
    public class MoonboardClient
    {
        // Client for a single panel (bottom/middle/top)
        public class PanelClient
        {
            private readonly IPAddress Address;
            private readonly int Port;
            private Socket clientSock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            private readonly TextWriter Log;
            private int CmdId = 1; // Commands are sent with sequential command ID
            private readonly Decoder dec = Encoding.ASCII.GetDecoder();
            private List<string> rcvdMsgList = new List<string>();
            private string rcvdBuf = "";

            public bool Debug { get; set; }

            public static PanelClient FromConfig(string addrAndPort, TextWriter log)
            {
                string[] parts = addrAndPort.Split(':'); // Expect IP:port
                return new PanelClient(IPAddress.Parse(parts[0]), Int32.Parse(parts[1]), log);
            }

            public PanelClient(IPAddress address, int port) :
                this(address, port, new DebugWriter())
            { }

            public PanelClient(string ipAddress, int port, Stream log) :
                this(IPAddress.Parse(ipAddress), port, new StreamWriter(log))
            { }

            public PanelClient(string ipAddress, int port, TextWriter log) :
                this(IPAddress.Parse(ipAddress), port, log)
            { }

            public PanelClient(IPAddress address, int port, TextWriter log)
            {
                Address = address;
                Port = port;
                Log = log;
                Debug = false;
            }

            public void LightHolds(IEnumerable<string> holds)
            {
                SendCommand("SET", string.Join(" ", holds));
            }

            public void ClearBoard()
            {
                SendCommand("CLR");
            }

            private void SendCommand(string command, string data = null)
            {
                string cmd = string.Format("{0} {1}", command, CmdId);
                if (data != null)
                {
                    cmd += (" " + data);
                }
                Log.WriteLine(string.Format("Sending command '{0}'", cmd));
                int totalBytesSent = 0;
                byte[] sendBuffer = Encoding.ASCII.GetBytes(cmd + "\r\n");
                try
                {
                    while (totalBytesSent < sendBuffer.Length)
                    {
                        totalBytesSent += clientSock.Send(sendBuffer, totalBytesSent, sendBuffer.Length - totalBytesSent, SocketFlags.None);
                    }
                }
                catch (SocketException se)
                {
                    if (se.SocketErrorCode == SocketError.TimedOut)
                    {
                        throw new MoonboardClientException("SendCommand(): Timed out");
                    }
                    if (se.SocketErrorCode == SocketError.NotConnected)
                    {
                        throw new MoonboardClientException("SendCommand(): Socket not connected");
                    }
                    if (se.SocketErrorCode == SocketError.ConnectionAborted)
                    {
                        throw new MoonboardClientException("SendCommand(): Connection aborted");
                    }
                    if (se.SocketErrorCode == SocketError.Interrupted)
                    {
                        throw new MoonboardClientException("SendCommand(): Command interrupted");
                    }
                }
                WaitForAck(CmdId);
                CmdId++;
            }

            private void WaitForAck(int cmdId)
            {
                string expected = string.Format("ACK {0}", cmdId);
                string rcvd;
                while (true)
                {
                    ReceiveLines();
                    if (rcvdMsgList.Count == 0) { throw new MoonboardClientException("WaitForAck(): No response received"); }
                    while (rcvdMsgList.Count > 0)
                    {
                        rcvd = rcvdMsgList[0];
                        rcvdMsgList.RemoveAt(0);
                        if (rcvd.Equals(expected)) { return; }
                        throw new MoonboardClientException(string.Format("Expected '{0}', got '{1}'", expected, rcvd));
                    }
                }
            }

            private void ReceiveLines()
            {
                char[] chars;
                int bytesRcvd = 0;
                byte[] rcvBuffer = new byte[256];
                while (rcvdMsgList.Count == 0)
                {
                    try
                    {
                        bytesRcvd = clientSock.Receive(rcvBuffer, 0, rcvBuffer.Length, SocketFlags.None);
                    }
                    catch (SocketException se)
                    {
                        if (se.SocketErrorCode == SocketError.TimedOut)
                        {
                            throw new MoonboardClientException("ReceiveLines(): Timed out");
                        }
                        if (se.SocketErrorCode == SocketError.NotConnected)
                        {
                            throw new MoonboardClientException("ReceiveLines(): Socket not connected");
                        }
                        if (se.SocketErrorCode == SocketError.ConnectionAborted)
                        {
                            throw new MoonboardClientException("ReceiveLines(): Connection aborted");
                        }
                        if (se.SocketErrorCode == SocketError.Interrupted)
                        {
                            bytesRcvd = 0;
                        }
                        throw se;
                    }
                    catch (ObjectDisposedException)
                    {
                        throw new MoonboardClientException("ReceiveLines(): Socket disposed while receiving");
                    }
                    if (bytesRcvd > 0)
                    {
                        chars = new char[bytesRcvd];
                        dec.GetChars(rcvBuffer, 0, bytesRcvd, chars, 0);
                        rcvdBuf += new string(chars);
                    }
                    int i;
                    while ((i = rcvdBuf.IndexOf("\r\n")) != -1)
                    {
                        string rcvdStr = rcvdBuf.Substring(0, i);
                        rcvdMsgList.Add(rcvdStr);
                        Log.WriteLine(string.Format("Received: {0}", rcvdStr));
                        rcvdBuf = rcvdBuf.Substring(i + 2);
                    }
                }
            }

            public void OpenConnection()
            {
                clientSock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                clientSock.Connect(Address, Port);
                clientSock.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReceiveTimeout, 10000);
                rcvdMsgList = new List<string>();
                rcvdBuf = "";
                Log.WriteLine("Connection opened");
            }

            public void CloseConnection()
            {
                if (clientSock != null && clientSock.Connected) { clientSock.Close(); }
                clientSock = null;
                Log.WriteLine("Connection closed");
            }
        }

        private readonly MoonServerDB Database;
        private readonly TextWriter Log;
        private PanelClient topPanel;
        private PanelClient midPanel;
        private PanelClient btmPanel;

        public bool Debug { get; set; }

        public MoonboardClient(MoonServerDB database) :
            this(database, new DebugWriter())
        { }

        public MoonboardClient(MoonServerDB database, Stream log) :
            this(database, new StreamWriter(log))
        { }

        public MoonboardClient(MoonServerDB database, TextWriter log)
        {
            Database = database;
            Log = log;
            Debug = false;
            btmPanel = PanelClient.FromConfig(Constants.GetFileConfig("BottomPanel"), log);
            midPanel = PanelClient.FromConfig(Constants.GetFileConfig("MiddlePanel"), log);
            topPanel = PanelClient.FromConfig(Constants.GetFileConfig("TopPanel"), log);
        }

        public void ShowProblem(int id)
        {
            Problem prb = Database.Problems.First(p => p.Id == id);
            PositionStrings ps = new PositionStrings(prb);
            try
            {
                btmPanel.OpenConnection();
                midPanel.OpenConnection();
                topPanel.OpenConnection();
            }
            catch (SocketException se)
            {
                throw new MoonboardClientException(string.Format("{0}:{1}", se.SocketErrorCode, se.Message));
            }
            try
            {
                btmPanel.ClearBoard();
                midPanel.ClearBoard();
                topPanel.ClearBoard();
                btmPanel.LightHolds(ps.Bottom);
                midPanel.LightHolds(ps.Middle);
                topPanel.LightHolds(ps.Top);
            }
            catch (MoonboardClientException mbe)
            {
                btmPanel.CloseConnection();
                midPanel.CloseConnection();
                topPanel.CloseConnection();
                throw mbe;
            }
            btmPanel.CloseConnection();
            midPanel.CloseConnection();
            topPanel.CloseConnection();
        }

    }

    public class DebugWriter : TextWriter
    {
        public override void Write(char value)
        {
            Debug.Write(value.ToString()); // When character data is written, append it to the text box.
        }

        public override Encoding Encoding
        {
            get { return System.Text.Encoding.UTF8; }
        }
    }

    public class MoonboardClientException :Exception
    {
        public MoonboardClientException() : base() { }
        public MoonboardClientException(string message) : base(message) { }
    }
}