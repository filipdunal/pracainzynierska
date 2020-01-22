using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchPages : MonoBehaviour
{
    public GameObject linkPage;
    public GameObject serverInfoPage;

    public void SwitchToLinkPage()
    {
        serverInfoPage.SetActive(false);
        linkPage.SetActive(true);
    }

    public void SwitchToServerInfoPage()
    {
        serverInfoPage.SetActive(true);
        linkPage.SetActive(false);
    }


}
