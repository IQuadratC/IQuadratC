﻿using System.Threading;
using Network.Both;
using Unity.Mathematics;
using UnityEngine;
using Utility;

namespace Network.Server
{
    public class ServerSend
    {
        private Network.Server.Server server;
        public ServerSend(Network.Server.Server server)
        {
            this.server = server;
        }
        
        public void DebugMessage(string message)
        {
            using (Packet packet = new Packet((byte) Packets.debugMessage))
            {
                packet.Write(message);
                server.SendTCPDataToAll(packet);
            }
        }
        
        public void ServerSettings(ServerClient client)
        {
            string version = "1.1";
            Debug.Log("SERVER: [" + client.id + "] sending settings" +
                  "\nVersion " + version +
                  "\nUDP " + server.udpSupport.value +
                  "\nCam " + server.camSupport.value +
                  "\nJoystick " + server.joystickSupport.value +
                  "\nChat " + server.chatSupport.value +
                  "\nLidar " + server.lidarSupport.value
            );
            
            using (Packet packet = new Packet((byte) Packets.serverSettings))
            {
                packet.Write(client.id);
                
                packet.Write(version);
                packet.Write(server.udpSupport.value);
                packet.Write(server.camSupport.value);
                packet.Write(server.joystickSupport.value);
                packet.Write(server.chatSupport.value);
                packet.Write(server.lidarSupport.value);

                server.SendTCPData(client, packet);
            }
        }
        
        public void ServerStartUDP(ServerClient client)
        {
            Debug.LogFormat("SERVER: [" +client.id+ "] starting udp test");
            using (Packet packet = new Packet((byte) Packets.serverStartUDP))
            {
                server.SendTCPData(client, packet);
            }
            
            Threader.RunAsync(() =>
            {
                Thread.Sleep(2000);
                if (client.updConnected) {return;}
                ServerUDPConnection(client, false);
            });
        }
        
        public void ServerUDPConnection(ServerClient client, bool recived)
        {
            Debug.LogFormat("SERVER: [" +client.id+ "] udp test message");
            using (Packet packet = new Packet((byte) Packets.serverUDPConnection))
            {
                packet.Write(recived);

                if (recived)
                {
                    server.SendUDPData(client, packet);
                }
                else
                {
                    server.SendTCPData(client, packet);
                }
            }
        }
        
        public void ServerLidarStatus(ServerClient client, int status)
        {
            Debug.LogFormat("SERVER: [" +client.id+ "] sending Lidar status: " + status);
            using (Packet packet = new Packet((byte) Packets.serverLidarStatus))
            {
                packet.Write(status);
                server.SendTCPData(client, packet);
            }
        }
        
        public void ServerSLAMMap(ServerClient client, byte[] map)
        {
            Debug.LogFormat("SERVER: [" +client.id+ "] sending SLAM map ");
            using (Packet packet = new Packet((byte) Packets.servertSLAMMap))
            {
                packet.Write(map.Length);
                packet.Write(map);
                server.SendUDPData(client, packet);
            }
        }
        
        public void ServerPosition(ServerClient client, float3 pos)
        {
            Debug.LogFormat("SERVER: [" +client.id+ "] sending Position: " + pos);
            using (Packet packet = new Packet((byte) Packets.servertSLAMMap))
            {
                packet.Write(pos);
                server.SendUDPData(client, packet);
            }
        }
    }
}