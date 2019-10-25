using System.Collections;
using System.Net;
using System.Net.Sockets;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.NetworkSystem;
#pragma warning disable 0618 

public class NetworkServerUI : MonoBehaviour
{
    public static bool isConnected;
    private void OnGUI()
    {
        string ipaddress = LocalIPAddress();
        GUI.Box(new Rect(10, Screen.height - 50, 100, 50), ipaddress);
        GUI.Label(new Rect(20, Screen.height - 35, 100, 20), "Status:" + NetworkServer.active);
        GUI.Label(new Rect(20, Screen.height - 20, 100, 20), "Connected:" + NetworkServer.connections.Count);
    }
    private void Start()
    {
        if(!NetworkServer.active)
        {
            ServerStart();
        }
        
    }

    public void ServerStart()
    {
        NetworkServer.Reset();
        NetworkServer.Listen(SettingsOfPlayer.lastUsedNetworkPort);

    }
    public static string LocalIPAddress()
    {
        IPHostEntry host;
        string localIP = "0.0.0.0";
        host = Dns.GetHostEntry(Dns.GetHostName());
        foreach (IPAddress ip in host.AddressList)
        {
            if (ip.AddressFamily == AddressFamily.InterNetwork)
            {
                localIP = ip.ToString();
                return localIP;
            }
        }
        return "error";
    }
}
