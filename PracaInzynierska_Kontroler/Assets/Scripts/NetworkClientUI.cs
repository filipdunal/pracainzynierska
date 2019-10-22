using System.Collections;
using System.Net;
using System.Net.Sockets;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine.Networking.NetworkSystem;
using UnityEngine;
using UnityEngine.UI;
#pragma warning disable 0618 
public class NetworkClientUI : MonoBehaviour
{
    public InputField serverIP;
    public InputField serverPort;
    public Text result;
    public Text connectedOrNot;
    public Button connectButton;
    public Button disconnectButton;

    static NetworkClient client;
    public static bool isConnected;
    private void OnGUI()
    {
        if(client.isConnected)
        {
            connectButton.interactable = false;
            disconnectButton.interactable = true;
            connectedOrNot.color = Color.green;
            connectedOrNot.text = "Connected";
            isConnected = true;
        }
        else
        {
            connectButton.interactable = true;
            disconnectButton.interactable = false;
            connectedOrNot.color = Color.red;
            connectedOrNot.text = "Not connected";
            isConnected = false;
        }
    }

    public void PressExitButton()
    {
        if(client.isConnected)
        {
            client.Disconnect();
        }
        Application.Quit();
    }
    

    private void Start()
    {
        client = new NetworkClient();
    }
    public void Connect()
    {
        int serverPortInt=0;
        string localIP = LocalIPAddress();

        //Checking if client is connected to the internet;
        if (localIP[0]=='1'&& localIP[0] == '2'&& localIP[0] == '7')
        {
            result.color = Color.red;
            result.text = "Error: You are not connected to the network";
            return;
        }

        //Checking if IP is correct
        if(serverIP.text=="")
        {
            result.color = Color.red;
            result.text = "Error: IP cannot be empty";
            return;
        }
        foreach(char x in serverIP.text)
        {
            if(!((x>='0' && x<='9')||x=='.'))
            {
                result.color = Color.red;
                result.text = "Error: IP must contains only 0-9 digits and dot symbol";
                return;
            }
        }

        //Checking if port is correct
        if (serverPort.text == "")
        {
            result.color = Color.red;
            result.text = "Error: Port cannot be empty";
            return;
        }
        if (!int.TryParse(serverPort.text, out serverPortInt))
        {
            result.color = Color.red;
            result.text = "Error: Port must contains only 0-9 digits";
            return;
        }
        result.text = "";
        //If everything is OK then try to connect
        client.Connect(serverIP.text, serverPortInt);
        
    }
    
    public void Disconnect()
    {
        client.Disconnect();
    }


    public string LocalIPAddress()
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

    public static void SendData(float x, float y, float z, float w, bool buttonPressed)
    {

    }

}
