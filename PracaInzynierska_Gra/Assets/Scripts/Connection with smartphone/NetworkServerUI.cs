using System.Collections;
using System.Net;
using System.Net.Sockets;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.NetworkSystem;
using UnityStandardAssets.CrossPlatformInput;
#pragma warning disable 0618 

public class NetworkServerUI : MonoBehaviour
{
    static string ipaddress;

    //Received data from mobile phone 
    public static string receivedString;

    private void OnGUI()
    {
        //Only for testing
        ipaddress = LocalIPAddress();
        GUI.Box(new Rect(10, Screen.height - 50, 100, 50), ipaddress);
        GUI.Label(new Rect(20, Screen.height - 35, 100, 20), "Status:" + NetworkServer.active);
        GUI.Label(new Rect(20, Screen.height - 20, 100, 20), "Connected:" + NetworkServer.connections.Count);
    }
    private void Start()
    {
        ipaddress = LocalIPAddress();
        if (!NetworkServer.active)
        {
            ServerStart();
        }
    }
    private void Update()
    {
        ipaddress = LocalIPAddress();
        if (!IsConnectedToNetwork())
        {
            if(NetworkServer.active)
            {
                Debug.Log("Reset of server");
                NetworkServer.Reset();
            }     
        }
        else
        {
            if(!NetworkServer.active)
            {
                Start();
            }
        }
    }

    static bool IsConnectedToNetwork()
    {
        if (ipaddress[0] == '1' && ipaddress[1] == '2' && ipaddress[2] == '7')
            return false;
        else
            return true;
    }

    public static void ServerStart()
    {
        if(IsConnectedToNetwork())
        {
            NetworkServer.Reset();
            NetworkServer.Listen(SettingsOfUser.lastUsedNetworkPort);
            NetworkServer.RegisterHandler(999, ServerReceiveMessage);
        }
    }
    private static void ServerReceiveMessage(NetworkMessage message)
    {
        StringMessage messg = new StringMessage();
        messg.value = message.ReadMessage<StringMessage>().value;
        string data = messg.value;
        receivedString = data;
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
