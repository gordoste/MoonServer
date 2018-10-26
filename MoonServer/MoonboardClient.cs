using MoonServer.Models;
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

        public bool ShowProblem(int id)
        {
            ConfirmConnected();
            ReceiveDebugLogTimeout();
            if (!ClearBoard()) { return false; }
            Problem prb = Database.Problems.First(p => p.Id == id);
            PositionStrings ps = new PositionStrings(prb);
            if (!LightHolds(ps.Normal)) { return false; }
            if (!LightHolds(ps.Start)) { return false; }
            if (!LightHolds(ps.End)) { return false; }
            return true;
        }

        private bool LightHolds(List<string> holds)
        {
            return SendCommand("SET", string.Join(" ", holds));
        }

        private bool ClearBoard()
        {
            return SendCommand("CLR");
        }

        private bool SendCommand(string command, string data = null)
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
            bool result = WaitForAck(CmdId);
            CmdId++;
            return result;
        }

        private bool WaitForAck(int cmdId)
        {
            string expected = string.Format("ACK {0}", cmdId);
            string rcvd;
            while (true)
            {
                ReceiveLines();
                if (rcvdList.Count == 0) { return false; }
                while (rcvdList.Count > 0)
                {
                    rcvd = rcvdList[0];
                    rcvdList.RemoveAt(0);
                    if (rcvd.Equals(expected)) { return true; }
                    if (!ProcessDebugLog(rcvd))
                    {
                        throw new IOException(string.Format("Expected '{0}', got '{1}'", expected, rcvd));
                    }
                }
            }
        }

        private bool ReceiveLines()
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
                        Log.WriteLine("Connection closed prematurely");
                        Reset();
                        return false;
                    }
                }
                catch (SocketException se)
                {
                    if (se.SocketErrorCode == SocketError.TimedOut)
                    {
                        Log.WriteLine(string.Format("Timed out without receiving complete response. Response: '{0}'", rcvdBuf));
                        clientSock.Close();
                        return false;
                    }
                }
                chars = new char[bytesRcvd];
                dec.GetChars(rcvBuffer, 0, bytesRcvd, chars, 0);
                rcvdBuf += new string(chars);
                int i;
                while ((i = rcvdBuf.IndexOf("\r\n")) != -1)
                {
                    rcvdList.Add(rcvdBuf.Substring(0, i));
                    rcvdBuf = rcvdBuf.Substring(i + 2);
                }
            }
            return true;
        }

        private void ReceiveDebugLogTimeout()
        {
            while (clientSock.Available > 0)
            {
                ReceiveLines();
                string rcvd;
                while (rcvdList.Count > 0)
                {
                    rcvd = rcvdList[0];
                    rcvdList.RemoveAt(0);
                    if (Debug) { Log.WriteLine("Received " + rcvd); }
                    if (!ProcessDebugLog(rcvd))
                    {
                        throw new IOException(string.Format("Unexpected message '{0}'", rcvd));
                    }
                }
            }
        }

        private bool ProcessDebugLog(string s)
        {
            if (s.StartsWith("DBG"))
            {
                if (Debug) Log.WriteLine(s);
                return true;
            }
            if (s.StartsWith("LOG"))
            {
                Log.WriteLine(s);
                return true;
            }
            return false;
        }

        private void ConfirmConnected()
        {
            if (clientSock.Connected) { return; }
            Reset();
            clientSock.Connect(Address, Port);
            clientSock.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReceiveTimeout, 10000);
        }

        public void Reset()
        {
            rcvdList = new List<string>();
            rcvdBuf = "";
            if (clientSock != null && clientSock.Connected) { clientSock.Close(); }
            clientSock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
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
}