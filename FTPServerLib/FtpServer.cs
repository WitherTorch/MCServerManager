using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Net.Sockets;
using System.IO;
using System.Threading;


namespace FTPServerLib
{
    public class FtpServer : IDisposable
    {

        private bool _disposed = false;
        private bool _listening = false;
        private Action<string> msgAction; 
        private TcpListener _listener;
        private List<ClientConnection> _activeConnections;
        internal static string homeDir ;
        private IPEndPoint _localEndPoint;

        public FtpServer(string dir,Action<string> msgAction)
        :this(IPAddress.Any, 21, dir, msgAction)
        {
        }

        public FtpServer(IPAddress ipAddress, int port,string dir, Action<string> msgAction)
        {
            _localEndPoint = new IPEndPoint(ipAddress, port);
            homeDir = dir;
            this.msgAction = msgAction;
        }

        public void Start()
        {
            _listener = new TcpListener(_localEndPoint);


            _listening = true;
            _listener.Start();

            _activeConnections = new List<ClientConnection>();

            _listener.BeginAcceptTcpClient(HandleAcceptTcpClient, _listener);
        }

        public void Stop()
        {

            _listening = false;
            _listener.Stop();

            _listener = null;
        }

        private void HandleAcceptTcpClient(IAsyncResult result)
        {
            if (_listening)
            {
                _listener.BeginAcceptTcpClient(HandleAcceptTcpClient, _listener);

                TcpClient client = _listener.EndAcceptTcpClient(result);

                ClientConnection connection = new ClientConnection(client,msgAction);

                _activeConnections.Add(connection);

                ThreadPool.QueueUserWorkItem(connection.HandleClient, client);
            }
        }

        public void Dispose()
        {
            Dispose(true);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    Stop();

                    foreach (ClientConnection conn in _activeConnections)
                    {
                        conn.Dispose();
                    }
                }
            }

            _disposed = true;
        }
    }
}
