using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConnectionDetailsOpener : MonoBehaviour
{
    bool isOpen;
    Animator ani;
    public void OpenDetails()
    {
        if(ani!=null)
        {
            isOpen = ani.GetBool("open");
            ani.SetBool("open", !isOpen);
        }

    }


    private void Start()
    {
        ani = GetComponent<Animator>();
    }
}
