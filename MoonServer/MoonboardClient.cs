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
        private readonly IPAddress Address;
        private readonly int Port;
        private Socket clientSock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        private readonly MoonServerDB Database;
        private readonly TextWriter Log;
        private int CmdId = 1;
        private readonly Decoder dec = Encoding.ASCII.GetDecoder();
        private List<string> rcvdList = new List<string>();
        private string rcvdBuf = "";

        public bool Debug { get; set; }

        public MoonboardClient(IPAddress address, int port, MoonServerDB database) :
            this(address, port, database, new DebugWriter())
        { }

        public MoonboardClient(string ipAddress, int port, MoonServerDB database, Stream log) :
            this(IPAddress.Parse(ipAddress), port, database, new StreamWriter(log))
        { }

        public MoonboardClient(string ipAddress, int port, MoonServerDB database, TextWriter log) :
            this(IPAddress.Parse(ipAddress), port, database, log)
        { }

        public MoonboardClient(IPAddress address, int port, MoonServerDB database, TextWriter log)
        {
            Database = database;
            Address = address;
            Port = port;
            Log = log;
            Debug = false;
        }

        public void ShowProblem(int id)
        {
            Problem prb = Database.Problems.First(p => p.Id == id);
            PositionStrings ps = new PositionStrings(prb);
            try
            {
                OpenConnection();
            }
            catch (SocketException se)
            {
                throw new MoonboardClientException(string.Format("{0}:{1}", se.SocketErrorCode, se.Message));
            }
            try
            {
                ClearBoard();
                LightHolds(ps.Normal.Concat(ps.Start.Concat(ps.End)));
            }
            catch (MoonboardClientException mbe)
            {
                CloseConnection();
                throw mbe;
            }
            CloseConnection();
        }

        private void LightHolds(IEnumerable<string> holds)
        {
            SendCommand("SET", string.Join(" ", holds));
        }

        private void ClearBoard()
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
            while (totalBytesSent < sendBuffer.Length)
            {
                totalBytesSent += clientSock.Send(sendBuffer, totalBytesSent, sendBuffer.Length - totalBytesSent, SocketFlags.None);
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
                if (rcvdList.Count == 0) { throw new MoonboardClientException("WaitForAck(): No response received"); }
                while (rcvdList.Count > 0)
                {
                    rcvd = rcvdList[0];
                    rcvdList.RemoveAt(0);
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
            while (rcvdList.Count == 0)
            {
                try
                {
                    if ((bytesRcvd = clientSock.Receive(rcvBuffer, 0, rcvBuffer.Length, SocketFlags.None)) == 0)
                    {
                        throw new MoonboardClientException("Connection closed prematurely");
                    }
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
                    throw se;
                }
                catch (ObjectDisposedException)
                {
                    throw new MoonboardClientException("ReceiveLines(): Socket disposed while receiving");
                }
                chars = new char[bytesRcvd];
                dec.GetChars(rcvBuffer, 0, bytesRcvd, chars, 0);
                rcvdBuf += new string(chars);
                int i;
                while ((i = rcvdBuf.IndexOf("\r\n")) != -1)
                {
                    string rcvdStr = rcvdBuf.Substring(0, i);
                    rcvdList.Add(rcvdStr);
                    Log.WriteLine(string.Format("Received: {0}", rcvdStr));
                    rcvdBuf = rcvdBuf.Substring(i + 2);
                }
            }
        }

        private void OpenConnection()
        {
            clientSock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            clientSock.Connect(Address, Port);
            clientSock.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReceiveTimeout, 10000);
            rcvdList = new List<string>();
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