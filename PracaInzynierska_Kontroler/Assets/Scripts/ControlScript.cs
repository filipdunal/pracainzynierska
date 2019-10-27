using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlScript : MonoBehaviour
{
    int count = 0;
    public void actionClicked()
    {
        count++;
        NetworkClientUI.SendToPC(count.ToString());
        
    }
}
