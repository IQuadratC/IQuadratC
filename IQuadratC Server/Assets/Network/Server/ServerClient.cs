﻿using System.Net;
using System.Net.Sockets;
using Network.Both;

namespace Network.Server
{
    public class ServerClient
    {
        public byte id;
        public TcpClient socket;
        public NetworkStream stream;
        public byte[] receiveBuffer;
        public string ip;
        public IPEndPoint endPoint;
        public NetworkState state;
        
        public bool clientUdpSupport;
        public bool updConnected;
        
        public bool clientCamSupport;
        public bool clientJoystickSupport;
        public bool clientChatSupport;
        public bool clientLidarSupport;

        public ServerClient(byte id, TcpClient socket)
        {
            this.id = id;
            this.socket = socket;
            state = NetworkState.notConnected;
        }
    }
}